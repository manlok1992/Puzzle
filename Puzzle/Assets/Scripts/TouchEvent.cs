using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {
	public bool isTouchDown = false;
	enum Direction{LEFT, RIGHT, UP, DOWN, NONE};
	Direction dir;
	// Use this for initialization
	void Start () {
		dir = Direction.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		if(isTouchDown) {
			if(Input.GetAxis("Mouse X") < -0.3) {
				//Left
				dir = Direction.LEFT;
			}
			if(Input.GetAxis("Mouse X") > 0.3) {
				//Right
				dir = Direction.RIGHT;
			}
			if(Input.GetAxis("Mouse Y") < -0.3) {
				//Down
				dir = Direction.DOWN;
			}
			if(Input.GetAxis("Mouse Y") > 0.3) {
				//Up
				dir = Direction.UP;
			}
			if(Input.GetAxis("Mouse X") < 0.3 && Input.GetAxis("Mouse X") > -0.3 && 
			   Input.GetAxis("Mouse Y") < 0.3 && Input.GetAxis("Mouse Y") > -0.3) {
				dir = Direction.NONE;
			}
		}
	}

	void OnMouseDown() { 
		isTouchDown = true;
	}

	void OnMouseUp() {
		isTouchDown = false;
	}
}
