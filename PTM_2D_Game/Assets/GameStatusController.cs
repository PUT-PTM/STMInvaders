using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStatusController : MonoBehaviour {
	public Text GameOverText;
	public Transform Player;
	// Use this for initialization
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
	
	// Update is called once per frame
	void LateUpdate () {
		if(Statics.PLAYER_LIFES == 0 && !Statics.PLAYER_IS_ALIVE) {
			GameOverText.gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			Statics.PLAYER_LIFES -= 2;
			Player.gameObject.SetActive(false);
		}
	}
}
