using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class ScreenClickPlayerMove : MonoBehaviour {
    
    AICharacterControl m_AIcontroller;

    void Start()
    {
        m_AIcontroller = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.transform.tag == "Citizen")
                {
                    m_followCitizen = true;
                    m_CitizenToFollow = hit.collider.gameObject.transform;
                    m_AIcontroller.SetTarget(m_CitizenToFollow.transform.position);
                }
                else
                {
                    m_followCitizen = false;
                    m_CitizenToFollow = null;
                    m_AIcontroller.SetTarget(hit.point);
                }
            }
        }
        else if (m_followCitizen)
        {
            Debug.Log(m_CitizenToFollow.transform.position);
            m_AIcontroller.SetTarget(m_CitizenToFollow.position);
        }
    }

    private Transform m_CitizenToFollow;
    private bool m_followCitizen;
}
