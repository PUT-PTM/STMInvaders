using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public AmmoStorage bullets;
	public Text text { get; set; }
	public float shootDelayMin;
	public float shootDelayMax;
	public float timeToNextShoot;
	public float speed;
	
	public string ID {
		get { return ID; }
		set {
			text.text = value;
		}
	}

	public Transform enemy { get; set; }

	void Awake () {
		enemy = GetComponent<Transform>();
		timeToNextShoot = RandNextShoot;
	}
	// Random shooting and (at this moment) simple move for testing
	void Update () {
		if (timeToNextShoot < 0) {
			AddBullet();
			timeToNextShoot = RandNextShoot;
		} else timeToNextShoot -= Time.deltaTime;

		enemy.position += new Vector3(-speed, -speed) * Time.deltaTime;
	}
	// Instantiate bullet
	private void AddBullet() {
		if (GetComponent<EnemyMovement>().CanShoot) {
			if (bullets.Length > 0) {
				// Instantiate
				Transform bullet = Instantiate(bullets[0]);
				bullet.position = enemy.position;
				bullet.GetComponent<BulletBehaviour>().SetType("EnemyBullet");
				//bullet.SetParent(this.transform);
			} else Debug.LogError("Bullets not added to array!!!");
		}
	}
	// Reaction for various triggers
	void OnTriggerEnter2D(Collider2D trigger) {
		switch (trigger.gameObject.tag) {
			case "Enemy": break;
			case "Player": break;
			case "Wall": break;
			case "SideWall": break;
			case "BottomWall": break;
			case "TopWall": break;
			case "PlayerBullet": {
					Destroy(trigger.gameObject);
					transform.DetachChildren();
					GetComponentInParent<EnemySpawnerBehaviour>().KillMe(this.transform);
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
