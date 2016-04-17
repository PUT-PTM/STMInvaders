#include <iostream>
#include <sstream>
#include <fstream>

using namespace std;

int main() {
	ifstream inputFile("SOUND_super_hiper.raw", ios::binary);
	ofstream outputFile("output.h", ios::trunc | ios::binary);
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
	int8_t samples[10538];
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