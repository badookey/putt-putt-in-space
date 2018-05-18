﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public Transform orbitingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;  // 椭圆距离
    public float orbitPeriod = 3f;  // 绕一圈时间
    public bool orbitActive = true;

	void Start () {
        if (orbitingObject == null) {
            orbitActive = false;
            return;
        }

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());

	}


    void SetOrbitingObjectPosition() {
        Vector2 orbitPos = orbitPath.evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, orbitPos.y, 0f);
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
