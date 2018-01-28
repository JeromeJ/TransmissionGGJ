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
    private bool m_greenArrowCoroutineOngoing;
    private bool m_redArrowCoroutineOngoing;
    void Awake ()
    {
       m_transmissionManager = gameObject.GetComponentInParent<TransmissionManager>();
        if(m_transmissionManager == null)
        {
            Debug.Log(gameObject.GetComponentInParent<TransmissionManager>());
        }
    }

    IEnumerator WaitAsecThenhideArrow (Image img)
    {
        if(img.name=="GreenArrow")
        {
            m_greenArrowCoroutineOngoing = true;
            yield return new WaitForSeconds(1);
            img.fillAmount = 0f;
            m_greenArrowCoroutineOngoing = false;
        }
        else
        {
            m_redArrowCoroutineOngoing = true;
            yield return new WaitForSeconds(1);
            img.fillAmount = 0f;
            m_redArrowCoroutineOngoing = false;
        }
        
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
            if (!m_greenArrowCoroutineOngoing)
            {
                StartCoroutine(WaitAsecThenhideArrow(m_GreenArrow));
            }
            
        }
        if (m_previousDiseaseLvl < m_transmissionManager.m_disease)
        {
            m_diseaseBar.fillAmount = m_transmissionManager.m_disease * 0.001f;
            m_RedArrow.fillAmount = 1f;
            m_previousDiseaseLvl = m_transmissionManager.m_disease;
        }
        else
        {
            if (!m_redArrowCoroutineOngoing)
            {
                StartCoroutine(WaitAsecThenhideArrow(m_RedArrow));
            }
            
        }
        
        
    }
}
