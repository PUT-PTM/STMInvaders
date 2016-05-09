using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using STMInputDLL;

namespace STMInputDLLTester {
	class Program {
		static void Main(string[] args) {
			STMInput STM_Input = new STMInput();
			new Thread(STM_Input.Run).Start();
			int i = 0;

			while (true) {
				//Thread.Sleep(100);
				Console.WriteLine(STM_Input.Input + " " + i++);
			}
		}
	}
}
