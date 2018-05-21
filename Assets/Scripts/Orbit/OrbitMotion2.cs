using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion2 : MonoBehaviour {

    public List<Transform> orbitingObjects;
    public Ellipse orbitPath;
    
    [Range(0f, 1f)]
    public float orbitProgress = 0f;  // 椭圆距离
    public float orbitPeriod = 3f;  // 绕一圈时间
    public bool orbitActive = true;

    List<float> progressCorrections;

    void Start() {
       
        if (progressCorrections == null) {
            progressCorrections = new List<float>();
        }

        if (orbitingObjects == null) {
            orbitingObjects = new List<Transform>();
        } else {
            for (int i = 0; i < orbitingObjects.Count; i++)
                progressCorrections.Add(0f);
        }

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
    }

    void SetOrbitingObjectPosition() {
        for (int i = 0; i < orbitingObjects.Count; i++) {

            float correctedProgress = (orbitProgress + progressCorrections[i]) % 1f;
            Vector2 orbitPos = orbitPath.evaluate(correctedProgress);

            //Debug.Log(string.Format("new pos is: {0}, {1}", orbitPos.x, orbitPos.y));
            orbitingObjects[i].localPosition = new Vector3(orbitPos.x, orbitPos.y, 0f);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameObject player = collision.gameObject;
            player.transform.SetParent(null, true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        print(collision.transform.position);

        //if (collision.tag == "Player") {
        //    GameObject player = collision.gameObject;
        //    player.transform.SetParent(this.GetComponentInParent<Transform>(), true);

        //    Vector3 lPos = player.transform.position;
        //    float correction = orbitPath.antiEvaluate(lPos.x, lPos.y);
        //    Debug.Log(correction);
        //    //orbitingObjects.Add(player.transform);
        //    //progressCorrections.Add(correction);
        //}
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
    }
}
