using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    protected Transform[] patrolPoints;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("No se encontró NavMeshAgent en " + gameObject.name);
        }
    }
    public virtual void SetPatrolPoints(Transform[] points)
    {
        patrolPoints = points;
    }

    public virtual void DoBehavior()
    {
        //cosas que hagan todos los NPCs
    }

    protected virtual void Update()
    {
        DoBehavior();
    }
}
