using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
	class Program {
		static void Main(string[] args) {
			foreach(var x in SerialPort.GetPortNames()) {
				Console.WriteLine(x);
			}
			Console.Read();
		}
	}
}
