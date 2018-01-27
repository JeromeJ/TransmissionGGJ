using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add requirements
public class CitizenIntegrator : DualBehaviour
{
    #region Public Members

    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        m_citizenAgent = GetComponent<CitizenAgent>();
        m_citizenManager = GetComponent<CitizenManager>();
        m_citizenTransmitter = GetComponent<TransmissionManager>();
    }

    protected void Start()
    {
        // At the Start or Awake?
        m_citizenAgent.m_isInInteractivityRange.AddListener(
            m_citizenManager.OnPlayerInInteractivityRange
        );

        m_citizenManager.m_IsInteracting.AddListener(CallTransmitter);
    }

    private void CallTransmitter(CitizenAgent _recipient)
    {
        m_citizenTransmitter.InitiateTransmission(_recipient.gameObject);
        _recipient.GetComponent<TransmissionManager>().InitiateTransmission(gameObject);
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    private CitizenAgent m_citizenAgent;
    private CitizenManager m_citizenManager;
    private TransmissionManager m_citizenTransmitter;

    #endregion
}
