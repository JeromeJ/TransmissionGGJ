using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BCWYTManager : DualBehaviour
{
    #region Public Members

    public int m_citizenCount = 3;

    public GameObject m_avatar;

    public List<Transform> m_citizenSpawnPoints = new List<Transform>();
    public List<Transform> m_pointsOfInterest = new List<Transform>();
    //public List<Transform> m_playerSpawnPoints = new List<Transform>();

    #endregion

    #region Public void

    #endregion

    #region System

    private void Start()
    {
        SpawnCitizen();

        // Hardcoded in the scene :) Easier, faster, etc
        //SpawnPlayer();
    }

    private void SpawnCitizen()
    {
        //// DEV NOTE: -1 because we HAVE TO use an instance (already existing in the scene) and NOT a prefab
        //// Still throws 3 errors but the code still work... somehow
        //for (int i = 0; i < m_citizenCount - 1; i++)
        for (int i = 0; i < m_citizenCount; i++)
        {
            Transform pointOfInterest = PickAtRandom(m_pointsOfInterest);

            GameObject citizen = Instantiate(m_avatar, pointOfInterest.position, pointOfInterest.rotation);

            // Copy or reference?
            citizen.GetComponent<CitizenAgent>().m_pointsOfInterest = m_pointsOfInterest;
        }
    }

    private void SpawnPlayer()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    private T PickAtRandom<T>(List<T> _list)
    {
        int pickedAtRandom = rnd.Next(_list.Count);

        return _list[pickedAtRandom];
    }

    #endregion

    #region Private and Protected Members

    /// <summary>
    /// [TODO] Make seedable easily (idea: through ScriptableObject)
    /// </summary>
    static private Random rnd = new Random();

    #endregion
}
