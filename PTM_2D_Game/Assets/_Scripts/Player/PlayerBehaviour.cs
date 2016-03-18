using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	public PlayerSpawner spawner;
	public LifesScript lifes;

	private Rigidbody2D rb;
	private RespawnShield shield;
	private float moveVertical;
	private float moveHorizontal;

	// Use this for initialization
	void Start() {
		if (lifes) {
			moveHorizontal = 0f;
			moveVertical = 0f;
			rb = GetComponent<Rigidbody2D>();
			lifes.UPDATE();
		} else Debug.LogError("No reference to LIFES");

		shield = GetComponentInChildren<RespawnShield>();
	}

	void Update() {
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		if(x != 0 && x != moveHorizontal) {
			rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
			moveHorizontal = x;
		}
		if(y != 0 && y != moveVertical) {
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
			moveVertical = y;
		}
		rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
	}
	void OnDisable() {
		if (lifes) {
			Debug.Log("Player died");
			if (Statics.PLAYER_LIFES > 1) {
				Statics.PLAYER_IS_ALIVE = false;
				Statics.PLAYER_LIFES--;
				lifes.UPDATE();
			} else {
				Statics.PLAYER_IS_ALIVE = false;
				Statics.PLAYER_LIFES--;
				lifes.UPDATE();
				Debug.Log("Sorry, no lifes left :P");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "PlayerBullet": return;
			case "PlayerShield": return;
			case "EnemyBullet": {
					Destroy(trigger.gameObject);
					if(!shield.gameObject.activeSelf) gameObject.SetActive(false);
				}
				break;
			default:
				Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
}
