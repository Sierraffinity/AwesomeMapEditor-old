

FOR %%I IN (*.txt) DO (echo [Spoiler=%%~nI] & type "%%I" & echo [/Spoiler]) >> result.post

pause