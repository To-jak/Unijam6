using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour {

    public GameObject heartPrefab;

    public Vector3 relativePosition = new Vector3(0, 3, 0);
    public float spacing = 0.5f;

    private List<GameObject> heartList;
    
    protected GameObject player;

    float maxThrowVelocity = 3f;
    float maxRange = 10f;
    GameObject throwableHeart;
    public bool throwing;
    Animator anim;

    int nbHearts;
    private AudioSource source;
    public AudioClip coeurClickBegin;
    public AudioClip coeurClickEnd;
    public AudioClip coeurClickLoop;
    public AudioClip coeurHover;


    void Start ()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        heartList = new List<GameObject>();
    }

    private void OnEnable()
    {
        UpdateDisplay(nbHearts);
        for (int i = 0; i < heartList.Count; i++)
        {
            heartList[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable ()
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            heartList[i].gameObject.SetActive(false);
        }
    }

    public void UpdateDisplay (int nbHearts)
    {
        this.nbHearts = nbHearts;
        if (heartList != null)
        {
            if (nbHearts < heartList.Count)
            {
                int n = heartList.Count - nbHearts;
                for (int i = 0; i < n; i++)
                {
                    GameObject heart = heartList[heartList.Count - 1];
                    heartList.RemoveAt(heartList.Count - 1);
                    Destroy(heart);
                }
            }
            else if (nbHearts > heartList.Count)
            {
                int n = nbHearts - heartList.Count;
                for (int i = 0; i < n; i++)
                {
                    GameObject newHeart = Instantiate(heartPrefab);
                    newHeart.SetActive(false);
                    heartList.Add(newHeart);
                }
            }
        }
    }

    private void Update()
    {
        transform.position = player.transform.position + relativePosition;
        if (heartList.Count > 0)
        {
            for (int i = 0; i < heartList.Count; i++)
            {
                heartList[i].transform.position = transform.position + ((i - (heartList.Count / 2f - 0.5f)) * spacing) * Vector3.right;
            }
        }

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Tab))
        {
            if (heartList.Count > 0)
            {
                if (!throwing)
                {
                    if (coeurClickBegin != null)
                        source.PlayOneShot(coeurClickBegin, 1F);
                }
                
                throwableHeart = Instantiate(heartPrefab);
                RemoveHeart();
                throwableHeart.transform.position = player.transform.position;
                throwing = true;
                throwableHeart.SetActive(false);
            }
        }

        if (throwing)
        {
            throwableHeart.transform.position = player.transform.position;

            //source.PlayOneShot(coeurClickLoop, 1F);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (heartList.Count >= 0 && throwing)
            {
                anim.SetBool("HeartLancer", true);
                Invoke("ThrowHeart", 0.2f);
                if (coeurClickEnd != null)
                    source.PlayOneShot(coeurClickEnd, 1F);
                //Debug.Log("CoeurClickEnd");
            }
        }
    }

    void ThrowHeart()
    {
        throwableHeart.SetActive(true);
        Vector3 dist = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        Vector3 dir = Mathf.Clamp(dist.magnitude, 0f, maxRange) * maxThrowVelocity * dist.normalized;
        throwableHeart.GetComponent<Heart>().SetVelocity(new Vector3(dir.x, dir.y, 0f));
        throwableHeart.GetComponent<Heart>().SetState(Heart.HeartState.inWorld);
        throwing = false;
    }

    public void AddHeart (GameObject newHeart)
    {
        Destroy(newHeart);
        heartList.Add(Instantiate(heartPrefab, transform.position + ((heartList.Count - (heartList.Count / 2f - 0.5f)) * spacing) * Vector3.right, Quaternion.identity));
        player.GetComponent<Health>().AddHealthUnits(1);
        if (coeurHover != null)
            source.PlayOneShot(coeurHover, 1F);
    }

    public void RemoveHeart()
    {
        GameObject heart = heartList[heartList.Count - 1];
        heartList.Remove(heart);
        Destroy(heart);
        player.GetComponent<Health>().RemoveHealthUnits(1);
    }
}
