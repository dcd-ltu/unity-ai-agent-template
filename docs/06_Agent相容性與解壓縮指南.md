# Agent 相容性與解壓縮指南

本版以 AGENTS.md 作為共用規範，其他入口只匯入或要求讀取共用規範。沒有要求安裝全部工具；同學選一個即可。此處為規則檔格式核對，尚未在四種工具各自實機驗證。

| 工具 | 本範本入口 | 注意事項 |
| --- | --- | --- |
| Codex Desktop / Codex | 根目錄 AGENTS.md | 將整個專案根目錄加入工具；不要只開 Scripts 資料夾。 |
| Kiro | AGENTS.md、.kiro/steering/unity-project.md | steering 使用 inclusion: always；自訂 Agent 若覆寫資源載入，須確認包含共用規範。 |
| Antigravity IDE / CLI | AGENTS.md、.agents/rules/unity-project.md | 使用 trigger: always_on；不使用錯誤的 alwaysOn。較舊工具若未載入，貼啟動提示詞。 |
| Claude Code | CLAUDE.md 匯入 @AGENTS.md | 使用一般文字檔，適合 Windows 解壓縮，沒有符號連結需求。 |

## 同學解壓縮後怎麼做

1. 解壓縮完整 ZIP；確認根目錄 README.md、AGENTS.md、CLAUDE.md、.gitignore 與 .gitattributes 都存在，並保留 .agents、.kiro、.github。
2. **此包仍是骨架**：第一次需按 docs/02_初始化與InputSystem.md，用 Unity Hub 建立 6000.3.17f1 專案，再合併骨架；本包完全不包含 Packages 與 ProjectSettings，使用 Unity 建立的既有目錄；不可把補充資料包當成完整專案。
3. 如果老師希望學生「解壓縮就能直接開 Unity 玩」，老師先完成初始化、加入 Main 場景與 Move 示範，驗證後再重新打包完整專案，保留所有 .meta。
4. 在 Agent 中開啟專案根目錄，首次貼上 docs/05_AI啟動提示詞.md。
5. 請 Agent 回報讀到的規則檔、Editor 版本、輸入設定與專案現況。回答必須包含「6000.3.17f1、Input System Package (New)、禁止舊 API、繁體中文」。只聲稱讀過不能證明遵循，仍需 review 產出。
6. 新增一個小任務，確認 Agent 能正確找到資料夾與說明 Inspector 設定後，再進行功能開發。

## .gitignore 已內附

忽略 Unity 快取、建置輸出、個人 IDE 設定、個人 AI 設定與憑證；保留 Unity 資產 .meta、Packages、ProjectSettings、共用 AI 規則與教學文件。共享規則不應被個人的全域 gitignore 排除，提交前檢查。

.gitignore 只管 Git 是否追蹤，並不能阻止 AI 讀取或讓命令自動取得權限。若檔案已被 Git 追蹤，追加 ignore 不會自動解除追蹤。

## 官方參考（2026-10-02 核對）

- Codex：https://developers.openai.com/codex/guides/agents-md
- Kiro：https://kiro.dev/docs/steering/
- Antigravity：https://www.antigravity.google/docs/rules/
- Claude Code：https://code.claude.com/docs/en/memory

工具版本或企業政策可能影響載入。出現規則未讀取時使用啟動提示詞；不要在未確認前聲稱任何版本皆已支援。

