using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public Transform[] obj = new Transform[0];
	public int moveStart;	//szerokość pomiedzy miejscami zerowymi funkcji
	public int moveEnd;		//w zasadzie ile punktów ma przelecieć statek

	private Transform tr;
	private Transform temp;
	private Vector2 position;
	private float posX, pos;
	public bool dir;
	void Start() {
		temp = tr = GetComponent<Transform>();
		position = temp.position;   //punkt odniesienia dla funkcji
		posX = -moveStart / 2;      //pozycja X względem punktu początkowego
		pos = 0;                    //pozycja Y względem punktu startowego 
		moveEnd = Random.Range(30, 100);
		//właściwie tylko po to aby mi w ścianę się statek nie wpieprzył :P
		if (position.x > 0) dir = false; else dir = true;
	}

	//tylko tymczasowa flaga pomocnicza, coś na zasadzie - jak jest to lataj i w którą stronę
	private bool terefere = true;
	// TODO private bool letMeFly = false;	
	void Update () {
		// 
		if (terefere) {
			if (posX < moveEnd) {
				tr.position = position + CalcPos();
			} else terefere = false;
		} else {
			if (posX > (-moveStart / 2)) {
				tr.position = position + CalcPos(false);
			} else terefere = true;
		}
	}

	private Vector2 CalcPos(bool dir = true) {
		if(dir) return new Vector2(Func(posX++), -pos++);
		else return new Vector2(Func(posX--), -pos--);
	}
	private float Func(float x) {
		if (dir) return -0.01f * x * x + 55f;
		else return 0.01f * x * x - 55f;
	}
}
