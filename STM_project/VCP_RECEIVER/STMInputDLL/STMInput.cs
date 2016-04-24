using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMInputDLL {
	public class STMInput {
		private VCPReceiver STM_Input;
		public bool InputOK { get; private set; }

		public STMInput() {
			STM_Input = new VCPReceiver();
			if (STM_Input.InitOK == false) {
				InputOK = false;
				return;
			} else {
				InputOK = true;
			}
		}
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
			// TODO sounds UnityGame->VCP
			switch (sound) {
				case "explode": STM_Input["explode"] = 1; break;
				case "dead": STM_Input["dead"] = 1;	break;
			}
		}
	}
}
