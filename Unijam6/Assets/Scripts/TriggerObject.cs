using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour {

    public TriggerController2D[] triggers;

    bool triggered;

	void Update () {
        bool allTriggers = true;
        foreach (TriggerController2D trigger in triggers)
        {
            allTriggers &= trigger.triggered;
        }

        if (allTriggers && !triggered)
        {
            Trigger(true);
        }
        else if (triggered && allTriggers)
        {
            Trigger(false);
        }
	}

    public virtual void Trigger(bool triggered)
    {

    }
}
