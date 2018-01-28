using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Camera))]
public class CameraMove : MonoBehaviour {
    [Header("Required Components")]
    public GameObject m_cameraHolder;
    public Camera m_camera;
    public BoxCollider m_cameraHolderBoxCollider;
    public Rigidbody m_cameraHolderRigidbody;

    public float m_camerSpeed = 100f;

    [Header("zoom")]
    public float m_minZoom = 20f;
    public float m_maxZoom = 40f;
    public float m_zoomSpeed = 30f;
    public float m_colliderXsizeVariationWithZoom = 3.8f;
    public float m_colliderZsizeVariationWithZoom = 2.2f;

    private Vector3 m_velocity = new Vector3(0f, 0f, 0f);
    private Vector3 m_colliderSizeAjustment = new Vector3(0, 0, 0);

    void Awake()
    {
        if(!m_cameraHolder)
        {
            m_cameraHolder = GameObject.Find("CameraHolder");
        }
        if (!m_camera)
        {
            m_camera = gameObject.GetComponent<Camera>();
            gameObject.transform.rotation = Quaternion.Euler(73f, 0f, 0f);
            m_camera.fieldOfView = 40;
        }
        if (!m_cameraHolderBoxCollider)
        {
            if(!m_cameraHolder.GetComponent<BoxCollider>())
            {
                m_cameraHolder.AddComponent<BoxCollider>();
            }
            m_cameraHolderBoxCollider = m_cameraHolder.gameObject.GetComponent<BoxCollider>();
        }
        m_cameraHolderBoxCollider.size.Set(136f, 10f, 74f);
        if (!m_cameraHolderRigidbody)
        {
            m_cameraHolderRigidbody = m_cameraHolder.GetComponent<Rigidbody>();
        }
        m_cameraHolderRigidbody.useGravity = false;
        m_cameraHolderRigidbody.constraints = (RigidbodyConstraints)116;//freeze y position and all rotations
    }

    void Update ()
    {
        HandleInputs();
	}

    private void HandleInputs()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * m_camerSpeed;
        float moveVertical = Input.GetAxis("Vertical") * m_camerSpeed;
        float zoom = Input.GetAxis("Mouse ScrollWheel") * m_zoomSpeed;
        if (moveHorizontal!=0 || moveVertical!=0)
        {
            ApplyMove(moveHorizontal, moveVertical);
        }
        if(zoom!=0)
        {
            ApplyZoom(zoom);
        }
        if (moveHorizontal == 0 && moveVertical ==0 )
        {
            m_velocity.Set(0, 0, 0);
            m_cameraHolderRigidbody.velocity = m_velocity;
        }
    }

    private void ApplyMove(float moveHorizontal, float moveVertical)
    {
        float AdaptMoveSpeedToZoom =  m_camera.fieldOfView / m_maxZoom;
        m_velocity.Set(moveHorizontal * AdaptMoveSpeedToZoom, 0.0f, moveVertical * AdaptMoveSpeedToZoom);
        m_cameraHolderRigidbody.velocity = m_velocity;
    }
    private void ApplyZoom(float zoom)
    {
        if((zoom > 0 && m_camera.fieldOfView > m_minZoom)|| (zoom < 0 && m_camera.fieldOfView < m_maxZoom))
        {
            m_camera.fieldOfView -= zoom;
            float xAdjustment = m_colliderXsizeVariationWithZoom * zoom;
            float zAdjustment = m_colliderZsizeVariationWithZoom * zoom;
            m_colliderSizeAjustment.Set(m_cameraHolderBoxCollider.size.x - xAdjustment,10, m_cameraHolderBoxCollider.size.z - zAdjustment);
            m_cameraHolderBoxCollider.size = m_colliderSizeAjustment;
        }
    }


}
