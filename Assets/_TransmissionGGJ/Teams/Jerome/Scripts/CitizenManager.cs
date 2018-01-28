using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Random = System.Random;

public class CitizenManager : DualBehaviour
{
    #region Public Members

    public float m_minConversationTime = 3f;
    public float m_maxConversationTime = 30f;

    /// <summary>
    /// Currently a constant but may interact with other factors later on.
    /// </summary>
    [Range(0, 100)]
    public int m_probabilityToInteract = 70;

    public Interaction m_IsInteracting = new Interaction();
    public class Interaction : UnityEvent<CitizenAgent> { }

    #endregion

    #region Public void

    public void OnPlayerInInteractivityRange(CitizenAgent _recipient)
    {
        // Keep giving a CitizenAgent or give a GameObject and dynamically retrieve the Agent (More costly)

        // Who stores the citizenInfo? -> The transmission manager
        // What does the transmission manager want function want to receive?
        //      -> Each citizen get triggered (also need to call them off after a while when ending the conversation)
        //      -> Needs to know who it's talking to

        //Debug.Log(this.name + " in range with " + _recipient.name, _recipient);

        if (InitiateConversation(_recipient))
        {
            // Random length between minConversationTime and maxConversationTime
            double conversationLength = rnd.NextDouble() * (m_maxConversationTime - m_minConversationTime) + m_minConversationTime;

            m_citizenAgent.StartConversationWith(_recipient, conversationLength, _initiator: true);
            _recipient.StartConversationWith(m_citizenAgent, conversationLength);

            m_IsInteracting.Invoke(_recipient);
        }
    }

    #endregion

    #region System

    protected override void Awake()
    {
        m_citizenAgent = GetComponent<CitizenAgent>();
    }

    #endregion

    #region Class Methods

    private bool InitiateConversation(CitizenAgent _recipient)
    {
        // One of the participant is busy or dead :<
        if (!_recipient.CanSwitchToInteracting() || !m_citizenAgent.CanSwitchToInteracting())
            return false;

        int r = rnd.Next(100);

        if (r < m_probabilityToInteract)
            return true;
        else
            return false;
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    /// <summary>
    /// [TODO] Make seedable easily (idea: through ScriptableObject)
    /// </summary>
    static private Random rnd = new Random();
    private CitizenAgent m_citizenAgent;

    #endregion
}
