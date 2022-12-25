using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int health, maxHealth = 2, healthRegen = 0; // 0 = no health regen

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);

            if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovementComponent)) // testing if the entity is a player
            {
                // transition to a death scene or something
            }
        }

    }

}