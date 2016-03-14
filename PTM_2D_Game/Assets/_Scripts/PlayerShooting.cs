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
	void Update () {
		if (spaceHolded) {
			// release space
			if (Input.GetKeyUp(KeyCode.Space)) {
				spaceHolded = false;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Space)) {
			// mechanics for shooting

			Transform bullet = Instantiate(bullets[0]);
			bullet.position = player.position;

			// setting flag to true (holding space)
			spaceHolded = true;
		}
	}
}
