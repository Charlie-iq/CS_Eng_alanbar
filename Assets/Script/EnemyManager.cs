using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyTOSpawn1;
    public Enemy enemyTOSpawn2;
    void Start()
    {
        InvokeRepeating(nameof(SpowNewEnemy), 1f, 1f);
        
    }

    void SpowNewEnemy()
    {
        var randoValue = Random.Range(0, 10);
        if (randoValue > 5)
        {
            var randomX = Random.Range(enemyTOSpawn1.minX, enemyTOSpawn1.maxX);
            var pos = new Vector3(randomX, 0, 0);
            Instantiate(enemyTOSpawn1, pos + transform.position, Quaternion.identity, transform);
        }
        else
        {
            var randomX = Random.Range(enemyTOSpawn2.minX, enemyTOSpawn2.maxX);
            var pos = new Vector3(randomX, 0, 0);
            Instantiate(enemyTOSpawn2, pos + transform.position, Quaternion.identity, transform);
        }
    }
}
