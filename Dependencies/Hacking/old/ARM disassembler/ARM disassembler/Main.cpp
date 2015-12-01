#pragma once
#include <string>
#include <iostream>
#include <fstream>
#include <stdlib.h>
#include <sys\types.h> 
#include <sys\stat.h> 
#include "armdis.h"

using namespace std;

int main(int argc, char** args)
{
	string IFile;
	int amount;
	int offset;
	if(argc > 3)
	{
		offset = atoi(args[1]);
		amount = atoi(args[2]);
		string *temp = new string(args[0]);
		IFile = *temp;
	}
	else
	{
		cout << "Write the file you wish to disassemble:";
		cin >> IFile;
		cout << "And offset:";
		cin >> offset;
		cout << "And the amount of opcodes:";
		cin >> amount;
	}

	disassmebler *dis = new disassmebler(IFile.c_str());
	string* output = new string(IFile.c_str());
	*output += ".txt";
	fstream oFile;
	oFile.open(output->c_str());
	if(oFile.fail() || !oFile.is_open())
		return 1;
	char* buffer = new char[15];
	for(int i = 0; i < amount; i++)
	{
		dis->disThumb(offset, buffer, 0);
		cout << buffer << endl;
		//oFile.write(buffer, 20);
		offset += 2;
	}
	oFile.close();
}