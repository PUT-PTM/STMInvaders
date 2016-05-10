using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStatusController : MonoBehaviour {
	public Text gameOverText;
	public Transform player;
	public EnemySpawnerBehaviour spawner;

	void Start () {
		if (!gameOverText) {
			Debug.LogError("GameOverText not attached!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!player) {
			Debug.LogError("Player transform not attached!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!spawner) {
			Debug.LogError("Enemy spawner transform not attached!!!");
			gameObject.SetActive(false);
			return;
		}
	}
	// Check if player lost every life
	void LateUpdate () {
		if(Statics.PLAYER_LIFES == 0 && !Statics.PLAYER_IS_ALIVE) {
			gameOverText.gameObject.SetActive(true);
		}
		// Just for testing
		if (Input.GetKeyDown(KeyCode.K)) {
			Statics.PLAYER_LIFES = 1;
			player.gameObject.SetActive(false);
		}
		// Check for escape
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		// CZITYYYY
		if (Input.GetKeyDown(KeyCode.L)) {
			spawner.ClearList();
		}
	}
}
