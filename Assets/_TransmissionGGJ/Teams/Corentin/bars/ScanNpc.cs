using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanNpc : MonoBehaviour {

    public Image m_knowledgeBar;
    public Image m_diseaseBar;
    public Image m_GreenArrow;
    public Image m_RedArrow;
    private float m_previousKnowledgeLvl;
    private float m_previousDiseaseLvl;
    private TransmissionManager m_transmissionManager;
    void Awake ()
    {
       m_transmissionManager = gameObject.GetComponentInParent<TransmissionManager>();
    }

	void Update ()
    {
        if (m_previousKnowledgeLvl < m_transmissionManager.m_knowledge)
        {
            m_knowledgeBar.fillAmount = m_transmissionManager.m_knowledge * 0.001f;
            m_GreenArrow.fillAmount = 1f;
            m_previousKnowledgeLvl = m_transmissionManager.m_knowledge;
        }
        else
        {
            m_GreenArrow.fillAmount = 0f;
        }
        if (m_previousDiseaseLvl < m_transmissionManager.m_disease)
        {
            m_diseaseBar.fillAmount = m_transmissionManager.m_disease * 0.001f;
            m_RedArrow.fillAmount = 1f;
            m_previousDiseaseLvl = m_transmissionManager.m_disease;
        }
        else
        {
            m_RedArrow.fillAmount = 0f;
        }
        
        
    }
}
