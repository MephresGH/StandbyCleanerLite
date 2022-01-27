@echo off
GOTO :CHECKPERMS

:CHECKPERMS
    echo Administrative Permissions Required. Detecting Permissions...

    NET SESSION >nul 2>&1
    IF %errorLevel% == 0 (
        echo Success: Administrative Permissions Confirmed. Continuing As Usual... && GOTO :PROMPT
    ) ELSE (
        echo Current Permissions Inadequate. Requesting Elevated Rights... && (powershell start -verb runas '%0' am_admin & exit /b)
    )

:PROMPT
SCHTASKS /END /TN "Standby Cleaner Lite" >NUL 2>&1
SCHTASKS /DELETE /TN "Standby Cleaner Lite" /F >NUL 2>&1
pause
taskkill /f /im cmd.exe