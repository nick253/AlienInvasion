using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaserGun : MonoBehaviour
{

    public GameObject laserGunPrefab;
    
    /// <summary>
    /// This functions spawns a laser gun that the player can pickup.
    /// </summary>
    public void spawnLaserGun()
    {
        print("spawned laser gun");
        GameObject laserGun = Instantiate(laserGunPrefab, transform.position, transform.rotation);
    }
}
