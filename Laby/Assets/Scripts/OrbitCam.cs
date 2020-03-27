using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCam : MonoBehaviour
{
    Vector2 last_pos;
    Vector2 centerScreen;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform tr;

    private void Start()
    {
        //  gc = GameObject.Find("@Board").GetComponent<GameController>();
        centerScreen.x = Screen.width * .5f;
        centerScreen.y = Screen.height * .5f;
        transform.LookAt(tr);
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(tr.position, Vector3.up, speed * 200);
        // transform.RotateAround(tr.position, Vector3.left, v.y * speed);
        transform.LookAt(tr);
    }
}