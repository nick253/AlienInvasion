using UnityEngine;

public class ObjectRecieveDamage : MonoBehaviour
{
    public float health = 100f;
    public GameObject deathDrop;
    public Transform playerTransform;

    /// <summary>
    /// This code allows for any object with this script to take damage and die if their health goes below 1.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // if our health fallse equal to or less than 0 we call the die function which deletes the object.
            Die();
        }

    }

    /// <summary>
    /// This function deletes the gameobject when called and also spawns an object with a rigidbody that the player can collect.
    /// </summary>
    void Die ()
    {
            //Destroy the object
            GameObject npcDead = Instantiate(deathDrop, transform.position, transform.rotation);
            //Slightly bounce the dead prefab up
            npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 2f) + new Vector3(0, 5, 0);
            Destroy(gameObject);
    }
}
