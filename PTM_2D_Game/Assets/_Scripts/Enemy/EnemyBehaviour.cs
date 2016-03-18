using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public Transform[] bullets = new Transform[0];
	public Text text { get; set; }
	public float shootDelayMin;
	public float shootDelayMax;
	public float timeToNextShoot;
	public float speed;

	private Transform enemy { get; set; }

	void Start () {
		enemy = GetComponent<Transform>();
		timeToNextShoot = RandNextShoot;
	}
	// Random shooting and (at this moment) simple move for testing
	void Update () {
		if (timeToNextShoot < 0) {
			AddBullet();
			timeToNextShoot = RandNextShoot;
		} else timeToNextShoot -= Time.deltaTime;

		enemy.position += new Vector3(speed, 0f) * Time.deltaTime;
	}
	// Instantiate bullet
	private void AddBullet() {
		if (bullets.Length > 0) {
			Transform bullet = Instantiate(bullets[0]);
			bullet.position = enemy.position;
			bullet.GetComponent<BulletBehaviour>().SetType("EnemyBullet");
		} else Debug.LogError("Bullets not added to array!!!");
	}
	// Reaction for various triggers
	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "Wall": break;
			case "SideWall": break;
			case "PlayerBullet": {
					Destroy(trigger.gameObject);
					Destroy(gameObject);
					break;
				}
			case "EnemyBullet": break;
			default:
				// Note for debugging
				Debug.Log("Unknown trigger: " + trigger.gameObject.tag, trigger);
				break;
		}
	}
	// Randomize time between last and next shoot
	private float RandNextShoot {
		get { return Random.Range(shootDelayMin, shootDelayMax); }
	}
}
