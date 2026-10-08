# Unity AI Agent 協作範本｜嶺東數媒教學與開發

**版本：v0.2.0** · Unity **6000.3.17f1** · Input System Package (New) · 繁體中文

Repo：https://github.com/dcd-ltu/unity-ai-agent-template

提供 Unity 教學、遊戲與 AR 專案使用的自訂資料夾、AI 協作規範。這是補充資料包，沒有遊戲場景，不是可直接以 Unity Hub 開啟的完整專案。

## 下載方式：一般使用者請從 Release 下載

**課堂與一般使用請到 [Releases](https://github.com/dcd-ltu/unity-ai-agent-template/releases)，下載老師指定版本的 `unity-ai-agent-template-vX.Y.Z.zip` 附件。不要使用 Code → Download ZIP、git clone 或 Use this template 作為課堂初始化方式。** 若尚無該版本的 Release 附件，請等待發布，不以 main 替代。

clone 會取得本範本的 `.git` 歷史及 origin；直接搬入自己的專案，可能造成巢狀儲存庫或推送到錯誤遠端。Code → Download ZIP 本身不含 `.git`，但取得的是分支內容，可能尚未驗收。Release 附件採固定版本，排除 `.git`；保留 `.gitignore`、`.gitattributes` 與 AI 規則，這些共用檔案仍須與既有設定比較後合併。

維護者與貢獻者才使用 clone／feature 分支；學生專案使用自己的 Git 初始化與遠端，不沿用本範本的 origin。Releases 中自動產生的 Source code (zip/tar.gz) 與課堂分發附件不同，請選老師指定的命名附件。

## 使用方式

1. 以 Unity Hub 建立 6000.3.17f1 專案，依初始化指南安裝新版 Input System；Active Input Handling 選 New，不選 Both。
2. 將 Release 附件解壓後的內容合併至專案根目錄；Assets 合併到既有 Assets，已有規則檔先比較再合併。
3. 安裝 Git LFS，在專案根目錄執行 `git lfs install`；需要取得已追蹤的套件時執行 `git lfs pull`。
4. 等 Unity 匯入完成，在選單選 `Tools → DCD → 初始化專案場景`，把預設 SampleScene 搬至 `Assets/_Project/Scenes`；詳見場景初始化指南。
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
| [場景初始化](docs/10_場景初始化.md) | 搬移 SampleScene 與驗收 |
| [後續計畫](docs/ROADMAP.md) | 版本里程碑與驗收 |
| [版本政策](docs/VERSIONING.md) | 版本號與發布流程 |

## 已完成與待驗證

已建立結構、規範、LFS 設定；尚未在 Unity 編譯、Play Mode 或 Build 驗證。Robot.fbx 尚未下載或加入，Vuforia TGZ 亦未附；AR 裝置功能待驗證。版本號代表範本內容基準，不代表 Unity 驗收已通過。

CreateFolder 工具維持原 repo 獨立管理，本版未收錄，也不設定其安裝依賴。

## 授權

自有內容 MIT。Quaternius 模型若加入，沿用原 CC0。Vuforia 另依原授權。詳見 LICENSE 與 THIRD_PARTY_NOTICES.md。
