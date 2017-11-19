using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShooter : TriggerObject {
    
    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float rotation;

    public float cooldown;
    float timer;

    protected override void Update()
    {
        timer += Time.deltaTime;

        bool allTriggers = true;
        for (int i = 0; i < triggers.Length; i++)
        {
            allTriggers &= triggers[i].triggered;
        }

        if (allTriggers && !triggered)
        {
            Trigger(true);
        }
    }

    public override void Trigger(bool triggered)
    {
        if (timer >= cooldown)
        {
            Fire();
            timer = 0f;
            this.triggered = false;
        }
    }

    void Fire()
    {
        projectile.GetComponent<Projectile>().direction = direction;
        projectile.GetComponent<Projectile>().rotation = rotation;
        GameObject proj = Instantiate(projectile, transform.position, transform.localRotation);
    }
}
