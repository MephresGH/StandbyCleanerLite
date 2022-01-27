@echo off
cd /d "%~dp0.."
IF EXIST "%~dp0..\bin" echo WARNING: directory found. Starting assembly... && GOTO :ASSEMBLY (
) ELSE (
GOTO :FIRSTASSEMBLY
)

:FIRSTASSEMBLY
mkdir "%~dp0..\bin"
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /t:exe /out:"Standby Cleaner Lite.exe" "Program.cs" "AssemblyInfo.cs" /platform:x86 /win32icon:"StandbyCleanerLite.ico"
move "Standby Cleaner Lite.exe" "bin"
explorer "%~dp0..\bin"
pause
taskkill /f /im cmd.exe

:ASSEMBLY
echo.
del "Standby Cleaner Lite\Standby Cleaner Lite.exe" >NUL 2>&1
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /t:exe /out:"Standby Cleaner Lite.exe" "Program.cs" "AssemblyInfo.cs" /platform:x86 /win32icon:"StandbyCleanerLite.ico"
move "Standby Cleaner Lite.exe" "bin"
explorer "%~dp0..\bin"
pause
taskkill /f /im cmd.exe