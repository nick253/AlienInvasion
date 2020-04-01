using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class RoverCar : MonoBehaviour
{

    NavMeshAgent rover;
    public Transform targetToFollow;
    public GameObject player;
    public Transform shipLocation;

    // Start is called before the first frame update
    void Start()
    {
        rover = GetComponent<NavMeshAgent>();
        if (targetToFollow == null)
        {
            targetToFollow = player.transform;
        }
        PauseAgent();
    }

    // Update is called once per frame
    void Update()
    {

        if (rover.isStopped == false)
        {
            // Here I am delaing the Agents destination location by 5 seconds.
            Invoke("UpdateDestination", 5);
        }
    }

    /// <summary>
    /// Pauses and Resumes the Agent when called.
    /// Called by Pause/Resume button
    /// </summary>
    public void PauseResume()
    {
        if (rover.isStopped == false)
        {
            PauseAgent();
        }
        else
        {
            ResumeAgent();
        }
    }

    /// <summary>
    /// This pauses the agent
    /// </summary>
    public void PauseAgent()
    {
        rover.isStopped = true;
        rover.angularSpeed = 0;
        print("rover paused");
    }

    /// <summary>
    /// This resumes the agent
    /// </summary>
    public void ResumeAgent()
    {
        rover.isStopped = false;
        rover.angularSpeed = 100;
        print("rover resumed");
    }


    /// <summary>
    /// This function is for calling the invoke method to delay the agents destination update by 5 seconds.
    /// </summary>
    private void UpdateDestination()
    {
        //Move towards the player
        rover.destination = targetToFollow.position;
        //Always look at player
        transform.LookAt(new Vector3(targetToFollow.transform.position.x, transform.position.y, targetToFollow.position.z));
    }

    /// <summary>
    /// This function is called on a button that the player can press which will send the rover back to the ship to unload the objects for points.
    /// </summary>
    public void ReturnToShip()
    {
        ResumeAgent();
        //Move towards the Sip for points
        rover.destination = shipLocation.position;
        //Always look at player
        transform.LookAt(new Vector3(shipLocation.transform.position.x, transform.position.y, shipLocation.position.z));
    }

    /// <summary>
    /// When the rover reaches the ship it will unload the collected objects for points. Then call the Return to Player coroutine and start moving toward the player again.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        //Make sure we are colliding with a SpawnTrigger.
        if (collision.gameObject.CompareTag("ReturnLocation"))
        {
            //wait 2 seconds before returning to the player.
            StartCoroutine(ReturnToPlayer(7));
        }

        //Make sure we are colliding with a SpawnTrigger.
        if (collision.gameObject.CompareTag("Player"))
        {
            PauseAgent();
        }

    }

    void OnTriggerStay(Collider collision)
    {
        //Make sure we are colliding with a SpawnTrigger.
        if (collision.gameObject.CompareTag("Collectable"))
        {

        }

    }

    /// <summary>
    /// The goal of this coroutine is so that the rover fully enters the Return location. It will wait for 2 seconds before returning.
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnToPlayer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Move towards the player
        rover.destination = targetToFollow.position;
        //Always look at player
        transform.LookAt(new Vector3(targetToFollow.transform.position.x, transform.position.y, targetToFollow.position.z));
    }
}
