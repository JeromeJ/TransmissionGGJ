using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CheckEndGame : MonoBehaviour {
    public TransmissionManager m_transmissionManager;
    public Canvas m_gameOverCanvas;
    public Text m_deathCountText;
	// Use this for initialization
	void Start () {
        m_citizenList = GameObject.FindGameObjectsWithTag("Citizen");
        foreach (GameObject citizen in m_citizenList)
        {
            m_transmissionStatusList.Add(citizen.GetComponent<TransmissionManager>());
        }
        StartCoroutine("LoopTestEnding");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool TestEnding()
    {
        //var affected = (from transmissionStatus in m_transmissionStatusList
        //                where transmissionStatus.m_disease < 1000 && transmissionStatus.m_disease >= transmissionStatus.m_knowledge
        //                select transmissionStatus).ToList();

        //return affected.Count > 5;
        int i = 0;
        foreach (TransmissionManager transmissionStatus in m_transmissionStatusList)
        {
            if (transmissionStatus.m_disease < 1000 && transmissionStatus.m_disease >= transmissionStatus.m_knowledge)
            {
                i++;
                if(i>5)
                    return false;
            }
        }
        return true;
    }

    int DisplayCasualties()
    {
        int DeathCount = 0;
        foreach (TransmissionManager transmissionStatus in m_transmissionStatusList)
        {
            if (transmissionStatus.m_disease >= 1000)
            {
                DeathCount++;
            }
        }
        Debug.Log("Casualties: " + DeathCount);
        return DeathCount;
    }

    IEnumerator LoopTestEnding()
    {
        bool _isEnd = false;
        while (!_isEnd)
        {
            yield return new WaitForSeconds(1);
            _isEnd = TestEnding();
        }
        Debug.Log("End Game");
        int deathCount = DisplayCasualties();
        m_gameOverCanvas.gameObject.SetActive(true);
        m_deathCountText.text = "DeathCount : " + deathCount ;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private GameObject[] m_citizenList;
    List<TransmissionManager> m_transmissionStatusList = new List<TransmissionManager>();
}
