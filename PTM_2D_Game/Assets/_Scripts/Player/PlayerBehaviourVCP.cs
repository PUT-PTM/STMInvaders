using UnityEngine;
using UnityEngine.UI;
using System;
using STMInputDLL;
using System.Threading;

public class PlayerBehaviourVCP : MonoBehaviour {
	public PlayerSpawner spawner;
	public LifesScript lifes;
	public float speed;

	private Rigidbody2D rb;
	private RespawnShield shield;
	private float moveVertical;
	private float moveHorizontal;
	public STMInput STM_Input;
	private Thread VCP;

	void Start() {
		STM_Input = new STMInput();
		VCP = new Thread(STM_Input.Run);
		VCP.Start();
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
		float x = 0, y = 0;
		x = STM_Input.GetAxisX();
		y = STM_Input.GetAxisY();

		if (x != 0 && x != moveHorizontal) {
			rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
			moveHorizontal = x;
		}
		if(y != 0 && y != moveVertical) {
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
			moveVertical = y;
		}
		rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
	}
	void OnDestroy() {
		STM_Input.ClosePort();
	}
}
