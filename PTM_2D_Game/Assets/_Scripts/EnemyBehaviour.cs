using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
	public Transform[] bullets = new Transform[0];
	public Transform target;
	public Transform enemy;
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

		//transform.position = Vector2.Lerp(
		//	transform.position, target.transform.position, 
		//	Time.deltaTime * rotationSpeed);

		//nie działa -.-
		//Vector3 vectorToTarget = target.position - transform.position;
		//float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		//Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		//transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
	}

	private void AddBullet() {
		if (bullets.Length > 0) {
			Transform bullet = Instantiate(bullets[0]);
			bullet.position = enemy.position;
			bullet.GetComponent<BulletBehaviour>().SetType("EnemyBullet");
		} else Debug.LogError("Bullets not added to array!!!");
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Bullet") {
			Destroy(trigger.gameObject);
			Destroy(gameObject);
		} else Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
	}
}
