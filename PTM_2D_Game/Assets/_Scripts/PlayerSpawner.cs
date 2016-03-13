using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public Transform playerPrefab;
	private float timer;

	void OnEnable () {
		timer = 3;
		if(timer > 0) {
			timer -= Time.deltaTime;
		}
		Vector3 pos = GetComponent<Transform>().position;
		Quaternion quat = GetComponent<Transform>().rotation;
		Instantiate(playerPrefab, pos, quat);
		gameObject.SetActive(false);
	}
}
