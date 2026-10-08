#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DcdLtu.Template.Editor
{
    /// <summary>透過選單搬移預設場景，保留 GUID 與建置清單順序。</summary>
    public static class InitializeProjectScene
    {
        private const string MenuPath = "Tools/DCD/初始化專案場景";
        private const string SourcePath = "Assets/Scenes/SampleScene.unity";
        private const string TargetFolder = "Assets/_Project/Scenes";
        private const string TargetPath = TargetFolder + "/SampleScene.unity";

        [MenuItem(MenuPath)]
        private static void Initialize()
        {
            bool moved = false;
            try
            {
                if (AssetDatabase.LoadAssetAtPath<SceneAsset>(SourcePath) == null)
                {
                    Notify(AssetDatabase.LoadAssetAtPath<SceneAsset>(TargetPath) != null
                        ? "場景已位於目標資料夾，無須再次搬移。"
                        : "找不到 " + SourcePath + "。若使用其他範本或已改名，請在 Unity Project 視窗手動搬移。" );
                    return;
                }

                // 同名資產、資料夾與殘留 .meta 都視為衝突，不覆蓋。
                if (File.Exists(TargetPath) || Directory.Exists(TargetPath) || File.Exists(TargetPath + ".meta"))
                {
                    Notify("目標已有同名檔案、資料夾或 .meta，已停止。請先檢查：" + TargetPath);
                    return;
                }

                if (!EditorUtility.DisplayDialog("初始化專案場景",
                    SourcePath + "\n將搬至\n" + TargetPath +
                    "\n\n保留場景名稱與 GUID，同步更新全域與專案內 Build Profile 清單；不刪除原 Scenes 資料夾。",
                    "搬移", "取消"))
                    return;

                if (HasDirtyScenes())
                {
                    // 必須成功儲存才能繼續，不提供略過儲存的搬移流程。
                    if (!EditorUtility.DisplayDialog("尚有未儲存場景",
                        "請先儲存所有開啟的場景，再搬移 SampleScene。", "儲存並繼續", "取消")
                        || !EditorSceneManager.SaveOpenScenes() || HasDirtyScenes())
                        return;
                }

                if (AssetDatabase.LoadAssetAtPath<SceneAsset>(SourcePath) == null)
                {
                    Notify("儲存後來源路徑已變更，已停止。請重新確認場景位置。");
                    return;
                }

                string sceneGuid = AssetDatabase.AssetPathToGUID(SourcePath);
                var globalScenes = EditorBuildSettings.globalScenes;
                var profileScenes = new Dictionary<BuildProfile, EditorBuildSettingsScene[]>();
                foreach (string guid in AssetDatabase.FindAssets("t:BuildProfile", new[] { "Assets" }))
                {
                    var profile = AssetDatabase.LoadAssetAtPath<BuildProfile>(AssetDatabase.GUIDToAssetPath(guid));
                    if (profile != null)
                        profileScenes[profile] = profile.scenes;
                }

                EnsureFolder(TargetFolder);
                string error = AssetDatabase.MoveAsset(SourcePath, TargetPath);
                if (!string.IsNullOrEmpty(error))
                    throw new InvalidOperationException(error);
                moved = true;

                if (sceneGuid != AssetDatabase.AssetPathToGUID(TargetPath))
                    throw new InvalidOperationException("搬移後 GUID 檢查不一致，請檢查資產參照。");

                if (Remap(globalScenes))
                    EditorBuildSettings.globalScenes = globalScenes;
                foreach (var entry in profileScenes)
                {
                    if (!Remap(entry.Value))
                        continue;
                    entry.Key.scenes = entry.Value;
                    EditorUtility.SetDirty(entry.Key);
                    AssetDatabase.SaveAssetIfDirty(entry.Key);
                }

                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(TargetPath);
                EditorGUIUtility.PingObject(scene);
                Debug.Log("[DCD] 場景搬移完成：" + TargetPath + "；GUID：" + sceneGuid);
                Notify("搬移完成。請檢查已開啟場景的儲存路徑與 Build Profiles 場景清單。\n" + TargetPath);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                Notify((moved ? "場景已搬移，但後續驗證或建置清單更新失敗。請檢查目標與各 Build Profile，不要重新覆蓋場景。"
                    : "場景尚未搬移，請檢查 Console 的錯誤。") + "\n" + exception.Message);
            }
        }

        [MenuItem(MenuPath, true)]
        private static bool ValidateInitialize()
        {
            return !EditorApplication.isPlayingOrWillChangePlaymode
                && !EditorApplication.isCompiling && !EditorApplication.isUpdating;
        }

        private static bool HasDirtyScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
                if (SceneManager.GetSceneAt(i).isDirty)
                    return true;
            return false;
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;
            int separator = path.LastIndexOf('/');
            string parent = path.Substring(0, separator);
            EnsureFolder(parent);
            if (string.IsNullOrEmpty(AssetDatabase.CreateFolder(parent, path.Substring(separator + 1))))
                throw new InvalidOperationException("無法建立資料夾：" + path);
        }

        private static bool Remap(EditorBuildSettingsScene[] scenes)
        {
            bool changed = false;
            if (scenes == null)
                return false;
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] == null || scenes[i].path != SourcePath)
                    continue;
                scenes[i] = new EditorBuildSettingsScene(TargetPath, scenes[i].enabled);
                changed = true;
            }
            return changed;
        }

        private static void Notify(string message)
        {
            EditorUtility.DisplayDialog("DCD 場景初始化", message, "確定");
        }
    }
}
#endif
