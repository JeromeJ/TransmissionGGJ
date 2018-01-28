using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMoveVirus : MonoBehaviour
{

    public float speed = 10f;
    private Vector3 m_randomRotation;
    private List<Vector3> m_randomRotationList = new List<Vector3>();

    private Transform m_transform;
    private List<Vector3> m_startPosition = new List<Vector3>();
    private List<Vector3> m_randomMoveList = new List<Vector3>();

    public float _x = 1;
    public float _y = 1;
    public float _z = 1;

    public List<Transform> m_virusT = new List<Transform>();

    private void Start()
    {
        for (int i=0; i< m_virusT.Count;i++)
        {
            m_randomRotationList.Add(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            m_startPosition.Add(m_virusT[i].position);
            m_randomMoveList.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        }
        
    }
    
    void Update()
    {
        for (int i = 0; i < m_virusT.Count; i++)
        {
            RotationVirus(i);
            FloatingLikeASinus(i);
        }
    }

    public void RotationVirus(int i)
    {
        m_virusT[i].transform.Rotate(m_randomRotationList[i], speed * Time.deltaTime);
    }

    public void FloatingLikeASinus(int i)
    {
        m_virusT[i].position = m_startPosition[i] + new Vector3(
            Mathf.Sin(Time.time) * _x * m_randomMoveList[i].x, 
            Mathf.Cos(Time.time) * _y * m_randomMoveList[i].y,  
            Mathf.Cos(Time.time) * Mathf.Sin(Time.time) * _z * m_randomMoveList[i].z);
        

        //README
        //ca marche neil ! le problème c'est qu'on faisait un random dans la fonction appelée a chanque frame ce qui fait que chaque frame le 
        //même objet bouge vers une direction aléatoire (qui change a chaque frame) en + de son sincostruc de base,
        //ici j'ai fait une liste de randomMove comme ta liste de random rotation pour que chaque objet ait une valeur fixe propre
        //a cet objet et il l'ajoute a son sincostruc, et voila. T'avais la solution avec ton randomrotation, on a juste fait les cons...

    }
}
