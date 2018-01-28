using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DeathManager : MonoBehaviour
{
    public void DeathApply()
    {
        /*
        ThirdPersonCharacter _thirdCaracterController = gameObject.GetComponent<ThirdPersonCharacter>();
        _thirdCaracterController.ListeningInteraction = false;
        _thirdCaracterController.TalkingInteraction = false;
        */
        Animator _animator = gameObject.GetComponent<Animator>();
        if (_animator != null) _animator.SetTrigger("Death");
        else Debug.LogError("DeathManager : Can't find Animator component");

        AICharacterControl _AICharacterControl = gameObject.GetComponent<AICharacterControl>();
        if (_AICharacterControl != null) _AICharacterControl.enabled = false;
        else Debug.LogError("DeathManager : Can't find AICharacterControl component");

        CitizenAgent _citizenAgent = gameObject.GetComponent<CitizenAgent>();
        if (_citizenAgent != null)
        {
            //_citizenAgent.enabled = false;
            _citizenAgent.m_state = CitizenAgent.E_States.DEAD;
        }
        else Debug.LogError("DeathManager : Can't find CitizenAgent component");
        
        TransmissionManager _transmissionManager = gameObject.GetComponent<TransmissionManager>();
        if (_transmissionManager != null)
        {
            _transmissionManager.enabled = false;
        }
        else Debug.LogError("DeathManager : Can't find TransmissionManager component");

    }
}
