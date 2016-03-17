using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public Transform player;
	public float timer;

	void Update() {
		if (!GlobalStatics.PLAYER_IS_ALIVE) {
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				GlobalStatics.PLAYER_IS_ALIVE = true;

				player.position = GetComponent<Transform>().position;
				player.gameObject.SetActive(true);

				timer = 3;
			}
		}
	}
}
