using UnityEngine;
using System.Collections;

public class AmmoStorage : MonoBehaviour {
	// Storage for all types off bullets
	public Transform[] bullets = new Transform[0];
	public int Length { get { return bullets.Length; } }

	// Overloaded indexer for acces to table
	public Transform this[int key] {
		get { return bullets[key]; }
	}
}
