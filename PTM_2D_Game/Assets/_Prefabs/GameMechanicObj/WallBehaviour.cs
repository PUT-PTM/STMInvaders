using UnityEngine;
using System.Collections;

public class WallBehaviour : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "PlayerBullet": Destroy(trigger.gameObject);
				break;
			case "EnemyBullet": Destroy(trigger.gameObject);
				break;
			default: Debug.Log("Unknown trigger entered: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
}
