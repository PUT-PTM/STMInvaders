using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public Transform player;
	public RespawnShield shield;
	public float timeToRespawn;
	public float timer;			

	// Check references for easier debugging
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
	// Spawn player if possible (when there are still some lifes and he's dead)
	// Script deactivating after player respawn
	void Update() {
		if (!Statics.PLAYER_IS_ALIVE && Statics.PLAYER_LIFES > 0) {
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				Statics.PLAYER_IS_ALIVE = true;

				player.position = GetComponent<Transform>().position;
				player.gameObject.SetActive(true);
				shield.ShieldOn();

				timer = timeToRespawn;
			}
		}
	}
}
