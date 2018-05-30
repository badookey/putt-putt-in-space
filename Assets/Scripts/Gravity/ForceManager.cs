using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForceManager : MonoBehaviour {

    public float G = 1000f;
    public float threshold = 250f;

    public List<ForceObject> forceGenerators;
    public List<ForceAffectedObject> forceReceivers;

    private void Start() {
        forceGenerators = new List<ForceObject>(FindObjectsOfType<ForceObject>());
        forceReceivers = new List<ForceAffectedObject>(FindObjectsOfType<ForceAffectedObject>());
    }
    
    private void FixedUpdate() {
        foreach (ForceAffectedObject fao in forceReceivers) {
            ApplyMagneticForce(fao);
        }
    }

    private void ApplyMagneticForce(ForceAffectedObject fao) {
        Vector2 newForce = Vector2.zero;

        foreach (ForceObject fo in forceGenerators) {
            if (fao == fo)
                continue;

            float distance = Vector2.Distance(fao.transform.position, fo.transform.position);
            float force = G * fao.Force() * fo.Force() / Mathf.Pow(distance, 2);

            if (float.IsInfinity(force)) {
                force = 0f;
            } else if (Mathf.Abs(force) > threshold) {
                force =  force > 0 ? threshold : -1f * threshold;
            }

            Vector2 direction = fao.transform.position - fo.transform.position;
            direction.Normalize();

            newForce += force * direction * Time.fixedDeltaTime;
        }
        
        fao.rb.AddForce(newForce);
    }
}

