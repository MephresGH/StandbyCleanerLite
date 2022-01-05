@echo off
cd /d %~dp0
mkdir bin
c:\windows\Microsoft.NET\Framework\v3.5\csc.exe /t:exe /out:"Standby Cleaner Lite.exe" "Program.cs" "AssemblyInfo.cs" /win32icon:"StandbyCleanerLite.ico"
move "Standby Cleaner Lite.exe" "bin"
explorer "%~dp0bin"
pause