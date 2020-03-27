using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollisions : MonoBehaviour
{
    public AudioClip[] audioClips;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall"|| collision.transform.tag == "Obstacle")
        {
            float dotFoward = Vector3.Dot(transform.forward, collision.contacts[0].normal);
            float dotRight = Vector3.Dot(transform.right, collision.contacts[0].normal);
            float dotLeft = Vector3.Dot(-transform.right, collision.contacts[0].normal);
            float dotBack = Vector3.Dot(-transform.forward, collision.contacts[0].normal);

            if(dotFoward < dotRight && dotFoward < dotLeft && dotFoward < dotBack)
            {

                AudioSource.PlayClipAtPoint(audioClips[0], collision.contacts[0].point);
                Debug.Log("forward");
            }
            else if (dotRight < dotFoward && dotRight < dotLeft && dotRight < dotBack)
            {
                AudioSource.PlayClipAtPoint(audioClips[1], collision.contacts[0].point);
                Debug.Log("right");
            }
            else if(dotLeft < dotRight && dotLeft < dotFoward && dotLeft < dotBack)
            {
                AudioSource.PlayClipAtPoint(audioClips[2], collision.contacts[0].point);
                Debug.Log("left");
            }
            else
            {
                AudioSource.PlayClipAtPoint(audioClips[3], collision.contacts[0].point);
                Debug.Log("back");
            }
        }

    }
}
