namespace STMInputDLL {
	public partial class STMInput {
		/// <summary>
		/// Get direction of horizontal move
		/// </summary>
		/// <returns> -1 when down, 1 when up, 0 when object stay </returns>
		public float GetAxisY() {
			if (this[0] == 'W') return 1f;
			else if (this[1] == 'S') return -1f;
			else return 0;
		}
		/// <summary>
		/// Get direction of vertical move
		/// </summary>
		/// <returns> -1 when left, 1 when right, 0 when object stay </returns>
		public float GetAxisX() {
			if (this[2] == 'A') return -1f;
			else if (this[3] == 'D') return 1f;
			else return 0;
		}
		/// <summary>
		/// Get info about button status
		/// </summary>
		/// <returns> 1 when pressed, 0 when not pressed </returns>
		public bool GetButton() {
			if (this[4] == 'B') {
				RunSound("dead");
				return true;
			}
			else return false;
		}
		/// <summary>
		/// Run sound from list (explode or dead) depend on param
		/// </summary>
		/// <param name="sound">explode or dead string</param>
		public void RunSound(string sound) {
			switch (sound) {
				case "shoot": _output[0] = '0'; break;
				case "explode": _output[0] = '1'; break;
				case "dead": _output[0] = '2'; break;
			}
		}
	}
}
