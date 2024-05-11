using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dealing damage to player");
            DealDamageToPlayer(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Dealing damage to enemy");
            DealDamageToEnemy(collision.gameObject);
        }

        // Destroy the ball regardless of what it collides with
        //Destroy(gameObject);
    }

    private void DealDamageToPlayer(GameObject player)
    {
        // Check if the player has a health component
        HP playerHP = player.GetComponent<HP>();
        if (playerHP != null)
        {
            // Call the DecreaseHP method to deal 1 damage to player
            playerHP.DecreaseHP(true); // Pass 'true' for player
        }
        else
        {
            Debug.LogWarning("Player does not have a Health Points component.");
        }
    }

    private void DealDamageToEnemy(GameObject enemy)
    {
        // Check if the enemy has a health component
        HP enemyHP = enemy.GetComponent<HP>();
        if (enemyHP != null)
        {
            // Call the DecreaseHP method to deal 1 damage to enemy
            enemyHP.DecreaseHP(false); // Pass 'false' for enemy
        }
        else
        {
            Debug.LogWarning("Enemy does not have a Health Points component.");
        }
    }
}
