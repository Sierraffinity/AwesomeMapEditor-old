echo off
cd %~dp0
echo on

DSTextFileEditor combine %1 "%~dpn1.bin"

pause
