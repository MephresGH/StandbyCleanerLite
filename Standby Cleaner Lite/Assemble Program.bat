@echo off
cd /d %~dp0
c:\windows\Microsoft.NET\Framework\v3.5\csc.exe /t:exe /out:"Standby Cleaner Lite.exe" "Program.cs" "AssemblyInfo.cs" /win32icon:"StandbyCleanerLite.ico"
pause