#include <iostream>
#include <sstream>
#include <fstream>

using namespace std;

struct File
{
	int number1;
	float number2;
};

int main()
{
	ofstream outf("out.bin", ios::binary);
	char tab[10];
	for (size_t i = 0; i < 10; i++)	{
		tab[i] = 1;
	}
	for (int i = 0; i < 10; i++) {
		outf.write((char*)(&tab[i]), sizeof(char));
	}
}