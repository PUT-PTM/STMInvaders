using UnityEngine;
using System.Threading;

public partial class STM_Manager : MonoBehaviour {
	private VCPReceiver VCP;

	void Awake() {
		VCP = new VCPReceiver();
		//new Thread(VCP.Run).Start();
		VCP.Run();
	}
	/// <summary>
	/// Close port to awoid IO errors
	/// </summary>
	void OnDestroy() {
		VCP.ClosePort();
	}
	/// <summary>
	/// Additional operator for simplier check if VCP work fine outside the class
	/// Now it's possible check status "as-is" in if-statement [if(VCP) { ... }]
	/// or via equals operator [if(VCP == true/false){ ... }])
	/// </summary>
	public static implicit operator bool(STM_Manager STM) {
		return STM.VCP.initOK;
	}
	/// <summary>
	/// Get direction of vertical move
	/// </summary>
	/// <returns> -1 when down, 1 when up, 0 when object stay </returns>
	public int GetAxisY() {
		if (VCP[0] == 'W') return 1;
		else if (VCP[1] == 'S') return -1;
		else return 0;
	}
	/// <summary>
	/// Get direction of horizontal move
	/// </summary>
	/// <returns> -1 when left, 1 when right, 0 when object stay </returns>
	public int GetAxisX() {
		if (VCP[2] == 'A') return -1;
		else if (VCP[3] == 'D') return 1;
		else return 0;
	}
	/// <summary>
	/// Get info about button status
	/// </summary>
	/// <returns> 1 when pressed, 0 when not pressed </returns>
	/// TODO Improve button (OnPressed & OnReleased)
	public bool GetButton() {
		if (VCP[4] == 'B') return true;
		else return false;
	}
	/// <summary>
	/// Run sound from list (explode or dead) depend on param
	/// </summary>
	/// <param name="sound">explode, shoot or dead string</param>
	public void RunSound(string sound) {
		VCP[sound] = '1';
	}
}
