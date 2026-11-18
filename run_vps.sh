#!/bin/bash
# Script khoi dong Login Server & Game Server NRO tren VPS Linux

DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "============================================="
echo "Dang kiem tra va dung cac tien trinh cu..."
echo "============================================="
pkill -f "2D_OBITO-1.0-RELEASE" 2>/dev/null || true
pkill -f "ServerLogin-1.0-RELEASE" 2>/dev/null || true
sleep 2

# 1. Khoi dong Login Server (Port 8888)
echo "[1/2] Khoi dong Login Server (Port 8888)..."
cd "$DIR/_Login"
if command -v screen >/dev/null 2>&1; then
    screen -wipe 2>/dev/null || true
    screen -dmS nro_login bash -c "java -Dfile.encoding=UTF-8 -server -jar target/ServerLogin-1.0-RELEASE-jar-with-dependencies.jar > log_login.txt 2>&1"
    echo "-> Login Server da chay trong screen: nro_login"
else
    nohup java -Dfile.encoding=UTF-8 -server -jar target/ServerLogin-1.0-RELEASE-jar-with-dependencies.jar > log_login.txt 2>&1 &
    echo "-> Login Server da chay bang nohup (PID: $!)"
fi

sleep 3

# 2. Khoi dong Game Server (Port 14445)
echo "[2/2] Khoi dong Game Server (Port 14445)..."
cd "$DIR/Soucre"
if command -v screen >/dev/null 2>&1; then
    screen -dmS nro_game bash -c "java -Dfile.encoding=UTF-8 -server -jar target/2D_OBITO-1.0-RELEASE-jar-with-dependencies.jar > log_server.txt 2>&1"
    echo "-> Game Server da chay trong screen: nro_game"
else
    nohup java -Dfile.encoding=UTF-8 -server -jar target/2D_OBITO-1.0-RELEASE-jar-with-dependencies.jar > log_server.txt 2>&1 &
    echo "-> Game Server da chay bang nohup (PID: $!)"
fi

echo "============================================="
echo "Server NRO da duoc khoi dong thanh cong!"
echo "- Xem log Game:  tail -f $DIR/Soucre/log_server.txt"
echo "- Xem log Login: tail -f $DIR/_Login/log_login.txt"
if command -v screen >/dev/null 2>&1; then
    echo "- Vao console truc tiep: screen -r nro_game (nhan Ctrl+A roi D de thoat ra)"
fi
echo "============================================="
