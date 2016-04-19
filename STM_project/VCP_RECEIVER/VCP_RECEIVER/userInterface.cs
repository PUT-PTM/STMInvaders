using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCP_RECEIVER {
	public partial class PortChat {
		public int GetAxisX() {
			if (STM_Input[0] == 'A') return -1;
			else if (STM_Input[1] == 'D') return 1;
			else return 0;
 		}
		public int GetAxisY() {
			if (STM_Input[2] == 'W') return 1;
			else if (STM_Input[3] == 'S') return -1;
			else return 0;
		}
		public bool GetButton() {
			if (STM_Input[4] == 'B') return true;
			else return false;
		}
		public void RunSound(string sound) {
			// TODO 
			switch (sound) {
				case "explode": break;
				case "dead": break;
			}
		}
	}
}
