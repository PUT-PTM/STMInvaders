using UnityEngine;
using System.Collections;

public class BulletKiller : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Bullet") {
			Destroy(trigger.gameObject);
		} else Debug.Log("Unknown trigger entered: " + trigger.gameObject.tag, trigger);
	}
}
