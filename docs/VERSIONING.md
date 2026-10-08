# 版本與發布政策

版本唯一基準為根目錄 VERSION，首版 0.1.0。README、CHANGELOG 與正式發布標籤須一致；版本號不代表 Editor 驗證已完成。

- PATCH：修正文件與規範錯誤且不改變資料夾契約。
- MINOR：加入教學範例、相容功能或規範擴充；0.x 期間有破壞性改動須特別標記與提供移轉說明。
- MAJOR：1.0 之後更改既有資產路徑／操作契約或移除支援。

main 保存審查後內容，feature/* 與 experiment/* 處理修改。討論用 Issues／Discussions，結論寫入 docs；機密資料另存私有 repo。

發布流程：完成里程碑 → 更新 VERSION 與 CHANGELOG → 記錄靜態與 Editor 驗證 → 合併 main → 建立 vX.Y.Z tag → Release 附驗收紀錄與分發 ZIP。首版目前只有內容版本，未宣稱已建立 tag／Release。正式課堂發布前測試完整解壓、Agent 讀規則與 Unity 編譯。

分發 ZIP 不包含 .git、Unity 快取與建置輸出；保留點開頭的共用規則檔。套件檢查不能只看副檔名；不得把 LFS 指標當作實體 TGZ 打包。
