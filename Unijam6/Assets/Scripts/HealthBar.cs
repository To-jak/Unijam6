using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    public float barSize = 1f; //Taille maximale de la barre
    [SerializeField]
    public Vector3 relativePostion = new Vector3 (0, 3, 0); //Position relative de la barre par rapport au joueur
    [SerializeField]
    public Vector3 velocity;
    [SerializeField]
    public float smoothTime_GreatDistance;
    [SerializeField]
    public float smoothTime_ShortDistance;
    [SerializeField]
    public float range;

    public int barState; //0: barre attachée au personnage | 1: Drag | 2: Drop (platform) | 3: retour

    GameObject player;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Init();
	}

    public void Init()
    {
        barState = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (barState != 1)
            {
                barState = 3;
            }
        }
        ProcessState();
    }

    public void UpdateDisplay(float healthValue)
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

    void ProcessState()
    {


        if (barState == 0)
        {
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + relativePostion, ref velocity, smoothTime_ShortDistance);
            transform.GetChild(0).gameObject.layer = 9;
        }   
        if (barState == 1)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.position = new Vector3(point.x, point.y, 0);

            transform.GetChild(0).gameObject.layer = 9;
        }
        if(barState == 2)
        {
            transform.GetChild(0).gameObject.layer = 8;
        }
        if(barState == 3)
        {
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + relativePostion, ref velocity, smoothTime_GreatDistance);
            transform.GetChild(0).gameObject.layer = 9;
            if (Vector3.Distance(transform.position,player.transform.position) < range)
            {
                barState = 0;
            }
        }
    }
}
