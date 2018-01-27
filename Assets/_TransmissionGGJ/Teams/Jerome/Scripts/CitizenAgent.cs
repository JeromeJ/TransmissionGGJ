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

    public List<Transform> m_pointsOfInterest;

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
        StateMachine();
    }

    #endregion

    #region Class Methods

    private void WarnIfNoPointOfInterestsSet()
    {
        if (m_pointsOfInterest == null || !m_pointsOfInterest.Any())
            Debug.LogWarning("CitizenAgent: I don't know where to go! (No points of interests set)", this.gameObject);
    }

    private void StateMachine()
    {
        switch (m_state)
        {
            case E_States.INVALID:
                Debug.LogWarning("INVALID STATE", gameObject);
                Debug.Break();

                break;
            case E_States.STROLLING:
                // Dev note: Would be nice if this could be in some sort of Init separate state
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

    /// <summary>
    /// [TODO] Make seedable easily (idea: through ScriptableObject)
    /// </summary>
    static Random rnd = new Random();

    private NavMeshAgent m_navMeshAgent;

    [SerializeField]
    private enum E_States
    {
        INVALID = -1,

        STROLLING = 1,
        INTERACTING = 2,
        DEAD = 4,
    }

    [SerializeField]
    private E_States m_state = E_States.STROLLING;

    private Transform m_nextDestination;

    #endregion
}
