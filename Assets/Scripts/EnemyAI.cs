using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetPos;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani= GetComponent<Animator>(); 
        agent= GetComponent<NavMeshAgent>();
        targetPos = GameObject.Find("EVE").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = targetPos.position;
        if (agent.isStopped)
        {
            ani.SetBool("Run", false);
        }
        else
        {
            ani.SetBool("Run", true);
        }
    }
}
