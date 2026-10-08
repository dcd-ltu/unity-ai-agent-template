# TGZ／Git LFS 異常 Q&A

## Q1：TGZ 原本已 commit，後來才設定 LFS，是否必須 migrate？

不一定。`git lfs track "*.tgz"` 只設定追蹤規則，不會自動把既有 Git 歷史轉成 LFS。若只需修正目前版本，在保留工作內容、確認 TGZ 為有效套件後執行：

```bash
git lfs install
git lfs track "*.tgz"
git add .gitattributes
git add --renormalize -- "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
git lfs ls-files
git diff --cached --stat
git commit -m "修正 Vuforia TGZ 的 Git LFS 追蹤"
git push
```

只提交本次相關檔案；Unity 產生的對應 `.meta` 也須保留。此方式不會清除舊 commit 的普通 Git 大檔。若遠端因歷史大檔而拒絕 push，才需另行評估歷史遷移。

## Q2：Unity 無法解壓 TGZ，是否表示檔案毀損？

不能直接判定。工作目錄的 TGZ 可能只有 LFS 指標文字，也可能是套件本身不完整。先關閉 Unity，保存本機修改，再檢查：

```bash
git status
git lfs status
git lfs ls-files
git lfs pointer --check --file="Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
```

最後一個命令回傳 0 表示檔案是 LFS 指標，不是可匯入的套件；非 0 也不代表套件一定完整。取得實體內容：

```bash
git lfs install
git lfs pull
git lfs checkout "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
```

`checkout` 使用本機已有的 LFS 物件，不能下載缺少的物件。確認 TGZ 可以列出內容：

```bash
tar -tzf "Assets/Editor/Migration/com.ptc.vuforia.engine-11.4.4.tgz"
git lfs fsck
```

套件應含合理的 Unity package 內容，例如 `package/package.json`。以上通過仍須重新在 Unity 驗證匯入，不能推定 AR 功能通過。

## Q3：何時使用歷史遷移？

當需要將舊 commit 中的 TGZ 一併改成 LFS，例如解決歷史大檔造成的 push 拒絕。先備份整個專案（包含 `.git` 與實體 TGZ），保存未提交工作、確認工作目錄乾淨，再評估：

```bash
git lfs migrate info --everything --include="*.tgz"
```

確認影響範圍與協作者安排後，才執行：

```bash
git lfs migrate import --everything --include="*.tgz"
```

這會檢查所有 refs 可達的歷史，把符合的 TGZ 轉為 LFS 指標，更新本機 refs 並改變相關 commit SHA；它不會自動推送遠端。`--everything` 不等於修復所有分支工作目錄，也不能修復本來就損壞的壓縮檔。

## Q4：migrate 完成後，是否直接強制推送？

先檢查 `git log`、`git lfs ls-files`、`git lfs fsck`，以及實體 TGZ 的內容與 Unity 匯入。若原歷史尚未推送，通常正常 push 即可；若已在遠端，需協調受影響的分支／標籤，再針對指定 ref 更新遠端。不得預設使用 `git push --force --all` 或 `--mirror`。

已發布的歷史遷移可能需要強制推送；應核對預期遠端 SHA 並使用受保護的 lease。協作者先備份自己的工作，再重新 clone，避免把舊歷史合回。一般新增套件不需要 migrate。

## Q5：pull 出現物件不存在、權限或配額錯誤怎麼辦？

保留完整錯誤與 `git lfs logs last` 結果。LFS 指標無法自行重建原始二進位檔；需要套件擁有者從保留實體內容的電腦補上傳，或從合法來源重新取得同版本套件並核對內容。不要刪除 `.git/lfs`、修改指標內容，或反覆 migrate。

## Q6：公開範本或發布 ZIP 要注意什麼？

提交 LFS 規則；每台電腦安裝 Git LFS。發布 ZIP 前檢查是否裝入實體檔案而非指標；GitHub 的原始碼 ZIP 是否包含 LFS 物件取決於儲存庫設定，不能假定都有。Vuforia 套件沿用原授權，不能以本範本的 MIT 代替。

## AI 操作規範

先辨識「追蹤不一致、指標未還原、缺少 LFS 物件、壓縮檔損壞、遠端拒絕歷史大檔」中的哪一種，再選處置。不得僅因 TGZ 異常就執行歷史遷移或強制推送。未取得本機錯誤與驗證結果時，不宣稱原因已確認或問題已修復。

## 官方參考

- https://github.com/git-lfs/git-lfs/blob/main/docs/man/git-lfs-faq.adoc
- https://github.com/git-lfs/git-lfs/blob/main/docs/man/git-lfs-migrate.adoc
- https://github.com/git-lfs/git-lfs/blob/main/docs/man/git-lfs-checkout.adoc
