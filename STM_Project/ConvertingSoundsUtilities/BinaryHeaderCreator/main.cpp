#include <iostream>
#include <sstream>
#include <fstream>

using namespace std;

int main() {
	ifstream inputFile("SOUND2.raw", ios::binary);
	ofstream outputFile("output3.h", ios::trunc | ios::binary);
	if (inputFile.good() && inputFile.is_open()) {
		if (outputFile.good() && outputFile.is_open()) {
			cout << "We're home :>" << endl;
		}
		else {
			cout << "shit..." << endl;
			cin.get();
			return 1;
		}
	}
	else {
		cout << "shit..." << endl;
		cin.get();
		return 1;
	}
	int counter = 0;
	int8_t buffer;
	int8_t samples[100000];
	while (inputFile.good() && inputFile.is_open()) {
		stringstream ss;

		inputFile >> buffer;
		ss << hex << buffer;
		string str(ss.str());

		samples[counter] = buffer;
		counter++;
	}
	outputFile << samples;

	cout << "Counted samples: " << counter << endl;
	cin.get();

	outputFile.close();
	inputFile.close();
}