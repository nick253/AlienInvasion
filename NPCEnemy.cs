using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class NPCEnemy : MonoBehaviour
{
    public float attackDistance = 3f;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    // How much damage will npc deal to the player
    public float npcDamage = 5;

    public GameObject npcDeadPrefab;

    public GameObject scoreUI;
    public ScoreManager scoreManager;

    // [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    // public EnemySpawner es;
    NavMeshAgent agent;

    Rigidbody r;

    private Animator animator;

    public GameObject playerToKill;
    DamageReceiver attackTarget;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance - .5f;
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        r.useGravity = false;
        animator.SetBool("isRunning", true);
        if (playerToKill == null) {
            playerToKill = GameObject.FindGameObjectWithTag("Player");
        }
        if (scoreUI == null)
        {
            scoreUI = GameObject.FindGameObjectWithTag("ScoreManager");
        }
        scoreManager = scoreUI.GetComponent<ScoreManager>();
        attackTarget = playerToKill.GetComponent<DamageReceiver>();

}

    // Update is called once per frame
    void Update()
    {
        // Move towards the player
        agent.destination = playerTransform.position;
        // Always look at player
        transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
        // Gradually reduce rigidbody velocity if the force was applied by the bullet
        r.velocity *= 0.99f;

    }

    private float stayCount = 0.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // if the enemy is within the players collider that is tagged "Player" it will switch to the attacking animation and start to damage the player by calling the players ApplyDamage() function.
            if (stayCount > 0.25f)
            {
                animator.SetBool("isAttacking", true);
                attackTarget.ApplyDamage(1);
                // Debug.Log("staying");
                stayCount = stayCount - 0.25f;
            }
            // if the enemy moves outside the player collider then the attacking animation will stop playing
            else
            {
                stayCount = stayCount + Time.deltaTime;
                animator.SetBool("isAttacking", false);
            }
        }

    }

    /// <summary>
    /// This function is called when it is time for the NPCEnemy to take damage.
    /// </summary>
    /// <param name="points"></param>
    public void ApplyDamage(float points)
    {
        npcHP -= points;
        StartCoroutine(TakeDamage());
        if (npcHP <= 0)
        {
            // Tell the snimator to switch to the isDead state.
            animator.SetBool("isDead", true);
            // Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            // Slightly bounce the npc dead prefab up
            npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            //Destroy(npcDead, 10);
            scoreManager.addScore(100);
            // Add score for eliminating an enemy to the players total score.
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///  This cooroutine handles what happens when the enemy takes damage.
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeDamage()
    {
        // Here the isHit animation is being played.
        animator.SetBool("isHit", true);
        // The agentys speed is temporarily reduced
        agent.speed = (agent.speed/2);
        yield return new WaitForSeconds(.5f);
        // Here the agents speed is incrased back up again but as the enemy gets closer to dieing it's speed will get slower.
        agent.speed = (agent.speed * 1.9f);
        // End the isHit animnation
        animator.SetBool("isHit", false);
    }

}