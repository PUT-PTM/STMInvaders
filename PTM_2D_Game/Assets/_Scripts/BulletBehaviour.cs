using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	private Transform pos;
	public float speed;

	// Use this for initialization
	void Start () {
		pos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		pos.position += Vector3.up * Time.deltaTime * speed;
	}
}
