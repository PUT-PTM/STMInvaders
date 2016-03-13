using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	private bool lastVertical;		// last move in vertical axis
	private bool lastHorizontal;	//last move in horizontal axis

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	/* version 4 kinematic body
	void Update () {
		Vector3 pos = rb.transform.position;

		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector3 offset = new Vector3(x, y) * speed * Time.deltaTime;

		rb.transform.position = new Vector3(pos.x + offset.x, pos.y + offset.y, pos.z);
		
	}*/
	void Update() {
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		rb.AddForce(new Vector2(x, y) * Time.fixedDeltaTime * speed);
		//rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
	}
}
