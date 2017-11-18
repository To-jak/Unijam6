using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour {

    public GameObject heartPrefab;

    public Vector3 relativePosition = new Vector3(0, 3, 0);
    public float spacing = 0.5f;

    private List<GameObject> heartList;
    
    protected GameObject player;
    
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        heartList = new List<GameObject>();
    }

    public void UpdateDisplay (int nbHearts)
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
                heartList.Add(Instantiate(heartPrefab));
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
    }

    public void AddHeart (GameObject newHeart)
    {
        heartList.Add(newHeart);
        player.GetComponent<Health>().AddHealthUnits(1);
    }
}
