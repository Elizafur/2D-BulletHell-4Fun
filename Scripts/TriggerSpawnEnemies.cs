using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TriggerSpawnEnemies : MonoBehaviour
{
    [Required,AssetsOnly, PreviewField]
    public GameObject prefabEnemy;

    [Required]
    public Transform spawnLocation;

    public float spawnTime = 1f;

    public int enemySpawnLimit = 1;

    public bool triggerOnlyOnce = true;
    private bool triggered = false;

    [AssetsOnly, PreviewField]
    public GameObject effectPrefab;

    private List<GameObject> children = new List<GameObject>();
    public bool allEnemiesDead = false;
    private bool spawnedEnemies = false;

    public List<GameObject> _PatrolPoints = new List<GameObject>();

    bool ChildrenDead()
    {
        foreach (var x in children)
        {
            if (x != null)
                return false;
        }

        return true;
    }

    void Update()
    {
        if (spawnedEnemies && ChildrenDead())
            allEnemiesDead = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered || !triggerOnlyOnce)
        {
            triggered = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        GameObject o = Instantiate(effectPrefab, spawnLocation);
        ParticleSystem ps = o.GetComponent<ParticleSystem>();
        ps.Play();

        for (int i = 0; i < enemySpawnLimit; ++i)
        {
            
            var enemy = Instantiate(prefabEnemy, spawnLocation);
            PatrolPoints p = enemy.GetComponent<PatrolPoints>();
            MovementHandler m = enemy.GetComponent<MovementHandler>();
            if (_PatrolPoints.Count > 0)    //Don't overwrite patrol points with an empty list if one is not provided in the spawner
            {
                m.usePatrolPoints = true;
                p._PatrolPoints = _PatrolPoints;
            }
            children.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        ps.Stop();
        spawnedEnemies = true;

    }
}
