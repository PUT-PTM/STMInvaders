using UnityEngine;
using System.Collections;

public class RespawnShield : MonoBehaviour {
	public float shieldMaxTime;
	public float remainingTime;
	public float resizeTime = 1;
	// 0 - reduce, 1 - expand shield
	public bool direction;

	private Transform shield;
	private SpriteRenderer sprite;
	private Color color;

	void Start() {
		shield = GetComponent<Transform>();
		sprite = GetComponent<SpriteRenderer>();
		direction = true;
	}
	// Mechanic for shield resizing and changing opacity
	void Update() {
		if(remainingTime > 0) {
			color = sprite.color;
			color = new Color(color.r, color.g, color.b, remainingTime / shieldMaxTime);
			sprite.color = color;

			if (direction) {
				if (resizeTime > 0) {
					//zwiększanie
					shield.localScale += new Vector3(0.001f, 0.001f);
					resizeTime -= Time.deltaTime;
				} else {
					direction = false;
					resizeTime = 1;
				}
			} else {
				if (resizeTime > 0) {
					//zmniejszanie
					shield.localScale -= new Vector3(0.001f, 0.001f);
					resizeTime -= Time.deltaTime;
				} else {
					direction = true;
					resizeTime = 1;
				}
			}
		}
	}
	// Check if shield still have time to work or not
	void LateUpdate () {
		shield.position = GetComponentInParent<Transform>().position;
		if (remainingTime > 0) {
			remainingTime -= Time.deltaTime;
		} else gameObject.SetActive(false);
	}
	// public method to switch shields on
	public void ShieldOn() {
		remainingTime = shieldMaxTime;
		gameObject.SetActive(true);
	}
	// Destroying enemy bullets if enabled
	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.tag) {
			case "PlayerBullet": break;
			case "Enemy": break;
			case "EnemyBullet":
				Destroy(trigger.gameObject);
				break;
		}
	}
}
