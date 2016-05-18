using UnityEngine;
using STMInputDLL;

public class PlayerShootingVCP : MonoBehaviour {
	#region Variables
	public AmmoStorage bullets;

	private Transform player;
	//private bool spaceHolded = false;
	private STMInput vcp;
	public int magazine;        // bullets remaining in magazine
	private float bulletTimer;  // time between shoots
	public float reload;        // time between next series

	private bool makeSound = true;
	#endregion
	#region Start & Update
	void Start() {
		player = GetComponent<Transform>();
		vcp = GetComponent<PlayerBehaviourVCP>().vcp;
		magazine = 10;
		bulletTimer = 0.1f;

		reload = 1f;
	}
	// Wait for space to release new bullet
	void Update() {
		// check if can run new shoot series
		if (magazine > 0) {
			// check if VCP is connected
			if (vcp) {
				if (bulletTimer <= 0 && vcp.GetButton()) {
					AddBullet();
					if(makeSound) {
						vcp.RunSound("shoot");
						makeSound = false;
					}
					magazine--;
					bulletTimer = 0.1f;
				}
				else bulletTimer -= Time.deltaTime;
			}
			// if not - have fun on keyboard
			else {
				if (bulletTimer <= 0 && Input.GetKey(KeyCode.Space)) {
					AddBullet();
					if (makeSound) {
						vcp.RunSound("shoot");
						makeSound = false;
					}
					magazine--;
					bulletTimer = 0.1f;
				}
				else bulletTimer -= Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.Space) || vcp.GetButton()) {
				if (magazine <= 0) {
					reload = 0f;
				}
			}
		}
		// otherwise wait a while
		else {
			if (reload < 1f) {
				if ((reload += Time.deltaTime) > 1f) reload = 1f;
			}
			else {
				magazine = 10;
				makeSound = true;
			}
		}
	}
	#endregion
	#region Other methods
	// Instantiate new bullet
	private void AddBullet() {
		Transform bullet = Instantiate(bullets[1]);
		bullet.position = player.position;
		bullet.GetComponent<BulletBehaviour>().SetType("PlayerBullet");
	}
	public void ClearOnDeath() {
		reload = 1f;
		magazine = 10;
		makeSound = true;
	}
	#endregion
}