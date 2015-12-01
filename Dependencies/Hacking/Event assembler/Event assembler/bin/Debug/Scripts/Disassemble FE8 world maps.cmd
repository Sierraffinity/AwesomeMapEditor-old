echo off
cd %~dp0
cd ..
echo on

Core D FE8 ToEnd 0xA39768 none -input:%1 "-output:%~dp1Prologue.txt" -addEndGuards
Core D FE8 ToEnd 0xA39D0C none -input:%1 "-output:%~dp1Ch1.txt" -addEndGuards
Core D FE8 ToEnd 0xA39D44 none -input:%1 "-output:%~dp1Ch2.txt" -addEndGuards
Core D FE8 ToEnd 0xA39F20 none -input:%1 "-output:%~dp1Ch3.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A0BC none -input:%1 "-output:%~dp1Ch4.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A1EC none -input:%1 "-output:%~dp1Ch5.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A4D8 none -input:%1 "-output:%~dp1Ch6.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A5C4 none -input:%1 "-output:%~dp1Ch7.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A6B0 none -input:%1 "-output:%~dp1Ch8.txt" -addEndGuards

Core D FE8 ToEnd 0xA3A730 none -input:%1 "-output:%~dp1Ch9Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3A990 none -input:%1 "-output:%~dp1Ch10Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3AB50 none -input:%1 "-output:%~dp1Ch12Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3AB6C none -input:%1 "-output:%~dp1Ch13Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3ACB0 none -input:%1 "-output:%~dp1Ch14Eir.txt" -addEndGuards

Core D FE8 ToEnd 0xA3AE58 none -input:%1 "-output:%~dp1Ch9Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B08C none -input:%1 "-output:%~dp1Ch10Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B1D8 none -input:%1 "-output:%~dp1Ch12Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B1F4 none -input:%1 "-output:%~dp1Ch13Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B2DC none -input:%1 "-output:%~dp1Ch14Eph.txt" -addEndGuards

Core D FE8 ToEnd 0xA3B528 none -input:%1 "-output:%~dp1Ch15Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B594 none -input:%1 "-output:%~dp1Ch16Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B738 none -input:%1 "-output:%~dp1Ch17Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3B8E8 none -input:%1 "-output:%~dp1Ch18Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3BA64 none -input:%1 "-output:%~dp1Ch19Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3BB74 none -input:%1 "-output:%~dp1Ch20Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3BD58 none -input:%1 "-output:%~dp1Final1Eir.txt" -addEndGuards

Core D FE8 ToEnd 0xA3BD74 none -input:%1 "-output:%~dp1Ch15Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3BF28 none -input:%1 "-output:%~dp1Ch16Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C0B4 none -input:%1 "-output:%~dp1Ch17Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C260 none -input:%1 "-output:%~dp1Ch18Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C3DC none -input:%1 "-output:%~dp1Ch19Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C4EC none -input:%1 "-output:%~dp1Ch20Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C6D0 none -input:%1 "-output:%~dp1Final1Eph.txt" -addEndGuards

Core D FE8 ToEnd 0xA3C860 none -input:%1 "-output:%~dp1LordsSplit.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C890 none -input:%1 "-output:%~dp1Ch5x.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C898 none -input:%1 "-output:%~dp1Final2Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C8A0 none -input:%1 "-output:%~dp1Final2Eph.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C8A8 none -input:%1 "-output:%~dp1Ch11Eir.txt" -addEndGuards
Core D FE8 ToEnd 0xA3C9D0 none -input:%1 "-output:%~dp1Ch11Eph.txt" -addEndGuards

pause