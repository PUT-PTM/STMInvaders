using UnityEngine;
using System.Collections;

public class EntitySpawner : MonoBehaviour {
	public float delay;
	private float DELAY = 0;

	public int min;
	public int max;
	public int heigh;

	public GameObject obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(DELAY < 0) {
			Instantiate(obj, new Vector3(Random.Range(min,max), heigh, 0), Quaternion.identity);
			DELAY = delay;
		} else {
			DELAY -= Time.deltaTime;
		}
	}
}
