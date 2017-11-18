using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	void OnMouseDrag()

	{

		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		gameObject.transform.position = point;

	}

	void OnMouseUp(){

	}

}
