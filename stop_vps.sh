#!/bin/bash
# Script dung Server NRO tren VPS Linux

echo "Dang dung Game Server va Login Server..."
pkill -f "2D_OBITO-1.0-RELEASE" 2>/dev/null || true
pkill -f "ServerLogin-1.0-RELEASE" 2>/dev/null || true
sleep 1
echo "Da dung tat ca server NRO."
