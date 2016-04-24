using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMInputDLL {
	public class STMInput {
		private VCPReceiver STM_Input;

		public STMInput() {
			STM_Input = new VCPReceiver();
		}
		/// <summary>
		/// Run STM_Input when everything is okey. If not, VCP thread will not run.
		/// </summary>
		/// <returns> True if everything is okey, false elsewhere </returns>
		public bool VCPRun() {
			if (STM_Input) {
				STM_Input.Start();
				STM_Input.Join();
				return true;
			} else return false;
		}
		/// <summary>
		/// Get direction of horizontal move
		/// </summary>
		/// <returns> -1 when left, 1 when right, 0 when object stay </returns>
		public int GetAxisX() {
			if (STM_Input[0] == 'A') return -1;
			else if (STM_Input[1] == 'D') return 1;
			else return 0;
		}
		/// <summary>
		/// Get direction of vertical move
		/// </summary>
		/// <returns> -1 when down, 1 when up, 0 when object stay </returns>
		public int GetAxisY() {
			if (STM_Input[2] == 'W') return 1;
			else if (STM_Input[3] == 'S') return -1;
			else return 0;
		}
		/// <summary>
		/// Get info about button status
		/// </summary>
		/// <returns> 1 when pressed, 0 when not pressed </returns>
		public bool GetButton() {
			if (STM_Input[4] == 'B') return true;
			else return false;
		}
		/// <summary>
		/// Run sound from list (explode or dead) depend on param
		/// </summary>
		/// <param name="sound">explode or dead string</param>
		public void RunSound(string sound) {
			switch (sound) {
				case "explode": STM_Input["explode"] = '1'; break;
				case "dead": STM_Input["dead"] = '1'; break;
			}
		}
	}
}
