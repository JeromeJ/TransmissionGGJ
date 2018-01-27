using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityStandardAssets.Characters.ThirdPerson;
using Random = System.Random;

/// <summary>
/// Current responsibilities:
///     - Move the player around
///     - Don't take any decision except where to stroll to next.
///         -> Move that out? GameManager or Stroller script?
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class CitizenAgent : DualBehaviour
{
    #region Public Members

    #region Config (Required to work)

    public Transform m_nextDestination;

    [Range(1, 10)] // Capped because conversation start from afar (and might end before they join up, if set too big)
    public float m_interactivityRange = 5;
    public InteractiveRange m_isInInteractivityRange = new InteractiveRange();

    public List<Transform> m_pointsOfInterest = new List<Transform>();

    #endregion

    #region Controls

    [Header("Debug")]

    public E_States m_state = E_States.STROLLING;

    #endregion

    #region Utils (what need to be public but is neither config or control)

    public enum E_States
    {
        INVALID = -1,

        STROLLING = 1,
        INTERACTING = 2,
        DEAD = 4,
    }

    // Can't be used via the Inspector (only to give constant value; which we don't want)
    // [System.Serializable]
    public class InteractiveRange : UnityEvent<CitizenAgent> { }

    public StopInteraction m_IsDoneInteracting = new StopInteraction();
    public class StopInteraction : UnityEvent<CitizenAgent> { }

    #endregion

    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        base.Awake();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void Start()
    {
        WarnIfNoPointOfInterestsSet();

        // Initialization
        SwitchToState(m_state);

        SetupInteractivityRangeDetection();
    }

    private void Update()
    {
        ExternallyControlledStateMachine();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Citizens need to be able to collide with each others to enable this trigger :<
        // Is this problematic? Do we need a workaround?
        if(other.tag == "Citizen" && this.tag == "Citizen")
            m_isInInteractivityRange.Invoke(other.GetComponent<CitizenAgent>());
    }

    #endregion

    #region Class Methods

    private void SetupInteractivityRangeDetection()
    {
        // TODO: Find a way to be able to distinguish different trigger source
        // -> Idea: Use children (uuurh)
        SphereCollider sc = gameObject.AddComponent<SphereCollider>();

        sc.isTrigger = true;
        sc.radius = m_interactivityRange;
    }

    private void WarnIfNoPointOfInterestsSet()
    {
        if (m_pointsOfInterest == null || !m_pointsOfInterest.Any())
            Debug.LogError("CitizenAgent: I don't know where to go! (No points of interests set)", this.gameObject);
    }

    private void ExternallyControlledStateMachine()
    {
        switch (m_state)
        {
            case E_States.INVALID:
                Debug.LogError("INVALID STATE", gameObject);
                Debug.Break();

                break;
            case E_States.STROLLING:
                MoveToDestination();

                if(CheckIfArrived())
                    m_nextDestination = PickNextDestination();

                break;
            case E_States.INTERACTING:
                MoveToDestination();

                if (CheckIfArrived())
                    m_transform.LookAt(m_recipient.transform);

                if (ConversationIsOver())
                {
                    m_IsDoneInteracting.Invoke(this);
                    SwitchToState(E_States.STROLLING);
                }

                break;
            case E_States.DEAD:
                break;
            default:
                break;
        }
    }

    private bool ConversationIsOver()
    {
        // Caution: Doesn't use Time /!\
        m_remainingConversationTime = (m_talkUntil - DateTime.UtcNow).TotalSeconds;

        return m_remainingConversationTime < 0;
    }

    private void MoveToDestination()
    {
        MoveToDestination(m_nextDestination.position);
    }

    private void MoveToDestination(Transform _destination)
    {
        MoveToDestination(_destination.position);
    }

    private void MoveToDestination(Vector3 _destination)
    {
        AICharacterControl AIController = GetComponent<AICharacterControl>();
        AIController.SetTarget(_destination);

        //m_navMeshAgent.SetDestination(_destination);
    }

    private bool CheckIfArrived()
    {
        // Source: https://answers.unity.com/answers/746157/view.html
        // Check if we've reached the destination
        if (!m_navMeshAgent.pathPending)
        {
            if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
            {
                return true;

                //if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
                //{
                //    return true;
                //}
            }
        }

        return false;
    }

    private Transform PickNextDestination()
    {
        int pickedAtRandom = rnd.Next(m_pointsOfInterest.Count);

        return m_pointsOfInterest[pickedAtRandom];
    }

    public bool CanSwitchToInteracting()
    {
        return m_state == E_States.STROLLING;

        //switch (m_state)
        //{
        //    case E_States.INVALID:
        //        return false;
        //    case E_States.STROLLING:
        //        return true;
        //    case E_States.INTERACTING:
        //        return false;
        //    case E_States.DEAD:
        //        return false;
        //    default:
        //        return false;
        //}
    }

    public void StartConversationWith(CitizenAgent _recipient, double _conversationLength, bool _initiator = false)
    {
        m_recipient = _recipient;
        m_initiator = _initiator;
        m_conversationLength = _conversationLength;
        SwitchToState(E_States.INTERACTING);
    }

    private void SwitchToState(E_States _state)
    {
        switch (_state)
        {
            case E_States.INVALID:
                break;
            case E_States.STROLLING:
                if (m_nextDestination == null)
                    m_nextDestination = PickNextDestination();

                break;
            case E_States.INTERACTING:
                if (m_recipient == null)
                {
                    Debug.LogError("No m_recipient set to go talk to!", gameObject);

                    // Do this or?
                    _state = E_States.INVALID;
                }
                else
                {
                    if (m_initiator)
                        m_nextDestination = GoTalkTo(m_recipient);
                    else
                    {
                        m_nextDestination = WaitForTalker();
                        m_transform.LookAt(m_recipient.transform);
                    }

                    // DateTimeOffset.Now.ToUnixTimeSeconds() in NET 4.6
                    // m_talkUntil = (Int32)(DateTime.UtcNow.AddSeconds(conversationLength).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    m_talkUntil = DateTime.UtcNow.AddSeconds(m_conversationLength);
                }

                break;
            case E_States.DEAD:
                break;
            default:
                break;
        }

        m_state = _state;
    }

    private Transform GoTalkTo(CitizenAgent m_recipient)
    {
        return m_recipient.transform;
    }

    private Transform WaitForTalker()
    {
        return m_transform;
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #region Serialized

    [SerializeField] public double m_conversationLength;
    [SerializeField] private double m_remainingConversationTime;

    #endregion

    #region "Others"

    /// <summary>
    /// [TODO] Make seedable easily (idea: through ScriptableObject)
    /// </summary>
    static private Random rnd = new Random();

    #endregion

    #region Dynamic

    private NavMeshAgent m_navMeshAgent;

    private CitizenAgent m_recipient;
    private bool m_initiator;
    private DateTime m_talkUntil;

    #endregion

    #endregion
}
