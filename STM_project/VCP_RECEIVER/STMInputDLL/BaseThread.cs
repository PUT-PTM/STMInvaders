using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STMInputDLL {
	public abstract class BaseThread {
		private Thread thread;

		public BaseThread() {
			thread = new Thread(Run);
		}

		public void Start() {
			thread.Start();
		}
		public void Join() {
			thread.Join();
		}

		protected abstract void Run();
	}
}
