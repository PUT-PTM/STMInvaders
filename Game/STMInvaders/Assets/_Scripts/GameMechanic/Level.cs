using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	// Enemies spawned at this level
	public Transform[] enemy = new Transform[0];
	// Flag that says if there are more types of enemies in this map
	// public bool multipleEnemies; - no use at this moment
	


	// NOTE - max positions to spawn on map:
	/*
	(-115,115)	|*********************|	(115,115)
				|                     |
				|                     |
	(-115,0)	|*********************|	(115,0)
				|                     |
				|                     |
	(-115,-95)	|*********************|	(115,-95)
	*/
}
