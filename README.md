# Unity AI Agent 協作範本｜嶺東數媒教學與開發

**版本：v0.1.0** · Unity **6000.3.17f1** · Input System Package (New) · 繁體中文

Repo：https://github.com/dcd-ltu/unity-ai-agent-template

提供 Unity 教學、遊戲與 AR 專案使用的自訂資料夾、AI 協作規範。這是補充資料包，沒有遊戲場景，不是可直接以 Unity Hub 開啟的完整專案。

## 使用方式

1. 以 Unity Hub 建立 6000.3.17f1 專案，依初始化指南安裝新版 Input System；Active Input Handling 選 New，不選 Both。
2. 將本 repo 的內容合併至專案根目錄；Assets 合併到既有 Assets，已有規則檔先比較再合併。
3. 安裝 Git LFS，在專案根目錄執行 `git lfs install`；需要取得已追蹤的套件時執行 `git lfs pull`。
4. 使用既有資料夾骨架；匯入模型時自行建立對應模型專屬資料夾。
5. Agent 先讀 AGENTS.md、docs/PROJECT_STATUS.md，再讀任務。Codex、Kiro、Antigravity、Claude Code 已附入口；首次啟動仍須確認讀取。

本資料包不建立 Packages、ProjectSettings、Build／Builds，也不刪除真實專案已有的系統目錄。合併時保留既有資產與 .meta，不覆蓋或刪除使用者工作。

## 模型管理：本版已採用

每個模型一個資料夾，例如 `Assets/_Project/Art/Models/Robot/Robot.fbx`。來源 FBX 必須內嵌全部使用的貼圖；匯入後的材質與貼圖放在該模型的 Materials／Textures 子資料夾，不放全域目錄。第三方模型放 `Assets/ThirdParty/<來源>/Models/<模型>/`。詳見模型規範。

## 文件索引

| 文件 | 用途 |
| --- | --- |
| [資料夾說明](docs/01_資料夾與文件說明.md) | 各目錄用途 |
| [初始化與 Input System](docs/02_初始化與InputSystem.md) | Editor 設定 |
| [AI 協作流程](docs/03_AI協作流程.md) | 工作與交接 |
| [教學檢查](docs/04_教學與發布檢查.md) | 課堂驗收 |
| [Agent 啟動](docs/05_AI啟動提示詞.md) | 啟動提示詞 |
| [Agent 入口](docs/06_Agent相容性與解壓縮指南.md) | 多工具支援 |
| [FBX 模型規範](docs/07_FBX模型匯入規範.md) | 內嵌、擷取與更新 |
| [公開發布與授權](docs/08_公開發布與授權說明.md) | 再散布 |
| [TGZ／LFS Q&A](docs/09_TGZ與GitLFS_QA.md) | AR 套件異常處理 |
| [後續計畫](docs/ROADMAP.md) | 版本里程碑與驗收 |
| [版本政策](docs/VERSIONING.md) | 版本號與發布流程 |

## 已完成與待驗證

已建立結構、規範、LFS 設定；尚未在 Unity 編譯、Play Mode 或 Build 驗證。Robot.fbx 尚未下載或加入，Vuforia TGZ 亦未附；AR 裝置功能待驗證。版本號代表範本內容基準，不代表 Unity 驗收已通過。

CreateFolder 工具維持原 repo 獨立管理，本版未收錄，也不設定其安裝依賴。

## 授權

自有內容 MIT。Quaternius 模型若加入，沿用原 CC0。Vuforia 另依原授權。詳見 LICENSE 與 THIRD_PARTY_NOTICES.md。
