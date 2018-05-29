using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpinScript))]
public class PortalScript : MonoBehaviour {

    public PortalAssociation association;

    public PortalScript target;  // for ono2one
    public List<PortalScript> targetList;  // for one2many
    public static List<PortalScript> Portals;  // for random

    bool isTeleporting = false;
    System.Random rnd = new System.Random();  // unity has own class Random

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {

            if (isTeleporting) {
                // donothing
            } else {
                PortalScript p = GetTargetPortal();
                if (p != this)  // teleport when diff
                    other.transform.position = p.transform.position;

                Rigidbody2D orb = other.GetComponent<Rigidbody2D>();

                float ang = SpinScript.CalculateRotateAngle(p.transform);
                Vector2 newDir = Quaternion.AngleAxis(ang, -Vector3.forward) * Vector2.up;
                orb.velocity = orb.velocity.magnitude * newDir;

                p.isTeleporting = true;
            }

            isTeleporting = false;
        }
    }

    void OnEnable() {
        if (Portals == null)
            Portals = new List<PortalScript>();

        Portals.Add(this);
    }

    void OnDisable() {
        Portals.Remove(this);
    }
    
    // Get target portal regarding association
    // (return itself by default)
    PortalScript GetTargetPortal() {

        PortalScript tp = this;

        if (association == PortalAssociation.random) {
            if (Portals.Count > 1) {
                while (tp == this)
                    tp = Portals[rnd.Next(Portals.Count)];
            }
        } else if (association == PortalAssociation.onoToOne) {
            if (target != null)
                tp = target;
        } else if (association == PortalAssociation.oneToMany) {
            if (targetList.Count > 0)
                tp = targetList[rnd.Next(targetList.Count)];
        }

        return tp;
    }
    
}

public enum PortalType {
    bidirection, entrance, exit
}

public enum PortalAssociation {
    random, onoToOne, oneToMany
}

