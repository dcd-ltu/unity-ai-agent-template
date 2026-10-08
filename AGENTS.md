# AI Agent 共用協作規範

## 開始前

- 回覆、計畫、提交說明與教學文件均使用繁體中文；類別、變數與路徑用清楚英文命名。
- 先讀 README.md、docs/02_初始化與InputSystem.md、docs/03_AI協作流程.md、docs/PROJECT_STATUS.md 與本次任務。
- 固定 Unity Editor 6000.3.17f1。不得自行升級 Editor、套件或切換渲染管線。
- 檢查實際 Packages/manifest.json、packages-lock.json 與 ProjectSettings，不能把骨架視為已初始化專案。

## 輸入系統：強制規則

- 只使用 com.unity.inputsystem 與 UnityEngine.InputSystem。
- Active Input Handling 必須設為 Input System Package (New)，不可設成 Both 或 Input Manager (Old)。
- 禁止新增 UnityEngine.Input、Input.GetAxis、Input.GetAxisRaw、Input.GetButton、Input.GetKey、Input.GetMouseButton 等舊 API；禁止使用舊 StandaloneInputModule。
- 遊戲操作以 Input Actions 定義，集中於 Assets/_Project/Input；使用 InputActionReference 或 PlayerInput 接入遊戲邏輯。直接查詢 Keyboard.current/Mouse.current 只限有明確需求的工具或除錯操作，須處理裝置不存在的情況。
- 使用 uGUI 時，EventSystem 使用 InputSystemUIInputModule，並驗證 UI Actions 指派正確。UI Toolkit 則依該版本官方支援方式設定，不要強行套用 uGUI 元件。
- Action 的 Enable/Disable 與事件訂閱/取消必須成對；停用或銷毀後不得重複收到輸入事件。
- 不假定套件最新版適合本專案。使用此 Editor 的 Package Manager 所提供的相容版本，將實際版本提交到 manifest 與 lock 檔並記錄。

## 資產與實作

- 本資料包只提供自訂內容，不建立 Packages、ProjectSettings、Build/Builds 等系統目錄。合併到已初始化的 Unity 專案；保留其設定與既有文件，既有 .gitignore 先比較再合併。不得把這些系統目錄從真正的 Unity 專案刪除。

- 自有資產放 Assets/_Project；第三方資產放 Assets/ThirdParty，避免修改第三方內容。
- 資產移動、重新命名與刪除，優先透過 Unity Editor；保留既有 .meta 與 GUID，不可重建來修復參照。
- 不手寫不存在的 Scene、Prefab 或 .meta 來假裝完成 Editor 工作。必要時提供 Editor 工具或明確操作步驟。
- 新增公開或 SerializeField 欄位，須交代 Inspector 指派、預設值與操作方式。
- 優先簡單 MonoBehaviour 與小型 C# 類別；沒有需求不增加框架、單例群、服務容器或額外套件。
- 本範本尚未建立 asmdef；初期可用預設組件。開始建立 Unity Test Framework 測試時，按測試與執行程式依賴建立 asmdef，避免測試引用不到程式碼。
- 不提交 Library、Temp、Obj、Logs、UserSettings、Builds、憑證、帳密或付費素材。

## FBX 模型交付與匯入：強制規則

- 所有新交付、匯入或更新的 FBX，必須內嵌其材質使用的全部貼圖；不得依賴交付者電腦上的外部貼圖路徑。材質定義須隨 FBX 一併匯出。純幾何、確實不使用貼圖的模型，須在匯入紀錄註明「無貼圖」，不可用此註記掩蓋缺漏。
- 未內嵌或內嵌不完整的 FBX 不予驗收，退回製作者在建模工具重新匯出；AI 不得以另外下載貼圖或從全域 Textures 補檔取代此要求。
- 每個模型一個資料夾：Assets/_Project/Art/Models/<ModelName>/。FBX 放在此資料夾；擷取的材質與貼圖分別放其 Materials/、Textures/ 子資料夾，禁止移到全域 Art/Materials 或 Art/Textures。
- 第三方 FBX 同樣適用內嵌與每模型集中管理原則，放於 Assets/ThirdParty/<來源>/Models/<ModelName>/；不可未經許可改造或再散布第三方資產。
- 流程：製作者啟用建模工具的媒體／貼圖內嵌選項並匯出 → 隔離外部來源貼圖驗證 FBX 可獨立取得貼圖 → 放入模型專屬資料夾 → 用 Unity 擷取內嵌貼圖到 Textures/ → 擷取材質到 Materials/ → 檢查材質貼圖連結、Shader 與目前渲染管線 → 場景外觀驗證 → 記錄結果後驗收。
- 內嵌是來源 FBX 的交付要求；Unity 擷取後產生外部 .mat 與貼圖是正常的專案流程，來源 FBX 仍保留。FBX 內嵌不代表建模工具的 Shader 能完整轉換成 Unity 材質。
- 更新既有 FBX 前先確認並保留 .meta、GUID 與材質對應；不可直接覆蓋已調整的材質或任意重建貼圖。記錄更新差異並重新驗證 Prefab 與場景參照。
- AI 無法檢查內嵌內容、操作 Editor 或驗證外觀時，標記「待 FBX 內嵌／Editor 驗證」，不可只靠副檔名或畫面正常就宣稱通過。
- 完整操作與驗收表見 docs/07_FBX模型匯入規範.md。

## 驗證與交接

- 流程：規劃 → 實作 → 編譯 → 執行 → 測試 → 審查；失敗修復後重測。
- 不能操作 Unity 時，清楚標記「待 Editor 驗證」，不可宣稱 Play Mode 或 Build 通過。
- 修改前檢查現有變更，不覆蓋使用者工作。每次只做一個可驗證任務。
- 完成後列出修改檔案、目的、Inspector 設定、驗證結果與未完成事項；更新 docs/PROJECT_STATUS.md。
- 需要與外部工具串接時，另設該工具的薄層設定，引用本檔，避免複製規則造成不同版本。AGENTS.md 是共用約定，不保證每個 Agent 自動讀取。

## 授權與教學模型

- 自有內容採 MIT；第三方素材保留原授權，維護 THIRD_PARTY_NOTICES.md。
- Robot.fbx 是未擷取的課堂示範；除非任務要求，不預先擷取材質／貼圖。內嵌交付規則不因教學而取消。

## AR／Vuforia 套件：Git LFS 強制規則

- 所有 `*.tgz` 套件以 Git LFS 追蹤；共用 `.gitattributes` 必須包含 `*.tgz filter=lfs diff=lfs merge=lfs -text`，並與套件一併提交。不得將 `.tgz` 加入 `.gitignore`。
- 目前指定路徑為 `Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz`。保留使用者指定的路徑與版本，不自行移動、解壓、升級或重新下載。
- 每台開發電腦先安裝 Git LFS，並在專案根目錄執行 `git lfs install`。首次設定依序執行：

```bash
git lfs install
git lfs track "*.tgz"
git add .gitattributes
git add "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
git check-attr filter diff merge text -- "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
git lfs ls-files
```

- 檢查屬性應為 `filter: lfs`、`diff: lfs`、`merge: lfs`、`text: unset`；`git lfs ls-files` 應列出該套件。由 Unity 產生的 `.meta` 另行一併提交，不能以 LFS 追蹤 `.meta` 或手寫它。
- 若套件已在暫存區／目前版本以普通 Git 檔案追蹤，設定規則後執行 `git add --renormalize -- "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"`，再檢查。這只更新目前提交，不能清除歷史的大檔；不得未經授權執行 `git lfs migrate import`、改寫歷史或強制推送。
- 後續 clone／pull 後執行 `git lfs pull`，確認取得實際套件而非 LFS 指標文字，再開啟 Unity。提交後正常 push，確保 LFS 上傳成功；失敗時不可宣稱交付完成。
- 此補充資料包只預設追蹤規則與操作文件，不附 Vuforia 套件，也不預建 `Assets/Editor/Migration`。套件匯入、AR 裝置執行與 Unity 相容性須另行驗證。
- Vuforia 是第三方套件，其授權不由本範本 MIT 授權涵蓋。公開範本收錄套件前須確認原授權允許再散布，並更新 `THIRD_PARTY_NOTICES.md`。

- TGZ／LFS 異常先依 docs/09_TGZ與GitLFS_QA.md 分類與驗證，不以歷史遷移作通用修復。

## 範本版本與範圍

- 內容版本讀 VERSION；開始前讀 docs/ROADMAP.md 與 docs/VERSIONING.md。版本變更同步更新 CHANGELOG。
- CreateFolder 工具暫不合併；不得將其原始碼、安裝依賴或工具目錄加入本範本，除非使用者另行要求。
- 模型採本版每模型一資料夾規範；不得自動改動既有資產路徑或預先擷取課堂模型。
