using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTargetManager : MonoBehaviour
{
    public static EnemyTargetManager I;
    public List<GameObject> targets;
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("PlayerBuilding").ToList());
    }

    public Transform GetClosestTargetPos(Vector3 damagerPos)
    {
        float dist = Mathf.Infinity;
        int index = -1;
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null){
                float d = (targets[i].transform.position - damagerPos).magnitude;
                if (dist > d)
                {
                    dist = d;
                    index = i;
                }
            }
        }

        if (index != -1)
        {
            return targets[index].transform;
        }

        return null;
    }
}
