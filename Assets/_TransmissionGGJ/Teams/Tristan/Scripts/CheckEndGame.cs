using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("CheckIfEnd");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CheckIfEnd()
    {
        while (!m_isEnd)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("check");
        }
    }

    private bool m_isEnd = false;

}
