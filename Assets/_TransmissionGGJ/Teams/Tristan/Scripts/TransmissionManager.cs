using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour {

    public bool isPlayer;
    [Range(0, 1001)]
    public float m_knowledge;
    [Range(0, 1001)]
    public float m_disease;
    [Range(0f, 1f)]
    public float m_diseaseReception;
    [Range(0f, 1f)]
    public float m_knowledgeReception;
    [Range(0f, 5f)]
    public float m_spreadRadius;
    [Range(0f, 1f)]
    public float m_spreadDangerosity;
    public int[] m_levelStep = new int[10];

    //Debug
    public TransmissionManager m_contact;
    

    // Use this for initialization

    IEnumerator Start()
    {
        m_levelKnowledge = 1;
        m_levelDisease = 1;
        m_collider = gameObject.GetComponent<Collider>();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(m_knowledge < m_disease)
                SpreadDisease();
        }
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

    public void InitiateTransmission(GameObject _citizen)
    {
        m_isCommunicating = true;
        m_contact = _citizen.GetComponent<TransmissionManager>();
    }

    void UpdateTransmission()
    {
        UpdateKnowledge();
        UpdateDisease();
    }

    public void QuitTransmission()
    {
        m_timeBuffer = 0f;
        m_isCommunicating = false;
        m_contact = null;
    }

    void UpdateKnowledge()
    {
        if(m_contact.m_levelKnowledge > m_levelKnowledge+1)
            m_knowledge += (m_contact.m_knowledge * m_knowledgeReception);
        if (m_knowledge > 1001) m_knowledge = 1001f;
    }

    void UpdateDisease()
    {
        if (m_contact.m_disease >= m_contact.m_knowledge)
            m_bufferDisease = ((m_contact.m_disease - m_contact.m_knowledge) * m_diseaseReception);
    }

    void SpreadDisease()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_spreadRadius);
        foreach (Collider _collider in hitColliders)
        {
            if(_collider != m_collider && _collider.gameObject.tag == "Citizen")
            {
                TransmissionManager _spreded =  _collider.gameObject.GetComponent<TransmissionManager>();
                _spreded.m_disease += ((m_disease - m_knowledge) * _spreded.m_diseaseReception * m_spreadDangerosity);
                if (_spreded.m_knowledge > 1001) _spreded.m_knowledge = 1001f;
            }
        }
    }

    void CheckLevel()
    {
        if (m_levelKnowledge < 10 && (int)m_knowledge > m_levelStep[m_levelKnowledge])
            m_levelKnowledge++;        
        if (m_levelDisease < 10 && (int)m_disease > m_levelStep[m_levelDisease])
            m_levelDisease++;
    }

    private float m_bufferDisease;
    private float m_timeBuffer;
    [SerializeField] private bool m_isCommunicating;
    [SerializeField] private int m_levelKnowledge;
    [SerializeField] private int m_levelDisease;
    private Collider m_collider;
}
