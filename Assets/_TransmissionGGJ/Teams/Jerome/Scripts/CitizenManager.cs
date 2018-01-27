using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CitizenManager : DualBehaviour
{
    #region Public Members

    public UnityEvent m_IsInteracting = new UnityEvent();

    #endregion

    #region Public void

    public void OnPlayerInInteractivityRange(GameObject _recipient)
    {
        // Who stores the citizenInfo? -> Not me
        // What does your function want to receive?
        //      -> Each citizen get triggered (also need to call them off after a while when ending the conversation)
        //      -> Needs to know who it's talking to

        //if (COMMUNICATION)
        //{
        //    _recipient.state = INTERACTING
        //    m_IsInteracting.Invoke();
        //}

        Debug.Log(this.name + " in range with " + _recipient.name, _recipient);
    }

    #endregion

    #region System

    protected override void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
