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

    float maxThrowVelocity = 2f;
    float maxRange = 10f;
    GameObject throwableHeart;
    bool throwing;
    
    void Awake () {
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

        if (Input.GetMouseButtonDown(0))
        {
            if (heartList.Count > 0)
            {
                throwableHeart = Instantiate(heartPrefab);
                RemoveHeart();
                throwableHeart.transform.position = player.transform.position;
                throwing = true;
            }
        }

        if (throwing)
        {
            throwableHeart.transform.position = player.transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (heartList.Count >= 0 && throwing)
            {
                Vector3 dist = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
                Vector3 dir = Mathf.Clamp(dist.magnitude, 0f, maxRange) * maxThrowVelocity * dist.normalized;
                throwableHeart.GetComponent<Heart>().SetVelocity(new Vector3(dir.x, dir.y, 0f));
                throwableHeart.GetComponent<Heart>().SetState(Heart.HeartState.inWorld);
                throwing = false;
            }
        }
    }

    public void AddHeart (GameObject newHeart)
    {
        Destroy(newHeart);
        heartList.Add(Instantiate(heartPrefab));
        player.GetComponent<Health>().AddHealthUnits(1);
    }

    public void RemoveHeart()
    {
        GameObject heart = heartList[heartList.Count - 1];
        heartList.Remove(heart);
        Destroy(heart);
        player.GetComponent<Health>().RemoveHealthUnits(1);
    }
}
