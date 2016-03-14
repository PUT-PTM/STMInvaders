using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public Transform playerPosition;
	private float timer;

	void Awake() {
		GlobalStatics.PLAYER_LIFES++;
	}

	void OnEnable () {
		timer = 3;
		if(timer > 0) {
			timer -= Time.deltaTime;
		}
		GlobalStatics.PLAYER_IS_ALIVE = true;
		GlobalStatics.PLAYER_LIFES--;
		
		playerPosition.position = GetComponent<Transform>().position;
		playerPosition.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}
}
