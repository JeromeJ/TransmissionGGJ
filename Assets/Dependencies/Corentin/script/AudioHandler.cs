using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {
    public AudioSource m_audioSourceChatter;
    public AudioSource m_audioSourceCough;
    [SerializeField]
    List<AudioClip> m_listCoughClip = new List<AudioClip>();
    private bool m_canPlay = true;
    public void ChangeVolumeTo(float volume)
    {
        m_audioSourceChatter.volume = volume;
    }
    void Update()
    {
        if(m_canPlay)
        {
            StartCoroutine(Cough());
        }
    }
    IEnumerator Cough()
    {
        m_canPlay = false;
        int randomTime = Random.Range(5, 25);
        yield return new WaitForSeconds(randomTime);
        TriggerCough();
        m_canPlay = true;
    }
    public void TriggerCough()
    {
        int randomSoundIndex = (int)Random.Range(0, m_listCoughClip.Count);
        m_audioSourceCough.clip = m_listCoughClip[randomSoundIndex];
        m_audioSourceChatter.volume = Random.Range(0.2f, 0.8f);
        m_audioSourceCough.Play();
    }
}
