using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndGame : MonoBehaviour {

    public TransmissionManager m_transmissionManager;

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
        foreach(TransmissionManager transmissionStatus in m_transmissionStatusList)
        {
            if (transmissionStatus.m_disease < 1000 && transmissionStatus.m_disease >= transmissionStatus.m_knowledge)
            {
                return false;
            }
        }
        return true;
    }

    void DisplayCasualties()
    {
        int DeathCount = 0;
        foreach (TransmissionManager transmissionStatus in m_transmissionStatusList)
        {
            if (transmissionStatus.m_disease == 1000)
            {
                DeathCount++;
            }
        }
        Debug.Log("Casualties: " + DeathCount);
    }

    IEnumerator LoopTestEnding()
    {
        bool _isEnd = false;
        while (!_isEnd)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("check");
            _isEnd = TestEnding();
        }
        Debug.Log("End Game");
        DisplayCasualties();
    }


    private GameObject[] m_citizenList;
    List<TransmissionManager> m_transmissionStatusList = new List<TransmissionManager>();

    private bool m_isEnd = false;

}
