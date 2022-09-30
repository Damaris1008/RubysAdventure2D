using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        
        //We are testing that the gameObject which entered the trigger is Ruby and not an enemy
        if(rubyController != null)
        {
            if(rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(1);
                Destroy(gameObject);
            }

        }
    }
}
