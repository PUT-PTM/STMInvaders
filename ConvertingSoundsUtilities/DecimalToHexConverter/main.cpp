#include <iostream>
#include <sstream>
#include <fstream>

using namespace std;

int main() {
	ifstream inputFile("SOUND_super_hiper.raw", ios::binary);
	ofstream outputFile("output2.h", ios::trunc);
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
	int8_t val;
	while (inputFile.good() && inputFile.is_open()) {
		inputFile >> val;
		stringstream ss;
		//ss << hex << val;
		ss << hex << (int16_t)val;
		counter++;

		string result(ss.str());
		//if (counter % 5 != 0) cout << result << "\t";
		//else cout << result << endl;

		if (counter % 10 != 0) outputFile << "0x" + result << ",";
		else outputFile << "0x" + result << "," << endl;
	}
	cout << endl << "Counted samples: " << counter << endl;
	outputFile << endl << "Counted samples: " << counter << endl;
	cin.get();

	outputFile.close();
	inputFile.close();
}