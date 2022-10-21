using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    Rigidbody2D rigidbody2d;
    Animator animator;

    bool broken = true;
    public ParticleSystem smokeEffect;

    AudioSource audioSource;
    public AudioClip fixedRobotAudio;
    public AudioClip hitRobotAudio;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the robot has been repaired, it will stop moving
        if(!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * direction * speed;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * direction * speed;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
 
        rigidbody2d.position = position;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if (controller != null && broken == true)
        {
            controller.ChangeHealth(-1);
        }
    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        broken = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Fixed");
        audioSource.PlayOneShot(hitRobotAudio);
        audioSource.clip = fixedRobotAudio;
        audioSource.volume = 0.5f;
        audioSource.Play();
        smokeEffect.Stop();
    }
}
