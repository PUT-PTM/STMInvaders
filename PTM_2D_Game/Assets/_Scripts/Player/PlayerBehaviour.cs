using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBehaviour : MonoBehaviour {
	public PlayerSpawner spawner;
	public LifesScript lifes;
	public float speed;
	public bool godMode;	// indestructible mode

	private Rigidbody2D rb;
	private RespawnShield shield;
	private float moveVertical;
	private float moveHorizontal;
	
	void Start() {
		if (lifes) {
			moveHorizontal = 0f;
			moveVertical = 0f;
			lifes.UPDATE();

			rb = GetComponent<Rigidbody2D>();
			shield = GetComponentInChildren<RespawnShield>();
		} else Debug.LogError("No reference to LIFES");
	}
	// Checking axis and moving player
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
	// Called when player dying, check lifes left and decide
	// if player could still play or you get deadly message :>
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
	// Reaction for various triggers
	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "PlayerBullet": return;
			case "Enemy": return;
			case "EnemyBullet": {
					if(godMode == false) {
						// Destroy bullet and (if shield isn't active) - kill player
						Destroy(trigger.gameObject);
						if (!shield.gameObject.activeSelf) gameObject.SetActive(false);
					}
				}
				break;
			default:
				// Note for debugging
				Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
}
