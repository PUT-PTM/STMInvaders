using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	public float speed;

	private Transform pos;
	private string type;

	void Start() {
		pos = GetComponent<Transform>();
	}
	// Move based on type (enemy or player)
	void Update() {
		switch (type) {
			case "EnemyBullet":
				pos.position -= Vector3.up * Time.deltaTime * speed;
				break;
			case "PlayerBullet":
				pos.position += Vector3.up * Time.deltaTime * speed;
				break;
		}
	}
	// Public function for set bullets type
	public void SetType(string type) {
		this.type = type;
		switch (type) {
			case "EnemyBullet":
				tag = "EnemyBullet";
				break;
			case "PlayerBullet":
				tag = "PlayerBullet";
				break;
		}
	}
	public void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.tag) {
			case "Enemy": break;
			case "Player": break;
			case "EnemyBullet": break;
			case "Wall": break;
			case "BottomWall": break;
			case "TopWall": break;
			case "PlayerBullet": {
					Destroy(trigger.gameObject);
					Destroy(this.gameObject);
					break;
				}
			default:
				// Note for debugging
				Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
}
