echo off
cd %~dp0
cd ..
echo on

Core Doc -output:"Event assembler language.txt" -docHeader:%1 -docFooter:%2

pause