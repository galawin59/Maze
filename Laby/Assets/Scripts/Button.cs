using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool doorIsOpen = false;
    bool needToOpenDoor = false;
    float timerOpenDoor;
    float timerLerpButon;
    bool butonIsPressed;
    Vector3 posButon;
    Vector3 posButonPressed;
    [SerializeField] GameObject door;
    [SerializeField] float heightDoor = 4.9f;
    [SerializeField] AudioClip clip;
    // Use this for initialization
    void Start()
    {
        posButon = transform.localScale;
        posButonPressed = posButon;
        posButonPressed.z = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (butonIsPressed)
        {
            timerLerpButon += Time.deltaTime * 3f;
            if (timerLerpButon >= 1f)
            {
                butonIsPressed = false;
            }
        }
        else
        {
            //si le button ressort
            //timerLerpButon -= Time.deltaTime;
        }
        timerLerpButon = Mathf.Clamp(timerLerpButon, 0f, 1f);
        transform.localScale = Vector3.Lerp(posButon, posButonPressed, timerLerpButon);
        if (needToOpenDoor)
        {
            timerOpenDoor += Time.deltaTime;
            door.transform.Translate(Vector3.down * heightDoor * Time.deltaTime / 2f);
            if (timerOpenDoor >= 2)
            {
                needToOpenDoor = false;
                doorIsOpen = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F) && Vector3.Dot(transform.forward, other.transform.forward) >= 0.6 && timerOpenDoor == 0f)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            needToOpenDoor = true;
            butonIsPressed = true;
            timerLerpButon = 0f;
        }
    }
}
