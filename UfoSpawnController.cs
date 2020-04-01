using System.Collections;
using UnityEngine;
using SWS;
using DG.Tweening;


public class UfoSpawnController : MonoBehaviour
{
    public splineMove ufo;
    public MeshRenderer tractorBeam;

    Animator tracktorBeamAnimator;
    public GameObject tracktorBeamCapsule;

    public bool isSpawning = false;
    private GameObject spawnPoint;
    private SpawnEnemies spawn;


    // Start is called before the first frame update
    void Start()
    {
        tracktorBeamAnimator = tracktorBeamCapsule.GetComponent<Animator>();
    }

    /// <summary>
    /// This spawns enemies when the ufo's tractor beam collides with an object tagged "SpawnTrigger".
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        //Make sure we are colliding with a SpawnTrigger.
        if (collision.gameObject.CompareTag("SpawnTrigger"))
        {
            //This allows enemies to only be spawned when it is time to spawn enemies
            if (isSpawning == true)
            {
                spawnPoint = collision.gameObject;
                spawn = spawnPoint.GetComponent<SpawnEnemies>();
                // Invoking to delay the coroutine by half a second so that the tractor beam is aligned with the spawn location.
                Invoke("startSpawnRoutine", .5f);
                print("spawn has been triggered");
            }
        }
    }

    private float stayCount = 0.0f;

    private void OnTriggerStay(Collider other)
    {
        if (stayCount > 0.25f)
        {
            //Debug.Log("staying");
            stayCount = stayCount - 0.25f;
        }
        else
        {
            stayCount = stayCount + Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "SpawnTrigger")
        {
        }
    }

    void startSpawnRoutine()
    {
        StartCoroutine(UfoSpawn());
    }

    /// <summary>
    /// This copntrols on the order of events for the UFO spawner. enables and disables the tracktor beam mesh and plays and stops the animation for the tracktor beam.
    /// </summary>
    /// <returns></returns>
    IEnumerator UfoSpawn()
    {
        ufo.Pause();
        yield return new WaitForSeconds(.5f);
        tractorBeam.enabled = true;
        tracktorBeamAnimator.SetBool("isUp", false);
        //Spawn Enemy
        spawn.SpawnEnemy();
        yield return new WaitForSeconds(1f);
        tracktorBeamAnimator.SetBool("isUp", true);
        tracktorBeamAnimator.SetBool("isUp", false);
        //Spawn Enemy
        spawn.SpawnEnemy();
        yield return new WaitForSeconds(1f);
        tracktorBeamAnimator.SetBool("isUp", true);
        yield return new WaitForSeconds(1f);
        tracktorBeamAnimator.SetBool("isUp", false);
        //Spawn Enemy
        spawn.SpawnEnemy();
        yield return new WaitForSeconds(1f);
        tracktorBeamAnimator.SetBool("isUp", true);
        tractorBeam.enabled = false;
        yield return new WaitForSeconds(.5f);
        ufo.Resume();
    }

}


