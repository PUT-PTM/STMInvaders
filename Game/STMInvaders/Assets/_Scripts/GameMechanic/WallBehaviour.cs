using UnityEngine;
using System.Collections;

public class WallBehaviour : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "PlayerShield": break;
			case "PlayerBullet":
				if (tag == "Wall") Destroy(trigger.gameObject);
				break;
			case "EnemyBullet":
				if (tag == "Wall") Destroy(trigger.gameObject);
				break;
			case "Enemy": {
					// Teleport for enemies on the sides or bottom of the map
					float x = trigger.GetComponent<Transform>().position.x;
					float y = trigger.GetComponent<Transform>().position.y;
					if (tag == "SideWall")
						trigger.GetComponent<Transform>().position =
							new Vector3(x >= 0 ? (-x + 10) : (-x - 10), y);
					else if (tag == "BottomWall")
						trigger.GetComponent<Transform>().position =
							new Vector3(x, -y + 10);
					break;
				}
			default:
				// Note for debugging
				Debug.Log("Unknown trigger entered: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
}
