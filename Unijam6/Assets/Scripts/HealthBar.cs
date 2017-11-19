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

    private AudioSource source;
    public AudioClip barreClickBegin;
    public AudioClip barreClickLoop;
    public AudioClip barreClickEnd;
    public AudioClip barreHover;
    public AudioClip bouclierHitProjectile;

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + relativePostion;
        source = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + relativePostion;
        barState = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (barState == 2)
            {
                barState = 3;
            }
            if (barState == 0)
            {
                barState = 4;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (barState == 0)
            {
                if (barreClickBegin != null)
                    source.PlayOneShot(barreClickBegin, 1F);
            }
            if (barState == 4)
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
        //source.PlayOneShot(barreClickLoop, 1F);
    }

    void OnMouseUp()
    {
        barState = 2;
        if (barreClickEnd != null)
            source.PlayOneShot(barreClickEnd, 1F);
    }
    void OnMouseDown()
    {
        if (barreClickBegin != null)
            source.PlayOneShot(barreClickBegin, 1F);
    }

    void OnMouseEnter()
    {
        if (barreHover != null)
            source.PlayOneShot(barreHover, 0.3F);
    }

    private void OnBecameInvisible()
    {
        barState = 3;
    }

    void ProcessState()
    {


        if (barState == 0)
        {
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + relativePostion, ref velocity, smoothTime_ShortDistance);
            transform.GetChild(0).gameObject.layer = 13;
        }   
        if (barState == 1)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.position = new Vector3(point.x, point.y, 0);

            transform.GetChild(0).gameObject.layer = 0;
        }
        if(barState == 2)
        {
            transform.GetChild(0).gameObject.layer = 8;
        }
        if(barState == 3)
        {
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + relativePostion, ref velocity, smoothTime_GreatDistance);
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, 1f);
            transform.GetChild(0).gameObject.layer = 9;
            if (Vector3.Distance(transform.position,player.transform.position) < range)
            {
                barState = 0;
            }
        }
        if (barState == 4)
        {
            Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
            Vector3 dist = new Vector3(dir.x, dir.y, 0f).normalized * 1.5f;
            float rot_z = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            transform.position = player.transform.position + dist;
            transform.GetChild(0).gameObject.layer = 13;
        }
    }
}
