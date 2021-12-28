using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private DistanceJoint2D _distanceJoint2D;
    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0,mousePos);
            _lineRenderer.SetPosition(1,transform.position);
            _distanceJoint2D.connectedAnchor = mousePos;
            _distanceJoint2D.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint2D.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (_distanceJoint2D.enabled)
        {
            _lineRenderer.SetPosition(1,transform.position);
        }
    }
}
