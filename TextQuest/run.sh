#!/bin/bash

echo "Spouštím Ollamu s modelem LLama3.2..."
ollama run llama3.2 &

echo "Čekám 5 sekund na inicializaci modelu..."
sleep 5

echo "Spouštím hru..."
dotnet run --project /mnt/d/Skeleton/text-quest
