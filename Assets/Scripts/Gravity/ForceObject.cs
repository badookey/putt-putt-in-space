using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject: MonoBehaviour {

    public bool active = true;
    public ForceType forceType;
    public float forceMagnitude;

    public float Force() {
        if (!active)
            return 0f;

        float force = 0f;
        switch (forceType) {
            case ForceType.positive:
                force = forceMagnitude;
                break;
            case ForceType.negative:
                force = - 1f * forceMagnitude;
                break;
        }
        return force;
    }
}

public enum ForceType {
    positive, negative
}