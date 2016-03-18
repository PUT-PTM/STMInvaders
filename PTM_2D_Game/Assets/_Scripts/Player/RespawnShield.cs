using UnityEngine;
using System.Collections;

public class RespawnShield : MonoBehaviour {
	public float ShieldMaxTime;
	public float remainingTime;
	private Transform shield;
	private SpriteRenderer sprite;
	private Color color;

	void Start() {
		shield = GetComponent<Transform>();
		sprite = GetComponent<SpriteRenderer>();
		Direction = true;
	}

	public float ResizeTime = 1;
	// 0 - reduce, 1 - expand
	public bool Direction;
	
	void Update() {
		if(remainingTime > 0) {
			color = sprite.color;
			color = new Color(color.r, color.g, color.b, remainingTime / ShieldMaxTime);
			sprite.color = color;

			if (Direction) {
				if (ResizeTime > 0) {
					//zwiększanie
					shield.localScale += new Vector3(0.001f, 0.001f);
					ResizeTime -= Time.deltaTime;
				} else {
					Direction = false;
					ResizeTime = 1;
				}
			} else {
				if (ResizeTime > 0) {
					//zmniejszanie
					shield.localScale -= new Vector3(0.001f, 0.001f);
					ResizeTime -= Time.deltaTime;
				} else {
					Direction = true;
					ResizeTime = 1;
				}
			}
		}
	}

	void LateUpdate () {
		shield.position = GetComponentInParent<Transform>().position;
		if (remainingTime > 0) {
			remainingTime -= Time.deltaTime;
		} else gameObject.SetActive(false);
	}

	public void ShieldOn() {
		remainingTime = ShieldMaxTime;
		gameObject.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.tag) {
			case "PlayerBullet": return;
			case "EnemyBullet":
				Destroy(trigger.gameObject);
				break;
		}
	}
}
