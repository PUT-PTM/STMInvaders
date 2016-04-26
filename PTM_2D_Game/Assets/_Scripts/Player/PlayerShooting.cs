using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	public AmmoStorage bullets;

	private Transform player;
	private bool spaceHolded = false;
	private PlayerBehaviourVCP dupa;

	void Start () {
		player = GetComponent<Transform>();
		dupa = GetComponent<PlayerBehaviourVCP>();
	}
	// Wait for space to release new bullet
	void Update () {
		if (dupa.STM_Input) {
			if (dupa.STM_Input.GetButton()) {
				AddBullet();
			}
		}else {
			if (spaceHolded) {
				if (Input.GetKeyUp(KeyCode.Space)) {
					spaceHolded = false;
				}
			} else if (Input.GetKeyDown(KeyCode.Space)) {
				AddBullet();
				spaceHolded = true;
			}
		}
	}
	// Instantiate new bullet
	private void AddBullet() {
		Transform bullet = Instantiate(bullets[1]);
		bullet.position = player.position;
		bullet.GetComponent<BulletBehaviour>().SetType("PlayerBullet");
	}
}
