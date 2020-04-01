using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageReceiver : MonoBehaviour
{
    //This script will keep track of player HP
    public float playerHP = 100;
    public bool WonGame = false;
    public bool gameOver = true;

    void Update()
    {

    }

    /// <summary>
    /// This is called when the player 
    /// </summary>
    /// <param name="points"></param>
    public void ApplyDamage(float points)
    {
        // When called this function damages the player.
        playerHP -= points;

        // If the player's health reaches 0 restart the game
        if (playerHP <= 0)
        {
            //Player is dead
            playerHP = 0;
            gameOver = true;
            SceneManager.LoadScene("MainGameScene");
        }
    }
}