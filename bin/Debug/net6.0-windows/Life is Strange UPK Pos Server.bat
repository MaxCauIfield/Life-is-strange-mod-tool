@echo off
setlocal enabledelayedexpansion
rem 计算UPK文件的个数
set count=0
for %%f in (*.upk) do (
    set /a count+=1
)
echo 一共有%count%个UPK文件

rem 依次用woc.exe打开UPK文件
for %%f in (*.upk) do (
    echo 正在打开%%f
    start "" "LisunpackServer.exe" "%%f"
    rem 循环判断woc.exe是否关闭
    :loop
    tasklist | find /i "LisunpackServer.exe" >nul
    if errorlevel 1 (
        echo woc.exe已关闭
    ) else (
        echo woc.exe仍在运行，等待5秒
        timeout /t 5 /nobreak >nul
        goto : loop
    )
)
echo 所有的UPK文件都已执行完毕，程序结束
exit