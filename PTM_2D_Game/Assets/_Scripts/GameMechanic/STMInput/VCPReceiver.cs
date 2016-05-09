using System;
using System.IO.Ports;

public class VCPReceiver {
	private SerialPort _serialPort;
	private char[] _input = { '_', '_', '_', '_', '_' };
	private char[] _output = { '_', '\n' };
	public bool initOK;

	public void ClosePort() {
		_serialPort.Close();
	}
	public VCPReceiver() {
		initOK = InitSerialPort();
	}
	/// <summary>
	/// Special indexer to encapsulate acces for _input table (movement & shoot)
	/// </summary>
	/// <param name="index"> _input table index </param>
	/// <returns></returns>
	public char this[int index] { get { return _input[index]; } }

	public char this[string index] {
		set {
			switch (index) {
				case "shoot": _output[0] = '0'; break;
				case "explode": _output[0] = '1'; break;
				case "dead": _output[0] = '2'; break;
			}
		}
	}
	/// <summary>
	/// Set initial values for SerialPort.
	/// </summary>
	/// <returns> False if there is no VCOM port connection </returns>
	private bool InitSerialPort() {
		// Create a new SerialPort object with default settings.
		_serialPort = new SerialPort();

		// Set properties (only name could be problem, other values set to default)
		// Return false when there is no STM connected
		try {
			_serialPort.PortName = SerialPort.GetPortNames()[0];
		} catch (IndexOutOfRangeException) { return false; }
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
		if (initOK) {
			// Create "DataReceived" event
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(ReadData);

			// Open port
			_serialPort.Open();

			// Synchronize with STM
			char end = 'X';
			while (end != (char)_serialPort.ReadChar()) ;

			//IO operations
			/*while (_serialPort.IsOpen) {
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
			}*/
		}
	}
	private void ReadData(object sender, SerialDataReceivedEventArgs e) {
		SerialPort sp = (SerialPort)sender;
		// Reading data from STM
		for (int i = 0; i < _input.Length; i++) {
			_input[i] = (char)sp.ReadChar();
		}
		sp.ReadChar();
		// Sending data to STM
		sp.Write(_output, 0, 1);
		_output[0] = '_';
	}
}

