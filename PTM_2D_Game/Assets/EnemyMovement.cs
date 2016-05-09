using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public Transform[] obj = new Transform[0];
	public int moveStart;	//width between zeroes of function
	public int moveEnd;		//basically points in space to move ship

	private Transform tr;
	private Transform temp;
	private Vector2 position;
	private float posX, pos;
	public bool dir;
	void Start() {
		temp = tr = GetComponent<Transform>();
		position = temp.position;   //point of reference for function
		posX = -moveStart / 2;      //position X counting from start position
		pos = 0;                    //position Y counting from start position
		moveEnd = Random.Range(30, 100);
		//its just for help, with this flag enemy ships not hit in the walls
		if (position.x > 0) dir = false; else dir = true;
	}

	//tylko tymczasowe flagi pomocniczE, coś na zasadzie - jak jest true to lataj i w którą stronę
	//TODO przerobić na delegaty
	private bool flyDir = true;		//ta określa kierunek
	private bool letMeFly = false;	//ta pozwala lecieć
	public bool LetMeFly { get { return letMeFly; } }
	//changing flag
	public void FlyNow() {
		letMeFly = true;
	}
	void Update () {
		if (flyDir && letMeFly) {
			if (posX < moveEnd) {
				tr.position = position + CalcPos();
			} else flyDir = false;
		} else if (letMeFly) {
			if (posX > (-moveStart / 2)) {
				tr.position = position + CalcPos(false);
			} else {
				flyDir = true;
				letMeFly = false;
			}
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
