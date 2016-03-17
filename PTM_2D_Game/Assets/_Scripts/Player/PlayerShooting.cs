using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	public Transform[] bullets = new Transform[0];

	private Transform player;
	// Use this for initialization
	void Start () {
		player = GetComponent<Transform>();
	}

	private bool spaceHolded = false;
	//private float AdditionalBulletsTimer = 0;
	void Update () {
		//if (AdditionalBulletsTimer > 0f) {
		//	AdditionalBulletsTimer -= Time.deltaTime;
		//	if (AdditionalBulletsTimer < 0f) {
		//		AddBullet();
		//	}
		//}
		if (spaceHolded) {
			//testTime += Time.deltaTime;
			// release space
			if (Input.GetKeyUp(KeyCode.Space)) {
				spaceHolded = false;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Space)) {
			// mechanics for shooting
			AddBullet();

			//AdditionalBulletsTimer = 0.1f;

			// setting flag to true (holding space)
			spaceHolded = true;
		}
	}

	private void AddBullet() {
		Transform bullet = Instantiate(bullets[0]);
		bullet.position = player.position;
		bullet.GetComponent<BulletBehaviour>().SetType("PlayerBullet");
	}
}
