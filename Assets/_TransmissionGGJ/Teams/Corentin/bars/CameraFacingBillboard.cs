using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {

    public Camera m_Camera;

    protected void Awake()
    {
        if(m_Camera == null)
        {
            m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
        m_Camera.transform.rotation * Vector3.up);
    }
}

