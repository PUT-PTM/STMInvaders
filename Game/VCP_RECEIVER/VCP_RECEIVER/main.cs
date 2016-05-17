using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace VCP_RECEIVER {
	public partial class VCP {
		static char[] STM_Input = new char[5];
		static SerialPort _serialPort;

		public static void Main() {
			StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
			Thread readThread = new Thread(Run);

			// Set serial port values
			InitSerialPort();

			// Run port I/O operating
			readThread.Start();

			// Wait for end thread
			readThread.Join();

			// Close serial port
			_serialPort.Close();
		}

		private static void Run() {
			_serialPort.ReadExisting();
			while (_serialPort.IsOpen) {
				// Reading data from STM
				try {
					for (int i = 0; i < STM_Input.Length; i++) {
						STM_Input[i] = (char)_serialPort.ReadChar();
					}
					Console.WriteLine(STM_Input);
				} catch (TimeoutException) { }
				// OLDTODO Sending data to STM
			}
		}
		private static void InitSerialPort() {
			// Create a new SerialPort object with default settings.
			_serialPort = new SerialPort();

			// Set properties.
			_serialPort.PortName = SetPortName(_serialPort.PortName);
			_serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);
			_serialPort.Parity = SetPortParity(_serialPort.Parity);
			_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
			_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
			_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			// Open port
			_serialPort.Open();
		}
		private static string SetPortName(string defaultPortName) {
			string portName = SerialPort.GetPortNames()[0];
			Console.WriteLine("Port: " + portName);
			return portName;
		}
		private static int SetPortBaudRate(int defaultPortBaudRate) {
			string baudRate = defaultPortBaudRate.ToString();
			Console.WriteLine("Baudrate: " + baudRate);
			return int.Parse(baudRate);
		}
		private static Parity SetPortParity(Parity defaultPortParity) {
			string parity = defaultPortParity.ToString();
			Console.WriteLine("Parity: " + parity);
			return (Parity)Enum.Parse(typeof(Parity), parity, true);
		}
		private static int SetPortDataBits(int defaultPortDataBits) {
			string dataBits = defaultPortDataBits.ToString();
			Console.WriteLine("Databits: " + dataBits);
			return int.Parse(dataBits.ToUpperInvariant());
		}
		private static StopBits SetPortStopBits(StopBits defaultPortStopBits) {
			string stopBits = defaultPortStopBits.ToString();
			Console.WriteLine("stopbits: " + stopBits);
			return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
		}
		private static Handshake SetPortHandshake(Handshake defaultPortHandshake) {
			string handshake = defaultPortHandshake.ToString();
			Console.WriteLine("Handshake: " + handshake);
			return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
		}
	}
}