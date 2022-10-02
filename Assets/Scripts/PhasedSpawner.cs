using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhasedSpawner : PhasedActor
{

   public GameObject prefabToSpawn;

   public float distanceVariation = .2f;
   private float delayBetweenSpawns = .2f;
   public AnimationCurve spawnCountVsLevel = AnimationCurve.Constant(0,4,1);

   private float currentLevel = 0;
   public Phase phaseToSpawn = Phase.NONE;

   protected override void Act(Phase phase)
   {
      if(phase != phaseToSpawn) return;
      int spawnCount = Mathf.CeilToInt(spawnCountVsLevel.Evaluate(currentLevel));

      StartCoroutine(SpawnWithDelay(spawnCount));

   }

   private IEnumerator SpawnWithDelay(int spawnCount)
   {        
      float xVar;
      float yVar;
      
      for (int i = 0; i < spawnCount; i++)
      {
         xVar = Random.Range(-distanceVariation, distanceVariation);
         yVar = Random.Range(-distanceVariation, distanceVariation);
         Instantiate(prefabToSpawn,
            transform.position + Vector3.right * xVar + Vector3.up*yVar,
            prefabToSpawn.transform.rotation);
         yield return new WaitForSeconds(delayBetweenSpawns);
      }
   }
}
