using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityHealth : MonoBehaviour
{
    /////////public static event Action<EntityHealth> OnEntityKilled; 
    [SerializeField] private int currentHealth, maxHealth = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovementComponent)) // testing if the entity is a player
            {
                // transition to a death scene or something
                SceneManager.LoadScene("MainMenu");
            }

            Destroy(gameObject);
            /////////OnEntityKilled?.Invoke(this);
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

}