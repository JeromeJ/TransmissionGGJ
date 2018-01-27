using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenIntegrator : DualBehaviour
{
    #region Public Members

    #endregion

    #region Public void

    #endregion

    #region System

    protected void Start()
    {
        // At the Start or Awake?
        GetComponent<CitizenAgent>().m_isInInteractivityRange.AddListener(
            GetComponent<CitizenManager>().OnPlayerInInteractivityRange
        );

        GetComponent<CitizenManager>().m_IsInteracting.AddListener(callTristanCode);
    }

    private void callTristanCode()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
