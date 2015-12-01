#include <string>
#include <iostream>

using namespace std;

void print(int* number)
{
	cout << *number;
}

int main()
{
	int number;
	__asm
	{
		mov number, 4
	}
	print(&number);
}