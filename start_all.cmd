@echo off
title NRO Server Launcher
echo ===================================================
echo [1/2] Dang khoi dong Login Server (Port 8888)...
echo ===================================================
cd /d "%~dp0_Login"
start "NRO - Login Server" cmd /k bat.cmd

timeout /t 3 /nobreak >nul

echo ===================================================
echo [2/2] Dang khoi dong Game Server (Port 14445)...
echo ===================================================
cd /d "%~dp0Soucre"
start "NRO - Game Server" cmd /k run.cmd

echo ===================================================
echo Ca 2 server da duoc khoi chay trong 2 cua so rieng!
echo Luu y: Dam bao MySQL tren cong 3306 da duoc bat va da import nro.sql.
echo ===================================================
pause
