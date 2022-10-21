using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    
    public ParticleSystem healEffect;

    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other) 
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        
        //We are testing that the gameObject which entered the trigger is Ruby and not an enemy
        if(rubyController != null)
        {
            if(rubyController.health < rubyController.maxHealth)
            {
                Instantiate(healEffect, other.gameObject.transform.position + other.gameObject.transform.up * 0.5f, other.gameObject.transform.rotation);
                rubyController.ChangeHealth(1);
                Destroy(gameObject);
                rubyController.PlaySound(collectedClip, 1.0f);
            }

        }
    }
}
