using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace STMInputDLL {
	class VCPReceiver {
		private SerialPort _serialPort;
		private char[] _input = new char[5];

		public bool InitOK { get; private set; }
		public char this[int index] { get { return _input[index]; } }
		public int this[string sound] {
			set {
				switch (sound) {
					case "explode": // TODO explode 
						break;
					case "dead": // TODO dead
						break;
				}
			}
		}

		public VCPReceiver() {
			InitOK = InitSerialPort();
		}
		/// <summary>
		/// Set initial values for SerialPort.
		/// </summary>
		/// <returns> False if there is no VCOM port connection </returns>
		private bool InitSerialPort() {
			// Create a new SerialPort object with default settings.
			_serialPort = new SerialPort();

			// Set properties (only name could be problem, other values set to default)
			try {
				_serialPort.PortName = SerialPort.GetPortNames()[0];
				//_serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);
				//_serialPort.Parity = SetPortParity(_serialPort.Parity);
				//_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
				//_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
				//_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);
			} catch (ArgumentNullException) {
				return false;
			}

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			// Open port
			// _serialPort.Open();

			//everything is ok
			return true;
		}
	}
}
