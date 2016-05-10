using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReloadBar : MonoBehaviour {
	#region Variables
	public Transform greenBar;
	public PlayerShootingVCP player;
	public Text text;
	public float actualReload;
	public int bullets;

	private float reloadMAX;
	private bool justFlag = false;
	#endregion
	#region Start & LateUpdate
	// Use this for initialization
	void Start () {
		reloadMAX = player.reload;
	}

	// Update is called once per frame
	void LateUpdate() {
		if (!justFlag) {
			reloadMAX = player.reload;
			justFlag = true;
		}
		actualReload = player.reload;
		bullets = player.magazine;

		// change bar
		greenBar.localScale = new Vector3(
			0.4f * CountPercent(),
			greenBar.localScale.y,
			greenBar.localScale.z
		);
		// change text
		if (bullets > 0) {
			text.text = " Bullets: " + bullets;
		}
		else text.text = " Reloading...";
	}
	#endregion
	#region Other methods
	private float CountPercent() {
		return (actualReload / reloadMAX);
	}
	#endregion
}
