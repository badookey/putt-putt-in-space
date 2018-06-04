using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchh : MonoBehaviour
{
    private Vector2 startPos;
    private Vector3 _sizeStart;
    private Vector3 _scaleStart;
    
    
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {  // start
            
            startPos = (Input.mousePosition);
            _sizeStart = GetComponent<Renderer>().bounds.size;
            _scaleStart = transform.localScale;
            //Vector3 _positionStart = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {  // holding
           //Debug.Log("accumulation: " + ((Vector2)Inpu.mousePosition - startPos));
            gameObject.GetComponent<Renderer>().enabled = true;
            float _pointerTravel = ((Vector2)Input.mousePosition - startPos).magnitude;
                        var _scaleX = ((_sizeStart.x + _pointerTravel) / _sizeStart.x / 5) * _scaleStart.x;
            if (_scaleX < _scaleStart.x)
            {
                _scaleX = _scaleStart.x;
            }
            if (_scaleX > 25)
            {
                _scaleX = 25;
            }
            transform.localScale = new Vector3(_scaleX, transform.localScale.y, transform.localScale.z);
                        //var _positionY = startPos.y + (_pointerTravel.y / 2);
            //transform.position = new Vector3(transform.position.x, _positionY, transform.position.z);
            }
        else if (Input.GetMouseButtonUp(0))
        {  // finish
            //gameObject.SetActive(false);
        transform.localScale = new Vector3(_scaleStart.x,transform.localScale.y, transform.localScale.z);
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
