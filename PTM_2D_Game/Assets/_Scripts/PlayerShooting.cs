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
	private float testTime;
	void Update () {
		if (spaceHolded) {
			testTime += Time.deltaTime;
			// release space
			if (Input.GetKeyUp(KeyCode.Space)) {
				spaceHolded = false;
				Debug.Log("Time to release space: " + testTime);
			}
		}
		else if (Input.GetKeyDown(KeyCode.Space)) {
			// mechanics for shooting
			AddBullet();
			// setting flag to true (holding space)
			spaceHolded = true;
		}
	}

	private void AddBullet() {
		Transform bullet = Instantiate(bullets[0]);
		bullet.position = player.position;
		testTime = 0;
	}
}
