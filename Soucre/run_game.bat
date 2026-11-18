@echo off
cd /d "%~dp0"
"C:\Program Files\Java\jdk-17\bin\java.exe" -Dfile.encoding=UTF-8 -jar target/2D_OBITO-1.0-RELEASE-jar-with-dependencies.jar > log_server.txt 2>&1
