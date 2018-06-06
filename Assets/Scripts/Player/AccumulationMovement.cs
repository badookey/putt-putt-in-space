using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulationMovement : MonoBehaviour {

    public float power = 10f;
    public float threshold = 500f;
    public DragMode drageMode = DragMode.backward;

    private Vector3 startPos;
    private Vector3 _sizeStart;
    private Vector3 _scaleStart;
    public float speed = 5f;
    public Transform target;

    private Rigidbody2D rb;
    private OrbitMotion om;
    private int _validHits;



    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        om = GetComponent<OrbitMotion>();

            _validHits = 0;
        drageMode = (DragMode) PlayerPrefs.GetInt("dragmode");

    }
    
    private void Update() {
        if (Time.timeScale > 0)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            if (Input.GetKeyDown("y"))
            {
                SwapDragMode();
            }
            if (Input.GetMouseButtonDown(0))
            {  // start
                startPos = Input.mousePosition;

            }
            else if (Input.GetMouseButton(0))
            {  // holding
               //Debug.Log("accumulation: " + ((Vector2)Input.mousePosition - startPos));
               //var _pointerTravel = (Vector2)(Input.mousePosition) - startPos;
               //var _rotation = ((_sizeStart.y + _pointerTravel.y) / _sizeStart.y) * _scaleStart.y;
               //transform.rotation = new Vector3(0, 0, _rotation);
               //Get mouse position
               //var _pointerTravel = (Vector3)(Input.mousePosition) - startPos;
               //var _scaleY = ((_sizeStart.y + _pointerTravel.y) / _sizeStart.y) * _scaleStart.y;
               //transform.localScale = new Vector3(transform.localScale.x, _scaleY, transform.localScale.z);

            }
            else if (Input.GetMouseButtonUp(0))
            {  // finish
                Vector2 endPos = transform.position;
                Vector2 newForce = (Vector3)Input.mousePosition - startPos;

                // plan 1
                //if (newForce.magnitude < threshold) {  // acceptable force
                //    _validHits += 1;  // count valid hit 

                //    switch (drageMode) {
                //        case DragMode.forward:
                //            rb.AddForce(newForce * power);
                //            break;
                //        case DragMode.backward:
                //            rb.AddForce(-1.0f * newForce * power);
                //            break;
                //    }
                //}

                // plan 2
                if (newForce.magnitude > threshold)
                {  // acceptable force
                    newForce = Vector3.ClampMagnitude(newForce, threshold);
                }

                _validHits += 1;  // count valid hit 

                switch (drageMode)
                {
                    case DragMode.forward:
                        rb.AddForce(newForce * power);
                        break;
                    case DragMode.backward:
                        rb.AddForce(-1.0f * newForce * power);
                        break;
                }
            }
        }
    }

    private void SwapDragMode()
    {
        if (drageMode == DragMode.backward)
        {
            drageMode = DragMode.forward;
        }
        else
        {
            drageMode = DragMode.backward;
        }
        PlayerPrefs.SetInt("dragmode", (int)drageMode);
        PlayerPrefs.Save();
    }

    public int ValidHits {
        get { return _validHits; }
        set { _validHits = value; }
    }

}

public enum DragMode {
    forward, backward
}

