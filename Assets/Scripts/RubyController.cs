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
    Vector2 lookDirection = new Vector2(0,-1);

    public GameObject projectilePrefab;

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

        Vector2 move = new Vector2(horizontal, vertical);
        
        //If Ruby is moving
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
                
        //We are sending the variables to the animator
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        //Multiplying by Time.deltaTime makes the character movement be the same regardless of how many frames per second are used to play the game
        //Time.deltaTime is the time Unity takes to reproduce a frame
        position = position + move * speed * Time.deltaTime;
        rigidbody2d.position = position;

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
                
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        //This assures that the currentHealth will never be less than 0 or greater than maxHealth
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UnityEngine.Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch()
    {
        //Quaternion.identity means no rotation
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
