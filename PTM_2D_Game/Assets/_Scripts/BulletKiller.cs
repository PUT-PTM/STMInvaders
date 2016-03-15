using UnityEngine;
using System.Collections;

public class BulletKiller : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Bullet") {
			Destroy(trigger.gameObject);
		}
	}
}
