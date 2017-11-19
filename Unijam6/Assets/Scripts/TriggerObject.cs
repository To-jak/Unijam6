using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour {

    public TriggerController2D[] triggers;

    protected bool triggered;

	protected virtual void Update () {
        bool allTriggers = true;
        for (int i = 0; i < triggers.Length; i++)
        {
            allTriggers &= triggers[i].triggered;
        }

        if (allTriggers && !triggered)
        {
            Debug.Log("AAAAAAAAAAAAAAAAA");
            Trigger(true);
        }
        else if (triggered && !allTriggers)
        {
            Trigger(false);
        }
	}

    public virtual void Trigger(bool triggered)
    {

    }
}
