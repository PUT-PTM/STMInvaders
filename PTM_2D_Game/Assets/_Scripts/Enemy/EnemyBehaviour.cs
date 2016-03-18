using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public Transform[] bullets = new Transform[0];
	private Transform enemy;

	public float ShootDelayMin;
	public float ShootDelayMax;
	public float timeToNextShoot;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		timeToNextShoot = RandomNextShoot();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeToNextShoot < 0) {
			AddBullet();
			timeToNextShoot = RandomNextShoot();
		} else timeToNextShoot -= Time.deltaTime;
		

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

	float RandomNextShoot() {
		return Random.Range(ShootDelayMin, ShootDelayMax);
	}

	public Text text {
		get; set;
	}
}
