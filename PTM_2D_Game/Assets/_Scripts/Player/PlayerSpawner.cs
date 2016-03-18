using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public Transform player;
	public float Timer;

	public RespawnShield shield;

	void Start() {
		if (!player) {
			Debug.LogError("No player transform attached!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!shield) {
			Debug.LogError("No shield script attached!!!");
			gameObject.SetActive(false);
			return;
		}
	}

	void Update() {
		if (!Statics.PLAYER_IS_ALIVE && Statics.PLAYER_LIFES > 0) {
			if (Timer > 0) {
				Timer -= Time.deltaTime;
			} else {
				Statics.PLAYER_IS_ALIVE = true;

				player.position = GetComponent<Transform>().position;
				player.gameObject.SetActive(true);
				shield.ShieldOn();

				Timer = 3;
			}
		}
	}
}
