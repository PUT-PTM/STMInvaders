using UnityEngine;
using UnityEngine.UI;
using System;
using STMInputDLL;
using System.Threading;

public class PlayerBehaviourVCP : MonoBehaviour {
	#region Variables
	public PlayerSpawner spawner;
	public LifesScript lifes;
	public float speed;
	public bool godMode;    // indestructible mode

	private Rigidbody2D rb;
	private RespawnShield shield;
	private float moveVertical;
	private float moveHorizontal;

	public STMInput vcp;
	private Thread vcpThread;

	#endregion
	#region Start & Update
	void Awake() {
		if (lifes) {
			moveHorizontal = 0f;
			moveVertical = 0f;
			lifes.UPDATE();

			rb = GetComponent<Rigidbody2D>();
			shield = GetComponentInChildren<RespawnShield>();
		} else Debug.LogError("No reference to LIFES");
		if ((vcp = new STMInput())) {
			vcpThread = new Thread(vcp.Run);
			vcpThread.Priority = System.Threading.ThreadPriority.AboveNormal;
			vcpThread.Start();
		} else Debug.LogError("Something wrong with VCP My Fabulous masterrrr...");
	}
	// Checking axis and moving player
	private	float x, y;
	void Update() {
		if (vcp) {
			x = vcp.GetAxisX();
			y = vcp.GetAxisY();
		} else {
			x = Input.GetAxisRaw("Horizontal");
			y = Input.GetAxisRaw("Vertical");
		}

		if (x != 0 && x != moveHorizontal) {
			rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
			moveHorizontal = x;
		}
		if (y != 0 && y != moveVertical) {
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
			moveVertical = y;
		}
		rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
	}
	#endregion
	#region Unity OnDisable, OnTriggerEnter, OnDestroy
	// Called when player dying, check lifes left and decide
	// if player could still play or you get deadly message :>
	void OnDisable() {
		if (lifes) {
			Debug.Log("Player died");
			if (Statics.PLAYER_LIFES > 1) {
				Statics.PLAYER_IS_ALIVE = false;
				Statics.PLAYER_LIFES--;
				lifes.UPDATE();
				vcp.RunSound("explode");
				GetComponent<PlayerShootingVCP>().ClearOnDeath();
			} else {
				Statics.PLAYER_IS_ALIVE = false;
				Statics.PLAYER_LIFES--;
				lifes.UPDATE();
				Debug.Log("Sorry, no lifes left :P");
				vcp.RunSound("dead");
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
	void OnDestroy() {
		Debug.Log("Close VCP port");
		vcp.Dispose();
	}
	#endregion
}
