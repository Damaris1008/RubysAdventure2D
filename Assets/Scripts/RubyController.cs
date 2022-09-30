using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //As this variable is public, it will appear in the Unity Inspector window of Ruby
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; }}

    public float timeInvincible = 1.45f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal; 
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 position = rigidbody2d.position;
        //Multiplying by Time.deltaTime makes the character movement be the same regardless of how many frames per second are used to play the game
        position.x = position.x + speed * horizontal * Time.deltaTime; //Time.deltaTime is the time Unity takes to reproduce a frame
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.position = position;

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        //This assures that the currentHealth will never be less than 0 or greater than maxHealth
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UnityEngine.Debug.Log(currentHealth + "/" + maxHealth);
    }
}
