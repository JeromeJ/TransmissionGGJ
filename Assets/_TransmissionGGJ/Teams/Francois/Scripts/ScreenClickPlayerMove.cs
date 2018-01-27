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
                m_AIcontroller.SetTarget(hit.point);
            }
        }
    }
}
