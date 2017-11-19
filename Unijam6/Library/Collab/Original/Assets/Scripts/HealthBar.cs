using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    float barSize = 1f;
    [SerializeField]
    Vector3 relativePostion = new Vector3 (0, 3, 0);

    int barState;

    GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateDisplay(barSize);
	}
	
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            barState = 0;
        }

        if (barState == 0)
        {
            transform.position = player.transform.position + relativePostion;
        }
        if (barState == 1)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.position = new Vector3 (point.x,point.y,0);
        }
        UpdateDisplay(1);
    }


    void UpdateDisplay(float healthValue)
    {
        transform.localScale = new Vector3 (healthValue*barSize, transform.localScale.y, transform.localScale.z);
    }

    void OnMouseDrag()
    {
        barState = 1;
    }

    void OnMouseUp()
    {
        barState = 2;

    }
}
