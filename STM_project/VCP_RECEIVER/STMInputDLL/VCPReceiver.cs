using System;
using System.IO.Ports;

namespace STMInputDLL {
	public partial class STMInput {
		private SerialPort _serialPort;
		public char[] _input = { '_', '_', '_', '_', '_' };
		public char[] _output = { '_', '\n' };
		private bool initOK;
		
		public void ClosePort() {
			_serialPort.Close();
		}
		/// <summary>
		/// Constructor - initialize VCP and check if everything is okey
		/// </summary>
		public STMInput() {
			initOK = InitSerialPort();
		}
		/// <summary>
		/// Special indexer to encapsulate acces for _input table (movement & shoot)
		/// </summary>
		/// <param name="index"> _input table index </param>
		/// <returns></returns>
		private char this[int index] { get { return _input[index]; } }
		/// <summary>
		/// Additional operator for simplier check if VCP work fine outside the class
		/// Now it's possible check status "as-is" in if-statement [if(VCP) { ... }]
		/// or via equals operator [if(VCP == true/false){ ... }])
		/// </summary>
		public static implicit operator bool(STMInput myClass) {
			return myClass.initOK;
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
			} catch (IndexOutOfRangeException) {
				return false;
			}

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;
			
			//everything is ok
			return true;
		}
		/// <summary>
		/// Main method of VCP - need to be run in thread to make everything works fine
		/// </summary>
		public void Run() {
			// Open port
			_serialPort.Open();
			if (initOK) {
				// Synchronize with STM
				char end = 'X';
				while (end != (char)_serialPort.ReadChar()) ;
				while (_serialPort.IsOpen) {
					try {
						// Reading data from STM
						for (int i = 0; i < _input.Length; i++) {
							_input[i] = (char)_serialPort.ReadChar();
						}
						_serialPort.ReadChar();
						// TODO Sending data to STM
						_serialPort.Write(_output, 0, 1);
						_output[0] = '_';
					} catch (TimeoutException) { }
				}
			}
		}
	}
}
