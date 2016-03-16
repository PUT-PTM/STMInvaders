﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	private float moveVertical;
	private float moveHorizontal;

	public PlayerSpawner spawner;
	public LifesScript lifes;

	// Use this for initialization
	void Start() {
		if (lifes) {
			moveHorizontal = 0f;
			moveVertical = 0f;
			rb = GetComponent<Rigidbody2D>();
			lifes.UPDATE();
		} else Debug.LogError("No reference to LIFES");
	}
	
	/* version 4 kinematic body
	void Update () {
		Vector3 pos = rb.transform.position;

		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector3 offset = new Vector3(x, y) * speed * Time.deltaTime;

		rb.transform.position = new Vector3(pos.x + offset.x, pos.y + offset.y, pos.z);
		
	}*/
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
		//rb.AddForce(new Vector2(x, y) * Time.deltaTime * speed);
		rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
	}
	void OnDisable() {
		Debug.Log("Entered disable mode - u died? :>");
		if (lifes) {
			GlobalStatics.PLAYER_IS_ALIVE = false;
			GlobalStatics.PLAYER_LIFES--;
			lifes.UPDATE();
			spawner.gameObject.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "EnemyBullet") {
			Destroy(trigger.gameObject);
			gameObject.SetActive(false);
		} else Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
	}
}
