using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController2D : Controller2D
{
    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        switch (other.tag)
        {
            case "Heart":
                if (GetComponent<Player>().playerState == Player.PlayerState.HeartBar && other.GetComponent<Heart>().catchable)
                    GameObject.FindGameObjectWithTag("HeartBar").GetComponent<HeartBar>().AddHeart(other);
                break;
            case "End":
                GameManager.instance.EndLevel();
                break;
            default:
                break;
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            GetComponent<Player>().Hurt(obstacle.damage, Vector3.Normalize(-dir + Vector3.up));
        }
    }

}
