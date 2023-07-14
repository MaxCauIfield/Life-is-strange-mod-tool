@echo off
setlocal enabledelayedexpansion
rem 计算包含.UPKpkginfo文件的文件夹的个数
set count=0
for /d %%d in (*) do (
    if exist "%%d\*.UPKpkginfo" (
        set /a count+=1
    )
)
echo 一共有%count%个包含.UPKpkginfo文件的文件夹

rem 依次用woc.exe打开包含.UPKpkginfo文件的文件夹
for /d %%d in (*) do (
    if exist "%%d\*.UPKpkginfo" (
        echo 正在打开%%d
        start "" "LisrepackServer" "%%d"
        rem 循环判断woc.exe是否关闭
        :loop
        tasklist | find /i "woc.exe" >nul
        if errorlevel 1 (
            echo woc.exe已关闭
        ) else (
            echo woc.exe仍在运行，等待5秒
            timeout /t 5 /nobreak >nul
            goto :loop
        )
    )
)
echo 所有的包含.UPKpkginfo文件的文件夹都已执行完毕，程序结束
exit