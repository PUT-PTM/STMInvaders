using UnityEngine;
using System.Collections;
using STMInputDLL;

public class PlayerShooting : MonoBehaviour {
	public AmmoStorage bullets;

	private Transform player;
	private bool spaceHolded = false;
	private PlayerBehaviourVCP STM_Input;

	void Start () {
		player = GetComponent<Transform>();
		STM_Input = GetComponent<PlayerBehaviourVCP>();
	}
	// Wait for space to release new bullet
	void Update () {
		if (STM_Input.STM_Input) {
			if (STM_Input.STM_Input.GetButton()) {
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
		STM_Input.STM_Input.RunSound("shoot");
		Transform bullet = Instantiate(bullets[1]);
		bullet.position = player.position;
		bullet.GetComponent<BulletBehaviour>().SetType("PlayerBullet");
	}
}
