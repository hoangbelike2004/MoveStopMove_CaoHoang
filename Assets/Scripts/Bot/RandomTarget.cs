using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomTarget : Singleton<RandomTarget>
{
    [SerializeField] NavMeshAgent agent;
    public Vector3 R_point_Get(Vector3 strat_point,float radius)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += strat_point;
        NavMeshHit navhit;
        Vector3 Final_pos = Vector3.zero;
        if(NavMesh.SamplePosition(dir,out navhit, radius, 1))
        {
            Final_pos = navhit.position;
        }
        return Final_pos;
    }

}
