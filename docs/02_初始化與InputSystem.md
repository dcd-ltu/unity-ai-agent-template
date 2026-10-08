# 初始化與新版 Input System

## 由老師先產生完整範本

1. Unity Hub 安裝並選擇 **6000.3.17f1**；依課程選擇相容的 2D 或 3D 範本。建議第一版先決定單一方向，不同學期可另開 2D/3D 版本。
2. 在本機建立新 Unity 專案，關閉 Editor，再把本骨架內容合併到專案根目錄。本包不提供 Packages、ProjectSettings 或 Build/Builds；保留 Unity 已產生的系統目錄。Assets 合併子目錄，既有同名文件與 .gitignore 應先比較再合併，不直接覆蓋。
3. 用指定版本重新開啟，等待資產匯入。
4. 開啟 Window > Package Manager，確認 Input System 已安裝；若沒有，在 Unity Registry 安裝 Editor 提供的相容版本。記錄實際版本，不固定猜測版號。
5. Edit > Project Settings > Player > Other Settings > Configuration > Active Input Handling 選 **Input System Package (New)**。若提示重新啟動，完成重啟。
6. 設定 Version Control 為 Visible Meta Files、Asset Serialization 為 Force Text；依 Editor 畫面搜尋設定名稱。
7. 建立 Assets/_Project/Scenes/Main.unity，儲存場景，於 Unity 6 的 Build Profiles 中設定要建置的場景。
8. 建立最小輸入示範後，完成下方驗證，提交 Assets、Packages、ProjectSettings 及 .meta。這時才是可供同學複製的完整專案。

## 輸入資產規劃

在 Assets/_Project/Input 建立 `GameInput.inputactions`。

| Action Map | Action | 類型 | 建議綁定 |
| --- | --- | --- | --- |
| Gameplay | Move | Value / Vector2 | WASD、方向鍵、手把左搖桿 |
| Gameplay | Interact | Button | E、手把南側按鈕 |
| Gameplay | Pause | Button | Escape、手把 Start |
| UI | 導覽、提交、取消、指標與點擊等 | 使用套件預設 UI Actions | 依 UI 操作需求配置 |

第一堂課先做 Move 即可，其他操作依遊戲需求加入。遊戲程式透過 InputActionReference 或 PlayerInput 讀取動作，避免把按鍵規則散落在每個腳本。切換選單時明確管理 Gameplay 與 UI 的啟用狀態。

uGUI 的 EventSystem 使用 InputSystemUIInputModule，移除舊 StandaloneInputModule，確認 Actions 綁定。其他 UI 技術採對應官方整合方式。

## 老師發布前驗證

- Editor 顯示 6000.3.17f1；Console 沒有編譯錯誤。
- 記錄 Input System 實際版號；Active Input Handling 為 New。
- Main 場景可進入 Play Mode；Move 輸入可驗證，停用再啟用不重複觸發。
- 如果有選單，滑鼠與鍵盤都能操作；暫停時角色不繼續移動。
- 完成一次目標平台 Build，實際開啟執行檔驗證。
- 在另一個乾淨目錄複製 Repo，使用相同 Editor 再開啟，確認沒有遺漏資產或設定。

## 官方參考

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/manual/Installation.html
- https://github.com/Unity-Technologies/InputSystem/blob/develop/Packages/com.unity.inputsystem/Documentation~/Installation.md

查閱時優先用本專案實際安裝版號的文件；develop 與 latest 可能超前本專案。

## 預設場景搬移

取得 Release 附件並合併後，等待編譯完成，執行 `Tools → DCD → 初始化專案場景`。保留 SampleScene 名稱；檢查 Build Profiles 與 .meta。詳見 [場景初始化](10_場景初始化.md)。
