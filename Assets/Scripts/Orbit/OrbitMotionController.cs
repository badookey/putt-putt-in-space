using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotionController : MonoBehaviour {

    public Transform orbitObject;
    public Rigidbody2D orbitObjectRb;
    public Attractor orbitObjectAt;

    [Range(0f, 1f)]
    public float orbitProgress;
    public float orbitPeriod;


    int orbitDirection;  // +1 clockwise -1 counter-clockwise
    bool orbitActive;
    public bool isExiting, isEntering = false;

    CircleCollider2D cc;
    Vector2 center;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
        center = transform.position;
    }

    void Update() {

        if (orbitActive && !isEntering && Input.GetKeyDown(KeyCode.Space)) {
            orbitActive = false;  // stop orbiting
            isExiting = true;

            if (orbitObject != null && orbitObjectRb != null) {
                //orbitObjectRb.bodyType = RigidbodyType2D.Dynamic;
                orbitObjectRb.WakeUp();
                orbitObjectRb.velocity = GetExitVelocity();
            }

            //Reset();
        }

    }

    Vector2 GetExitVelocity() {
        float angle = GetAngle(orbitObject.position);
        Vector2 tangentVector = Vector2.zero;
        // rotate vector
        if (orbitDirection == 1)
            tangentVector = Quaternion.AngleAxis(angle, -Vector3.forward) * Vector2.right;
        else
            tangentVector = Quaternion.AngleAxis(angle, -Vector3.forward) * Vector2.left;

        float currentSpeed = Mathf.Deg2Rad * 360f * cc.radius / orbitPeriod;
        return currentSpeed * tangentVector;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player")
            return;

        if (isExiting)
            return;
        
        
        isEntering = true;
        orbitObject = other.transform;
        orbitObjectRb = orbitObject.GetComponent<Rigidbody2D>();
        orbitObjectAt = orbitObject.GetComponent<Attractor>();
        orbitObjectAt.enabled = false;

        Vector2 enterV = orbitObjectRb.velocity;
        orbitDirection = GetOrbitDirection();  // compute direction
        
        // disable rb temperately
        orbitObjectRb.Sleep();
        orbitProgress = (orbitDirection * GetAngle(orbitObject.position)) % 360f / 360f;

        StartCoroutine(AnimateApproach(GetContactPoint()));
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag != "Player")
            return;

        orbitObjectAt.enabled = true;
        isExiting = false;
        Reset();
    }

    void Reset() {
        orbitObject = null;
        orbitObjectRb = null;
        orbitObjectAt = null;
        orbitProgress = 0f;
    }

    void SetOrbitingObjectPosition() {
        if (orbitObject == null)
            return;

        Vector2 orbitPos = GetPoint(orbitDirection * orbitProgress);
        orbitObject.position = new Vector3(orbitPos.x, orbitPos.y, 0f);
    }

    IEnumerator AnimateApproach(Vector2 pos) {
        // start approaching
        bool isApproaching = true;
        float smoothSpeed = 5f;
        float limitation = 0.01f;

        while (isApproaching) {
            orbitObject.position = Vector2.Lerp(orbitObject.position, pos, Time.deltaTime * smoothSpeed);
            if (Vector2.Distance(orbitObject.position, pos) < limitation) {
                orbitObject.position = pos;
                isApproaching = false;
            }
                
            yield return null;
        }

        // finish entering
        orbitActive = true;
        isEntering = false;
        // start orbiting
        StartCoroutine(AnimateOrbit());
    }

    IEnumerator AnimateOrbit() {
        if (Mathf.Abs(orbitPeriod) < 0.1f) {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive) {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
        orbitActive = true;
    }

    // get clockwise angle
    float GetAngle(Vector2 pos) {
        Vector2 from = Vector2.up;
        Vector2 to = pos - center;
        int sign = pos.x < center.x ? -1 : 1;
        float ang = sign * Vector2.Angle(from, to);
        ang = (360 + ang) % 360;
        return ang;
    }

    Vector2 GetPoint(float progress) {

        float ang = 360 * progress;
        float a = ang * Mathf.Deg2Rad;
        float x = center.x + cc.radius * Mathf.Sin(a);
        float y = center.y + cc.radius * Mathf.Cos(a);

        return new Vector2(x, y);
    }

    Vector2 GetContactPoint() {
        float ang = GetAngle(orbitObject.position);
        return GetPoint(ang / 360f);
    }

    int GetOrbitDirection() {

        float ang = Vector2.Angle(Vector2.up, orbitObjectRb.velocity);
        float xMagnitude = Mathf.Sin(ang) * orbitObjectRb.velocity.magnitude;
        float yMagnitude = Mathf.Sin(ang) * orbitObjectRb.velocity.magnitude;

        float entryAngle = GetAngle(orbitObjectRb.velocity);
        if (orbitObject.position.y > center.y) {  // 1,4 quadrant
            if (orbitObject.position.x > center.x)
                return (entryAngle > 90 && entryAngle <= 180) ? 1 : -1;
            else
                return (entryAngle > 90 && entryAngle <= 180) ? -1 : 1;
        } else {  // 2,3 quadrant
            if (orbitObject.position.x > center.x)  
                return (entryAngle > 180 && entryAngle <= 360) ? 1 : -1;
            else
                return (entryAngle > 180 && entryAngle <= 360) ? -1 : 1;
        }
    }

}
