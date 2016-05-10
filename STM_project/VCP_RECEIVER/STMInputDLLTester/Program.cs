using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using STMInputDLL;
using System.Diagnostics;

namespace STMInputDLLTester {
	class Program {
		static void Main(string[] args) {
			STMInput STM_Input = new STMInput();
			new Thread(STM_Input.Run).Start();
			int i = 0;

			Stopwatch t = new Stopwatch();
			t.Start();
			while (true) {
				if (STM_Input.GetButton()) {
					STM_Input.RunSound("shoot");
				}
				//Thread.Sleep(100);
				//Console.WriteLine(STM_Input.Input + " " + i++);
				/*if(t.ElapsedMilliseconds > 1000) {
					STM_Input.RunSound("explode");
					t.Restart();
				}*/
			}
		}
	}
}
