using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController2D : Controller2D {

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        switch (other.tag)
        {
            case "Player":
               other.GetComponent <Player> ().Hurt(GetComponent<Projectile>().damage,GetComponent<Projectile>().direction);
                Destroy(gameObject);
                break;
            case "HealthBar":
                Destroy(gameObject);
                break;
            default:
                GetComponent<Projectile>().Impact();
                Destroy(gameObject);
                break;
        }
    }
}
