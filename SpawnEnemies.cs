using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject playerObject;
    private DamageReceiver player;
    public Transform spawnPoint;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<DamageReceiver>();
    }

    /// <summary>
    /// This function spawns an enemy at a given spawn point when called.
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        NPCEnemy npc = enemy.GetComponent<NPCEnemy>();
        npc.playerTransform = player.transform;
    }
}
