using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    /////////public static event Action<EntityHealth> OnEntityKilled; 
    [SerializeField] private int health, maxHealth = 2;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovementComponent)) // testing if the entity is a player
            {
                // transition to a death scene or something

            }

            Destroy(gameObject);
            /////////OnEntityKilled?.Invoke(this);
        }

    }

}