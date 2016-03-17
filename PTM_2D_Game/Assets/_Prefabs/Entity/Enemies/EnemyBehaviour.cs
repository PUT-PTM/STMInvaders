using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
	public Transform[] bullets = new Transform[0];
	private Transform enemy;
	public float rotationSpeed;

	public float shootDelay;
	// Use this for initialization
	void Start () {
		shootDelay = 2;
		enemy = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shootDelay < 0) {
			AddBullet();
			shootDelay = 3f;
		} else shootDelay -= Time.deltaTime;
		

	}

	private void AddBullet() {
		if (bullets.Length > 0) {
			Transform bullet = Instantiate(bullets[0]);
			bullet.position = enemy.position;
			bullet.GetComponent<BulletBehaviour>().SetType("EnemyBullet");
		} else Debug.LogError("Bullets not added to array!!!");
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "PlayerBullet": {
					Destroy(trigger.gameObject);
					Destroy(gameObject);
				}
				break;
			case "EnemyBullet":;
				break;
			default: Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
				break;
		}
	}

	public Text text {
		get; set;
	}
}
