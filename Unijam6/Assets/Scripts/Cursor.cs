using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {


	[SerializeField]
	private Sprite cursorGrab;

	[SerializeField]
	private Sprite cursorDefault;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3 (cursorPosition.x, cursorPosition.y, 0);
		CheckClick ();
	}

	void CheckClick () {

		if (Input.GetMouseButton(0)) {
			GetComponent<SpriteRenderer> ().sprite = cursorGrab;
		} else {
			GetComponent<SpriteRenderer> ().sprite = cursorDefault;
		}
	}

}
