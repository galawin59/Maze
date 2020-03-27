using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollision : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] clips;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            /*RaycastHit hit;
            Ray r = new Ray(transform.position, rb.velocity);
            if(Physics.Raycast(r, out hit))
            {*/
            ;


            float dot1 = Vector2.Dot(new Vector2(transform.forward.x, transform.forward.z), new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.z));
          
            Debug.Log("dot1 " + dot1);
            
            audio.PlayOneShot(clips[0]);
            audio.PlayOneShot(clips[1]);
            audio.PlayOneShot(clips[2]);
            audio.PlayOneShot(clips[3]);

        }
    }
}
