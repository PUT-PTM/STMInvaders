using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStatusController : MonoBehaviour {
	public Text GameOverText;
	public Transform Player;

	void Start () {
		if (!GameOverText) {
			Debug.LogError("GameOverText not attached!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!Player) {
			Debug.LogError("Player transform not attached!!!");
			gameObject.SetActive(false);
			return;
		}
	}
	// Check if player lost every life
	void LateUpdate () {
		if(Statics.PLAYER_LIFES == 0 && !Statics.PLAYER_IS_ALIVE) {
			GameOverText.gameObject.SetActive(true);
		}
		// Just for testing
		if (Input.GetKeyDown(KeyCode.K)) {
			Statics.PLAYER_LIFES = 1;
			Player.gameObject.SetActive(false);
		}
	}
}
