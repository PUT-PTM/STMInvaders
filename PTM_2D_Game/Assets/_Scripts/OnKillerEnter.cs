using UnityEngine;
using System.Collections;

public class OnKillerEnter : MonoBehaviour {
	private Rigidbody2D rb;
	public float power;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
	}
}
