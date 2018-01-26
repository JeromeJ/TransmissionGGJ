using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour {

    public bool isPlayer;
    [Range(0, 100)]
    public int m_knowledge;
    [Range(0, 100)]
    public int m_disease;
    [Range(0f, 1f)]
    public float m_diseaseReception;
    [Range(0f, 1f)]
    public float m_knowlegeReception;
    public bool m_isCommunicating;

    //Debug
    public TransmissionManager m_contact;
    



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_bufferDisease > 0)
        {
            m_disease += m_bufferDisease;
            m_bufferDisease = 0;
        }

        if (m_isCommunicating)
        {
            m_timeBuffer += Time.deltaTime;
            if(m_timeBuffer > 1f)
            {
                UpdateTransmission();
                m_timeBuffer -= 1f;
            }
        }
        
        //if (m_contact)
        //{
        //    m_knowledge = m_bufferKnowledge;
        //    m_disease = m_bufferDisease;
        //    m_contact = false;
        //    Debug.Log("Contact");
        //}
	}

    void InitiateTransmission(TransmissionManager _contact)
    {
        m_isCommunicating = true;
        m_contact = _contact;
    }

    void UpdateTransmission()
    {
        UpdateKnowledge();
        UpdateDisease();
    }

    void QuitTransmission()
    {
        m_timeBuffer = 0f;
        m_isCommunicating = false;
        m_contact = null;
    }

    void UpdateKnowledge()
    {
        if(m_contact.m_knowledge > m_knowledge)
        {
            m_knowledge += (int)(m_contact.m_knowledge * m_knowlegeReception);
        }
    }

    void UpdateDisease()
    {
        if (m_contact.m_disease > m_contact.m_knowledge)
        {
            m_bufferDisease = (int)((m_contact.m_disease - m_contact.m_knowledge) * m_diseaseReception);
        }
    }


    //void OnCollisionEnter(Collision collision)
    //{
        
        //contactTransmission = collision.gameObject.GetComponent<TransmissionManager>();
        //Debug.Log("Disease contact : " + contactTransmission.m_disease);
        //Debug.Log("Disease self : " + m_disease);

        //if (contactTransmission.m_knowledge > m_knowledge)
        //{
        //    m_bufferKnowledge = contactTransmission.m_knowledge + (int)(m_knowledge * m_ratioTransmissionKnowledge);
        //    Debug.Log(m_bufferKnowledge);
        //}

        //m_bufferDisease = contactTransmission.m_disease + (int)(m_disease * m_ratioTransmissionDisease);
        
        //Debug.Log(""+(int)(m_disease * m_ratioTransmissionDisease));
        //Debug.Log(m_bufferDisease);
        //contactTransmission = null;
        //m_inContact = true;

        //if (contactTransmission.isPlayer)
        //{
        //    // TODO: Lancer un cooldown pour éviter de spammer le knowledge sur les civils
        //}

    //}

    //private TransmissionManager contactTransmission;
    //private int m_bufferKnowledge;
    private int m_bufferDisease;
    //private bool m_inContact;
    private float m_timeBuffer;

}
