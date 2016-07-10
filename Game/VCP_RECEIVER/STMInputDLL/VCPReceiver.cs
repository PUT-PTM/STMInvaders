using System;
using System.IO.Ports;

namespace STMInputDLL {
	public partial class STMInput : IDisposable {
		#region Class Variables
		private static SerialPort _serialPort;
		private char[] _input = { '_', '_', '_', '_', '_' };
		private char[] _output = { '_' };
		private bool initOK;
		private static string error = "NO ERROR DEFINED - maybe wrong initialization?";
		public string Input {
			get {
				string val = "";
				for (int i = 0; i < _input.Length; i++) val += _input[i];
				return val;
			}
		}
		#endregion
		#region Bool-operator & indexer
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
		#endregion
		#region SerialPort Initiaization
		/// <summary>
		/// Constructor - initialize VCP and check if everything is okey
		/// </summary>
		public STMInput(string port = "") {
			initOK = InitSerialPort(port);
		}
		/// <summary>
		/// Set initial values for SerialPort.
		/// </summary>
		/// <returns> False if there is no VCOM port connection </returns>
		private bool InitSerialPort(string port) {
			// Create a new SerialPort object with default settings.
			if (port.Length > 0) {
				if (port.ToLower().StartsWith("com")) {
					_serialPort = new SerialPort(port);
				}
				else {
					error = "Wrong VCP name";
					return false;
				}
			} else {
				error = "No VCP name added - running default mode (keyboard play)";
				return false;
			}

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			//everything is ok
			return true;
		}
		#endregion
		#region Major methods
		/// <summary>
		/// Main method of VCP - need to be run in thread to make everything works fine
		/// </summary>
		public void Run() {
			// Open port
			_serialPort.Open();
			if (initOK) {
				// Synchronize with STM
				while ((char)_serialPort.ReadChar() != 'X') ;
				// Main loop
				while (_serialPort.IsOpen) ReadData();
			}
		}
		private void ReadData() {
			try {
				// Reading data from STM
				for (int i = 0; i < _input.Length; i++) {
					_input[i] = (char)_serialPort.ReadChar();
				}
				_serialPort.ReadChar(); // Read end-char										
			} catch (TimeoutException) { }
			try {
				// Sending data to STM
				_serialPort.Write(_output, 0, 1);
				_output[0] = '_';
			} catch (TimeoutException) { }
		}
		// Read error message
		public static string GetErrorMessage() {
			return error;
		}
		#endregion
		#region IDisposable Support
		private bool disposed = false; // To detect redundant calls
									   // Overriden Dispose method
		protected virtual void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing) {
					// Dispose managed objects
					_serialPort.Close();
				}
				// Free unmanaged objects
				this._output = null;
				this._input = null;

				disposed = true;
			}
		}
		// Method needed to implement by IDisposable interface
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}