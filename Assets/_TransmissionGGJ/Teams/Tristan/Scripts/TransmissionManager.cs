using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour {

    public bool isPlayer;
    [Range(0, 1000)]
    public float m_knowledge;
    [Range(0, 1000)]
    public float m_disease;
    [Range(0f, 1f)]
    public float m_diseaseReception;
    [Range(0f, 1f)]
    public float m_knowledgeReception;
    public int[] m_levelStep = new int[10];

    //Debug
    public TransmissionManager m_contact;
    

    // Use this for initialization
    void Start () {
        m_levelKnowledge = 1;
        m_levelDisease = 1;
	}
	
	// Update is called once per frame
	void Update () {
        // Set Disease count calculated from previous frame
        if (m_bufferDisease > 0)
        {
            m_disease += m_bufferDisease;
            m_bufferDisease = 0;
            if (m_disease >= 1000)
            {
                m_disease = 1000f;
                //TODO: die!
            }
        }

        CheckLevel();

        // If isCommunating, Process the transmission
        if (m_isCommunicating)
        {
            m_timeBuffer += Time.deltaTime;
            if(m_timeBuffer > 1f)
            {
                UpdateTransmission();
                m_timeBuffer -= 1f;
            }
        }
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
        if(m_contact.m_levelKnowledge > m_levelKnowledge+1)
            m_knowledge += (m_contact.m_knowledge * m_knowledgeReception);
        if (m_knowledge > 1000) m_knowledge = 1000f;
    }

    void UpdateDisease()
    {
        if (m_contact.m_disease > m_contact.m_knowledge)
            m_bufferDisease = ((m_contact.m_disease - m_contact.m_knowledge) * m_diseaseReception);
    }

    void CheckLevel()
    {
        if ((int)m_knowledge > m_levelStep[m_levelKnowledge])
            m_levelKnowledge++;        
        if ((int)m_disease > m_levelStep[m_levelDisease])
            m_levelDisease++;
    }

    private float m_bufferDisease;
    private float m_timeBuffer;
    [SerializeField] private bool m_isCommunicating;
    [SerializeField] private int m_levelKnowledge;
    [SerializeField] private int m_levelDisease;
}
