using UnityEngine;

public class Target : MonoBehaviour
{
    // health of target
    public float health = 1000f;

    public void takeDamage (float amount)
    {
        // take damage from health until target is dead
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // destroy target when it dies
        Destroy(gameObject);
    }
}
