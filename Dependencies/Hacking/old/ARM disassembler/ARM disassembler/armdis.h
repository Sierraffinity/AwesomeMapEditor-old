#pragma once
// -*- C++ -*-
// VisualBoyAdvance - Nintendo Gameboy/GameboyAdvance (TM) emulator.
// Copyright (C) 1999-2003 Forgotten
// Copyright (C) 2004 Forgotten and the VBA development team

// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2, or(at your option)
// any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software Foundation,
// Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.

/************************************************************************/
/* Arm/Thumb command set disassembler                                   */
/************************************************************************/

#define DIS_VIEW_ADDRESS 1
#define DIS_VIEW_CODE 2

#include <string>
#include <iostream>
#include <fstream>
#include <sys\types.h> 
#include <sys\stat.h> 
#include <stdio.h>

using namespace std;

typedef unsigned int u32;

class disassmebler
{
public:
	disassmebler(const char* ROMpointer);

	int disThumb(u32 offset, char *dest, int flags);
	int disArm(u32 offset, char *dest, int flags);

private:
	char* ROMptr;

	char *elfGetAddressSymbol(u32 addr);
	unsigned char debuggerReadByte(u32 addr);
	unsigned short debuggerReadHalfWord(u32 addr);
	unsigned int debuggerReadMemory(u32 addr);
	char* addStr(char *dest, const char *src);
	char* addHex(char *dest, int siz, u32 val);
};

