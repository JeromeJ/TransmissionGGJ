using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DeathManager : MonoBehaviour
{
    TransmissionManager m_transmissionManager;
    private void Start()
    {
        m_transmissionManager = gameObject.GetComponent<TransmissionManager>();
        if (m_transmissionManager != null)
        {
            m_transmissionManager.m_isDead.AddListener(DeathApply);
        }
        else Debug.LogError("DeathManager : Can't find TransmissionManager component");
    }

    public void DeathApply()
    {
        ThirdPersonCharacter _thirdCaracterController = gameObject.GetComponent<ThirdPersonCharacter>();
        if (_thirdCaracterController != null)
        {
            _thirdCaracterController.ListeningInteraction = false;
            _thirdCaracterController.TalkingInteraction = false;
        }
        else Debug.LogError("DeathManager : Can't find ThirdPersonCharacter component");
        

        Animator _animator = gameObject.GetComponent<Animator>();
        if (_animator != null) _animator.SetTrigger("Death");
        else Debug.LogError("DeathManager : Can't find Animator component");


        AICharacterControl _AICharacterControl = gameObject.GetComponent<AICharacterControl>();
        if (_AICharacterControl != null) _AICharacterControl.enabled = false;
        else Debug.LogError("DeathManager : Can't find AICharacterControl component");


        CitizenAgent _citizenAgent = gameObject.GetComponent<CitizenAgent>();
        if (_citizenAgent != null)
        {
            _citizenAgent.SwitchToState(CitizenAgent.E_States.DEAD);
        }
        else Debug.LogError("DeathManager : Can't find CitizenAgent component");

        Collider _collider = gameObject.GetComponent<Collider>();
        if(_collider != null) _collider.enabled = false;
        else Debug.Log("DeathManager : Can't find Collider component");

        Collider[] _colliders = gameObject.GetComponentsInChildren<Collider>();
        if (_colliders != null) foreach (Collider c in _colliders) c.enabled = false;

        m_transmissionManager.m_isDead.RemoveListener(DeathApply);
        m_transmissionManager.enabled = false;
    }
}
