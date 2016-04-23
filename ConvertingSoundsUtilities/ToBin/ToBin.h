// ToBin.h
#pragma once

#include <iostream>
#include <string>
#include <bitset>
#include <fstream>

namespace ToBin {
	using namespace std;

	public class Files {
	public:
		ifstream in;
		ofstream out;
		Files(string input, string output) {
			in.open(input, ios::binary);
			out.open(output, ios::trunc);
		}
		~Files() {
			in.close();
			out.close();
		}
	};

	public ref class ToBin {
		Files* f;
	public:
		bool FILES_OPENED;
		unsigned int cnt;

		ToBin(string input, string output) {
			f = new Files(input, output);
			cnt = 0;

			if (f->in.good() && f->in.is_open() && f->out.good() && f->out.is_open()) {
				FILES_OPENED = true;
			}
			else FILES_OPENED = false;
		}
		// Get next value from input file
		// Return false when reach EOF
		bool readNext(char & value) {
			if (f->in.good() && f->in.is_open()) {
				f->in >> value;
				cnt++;
				return true;
			}
			else return false;
		}
		// Put next value to output file
		void putNext(string value) {
			f->out << value << ",";
		}
		// Convert char into set of bits (in string)
		string getBinary(char value) {
			return bitset<8>(value).to_string();
		}
	};
}
