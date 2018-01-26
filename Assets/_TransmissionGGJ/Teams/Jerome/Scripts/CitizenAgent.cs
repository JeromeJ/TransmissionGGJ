using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

using Random = System.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class CitizenAgent : DualBehaviour
{
    #region Public Members

    #region Config (Required to work)

    public float m_interactableBubbleSize = 5;
    public List<Transform> m_pointsOfInterest = new List<Transform>();

    #endregion

    #region Controls

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
    #endregion

    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void Start()
    {
        WarnIfNoPointOfInterestsSet();
    }

    private void Update()
    {
        ExternallyControlledStateMachine();
    }

    #endregion

    #region Class Methods

    private void WarnIfNoPointOfInterestsSet()
    {
        if (m_pointsOfInterest == null || !m_pointsOfInterest.Any())
            Debug.LogWarning("CitizenAgent: I don't know where to go! (No points of interests set)", this.gameObject);
    }

    private void ExternallyControlledStateMachine()
    {
        switch (m_state)
        {
            case E_States.INVALID:
                Debug.LogWarning("INVALID STATE", gameObject);
                Debug.Break();

                break;
            case E_States.STROLLING:
                // Dev note: Would be nice if this could be in some sort of Init separate state
                // instead of running this every frame?
                if (m_nextDestination == null)
                    m_nextDestination = PickNextDestination();

                MoveToDestination();

                if(CheckIfWeArrived())
                    m_nextDestination = PickNextDestination();

                break;
            case E_States.INTERACTING:
                break;
            case E_States.DEAD:
                break;
            default:
                break;
        }
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
        m_navMeshAgent.SetDestination(_destination);
    }

    private bool CheckIfWeArrived()
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

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #region Serialized

    #endregion

    #region "Others"

    /// <summary>
    /// [TODO] Make seedable easily (idea: through ScriptableObject)
    /// </summary>
    static Random rnd = new Random();

    #endregion

    #region Dynamic
    private Transform m_nextDestination;
    private NavMeshAgent m_navMeshAgent;
    #endregion

    #endregion
}
