using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerFPS : MonoBehaviour
{
    public delegate void delegateControllersFps(int nbItem);
    public delegate void delegateControllersFpsNbCollision(int nbCollision);
    public delegate void delegateControllersFpsWin();
    public event delegateControllersFps onControllerFps = (int nbItem) => { };
    public event delegateControllersFpsNbCollision onControllerFpsNbCollision = (int nbCollision) => { };
    public event delegateControllersFpsNbCollision onControllerFpsNbCollisionWall = (int nbCollision) => { };
    public event delegateControllersFpsWin onControllerFpsWin = () => { };
    [SerializeField] float forwardVelocity = 5f;
    [SerializeField] float runningVelocity = 10f;
    //[SerializeField] float strafVelocity = 5f;
    [SerializeField] float sensitivity = 180f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] int nbItemToCollect = 4;
    [SerializeField] AudioClip clip;

    public bool isWin = false;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    float cameraRotation = 0f;
    public int nbItem = 0;
    public int nbCollision = 0;
    public int nbCollisionWall = 0;
    bool isGrounded;
    public int NbItemToCollect
    {
        get
        {
            return nbItemToCollect;
        }
    }

    // Use this for initialization


    void Start()
    {
        nbItem = 0;

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3.0f))
        {
            if (hit.collider.gameObject.tag == "Coin")
            {
                GameObject.Find("Simon").GetComponent<Simon>().IsCorrectItem(hit.collider.gameObject);
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float desiredVelocity = forwardVelocity;
        if (Input.GetButton("Running"))
        {
            desiredVelocity = runningVelocity;
        }
        isGrounded = IsGrounded();

        if (Input.GetButton("Zoom"))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 15f, Time.deltaTime * 6f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 6f);
        }

        if (Input.GetButtonDown("Crounch"))
        {
            capsuleCollider.height = 1f;
            capsuleCollider.center = new Vector3(0f, 0.5f, 0f);
            Camera.main.transform.localPosition = new Vector3(0f, 1f, 0f);
            if (!IsGrounded())
            {
                transform.position += new Vector3(0f, 0.6f, 0f);
            }
        }
        else if (!Input.GetButton("Crounch") && CanWakeUp() && rb.velocity.y <= 0 && isGrounded)
        {
            capsuleCollider.height = 2f;
            capsuleCollider.center = new Vector3(0f, 1f, 0f);
            Camera.main.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            if (!IsGrounded())
            {
                transform.position -= new Vector3(0f, 0.6f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        Vector3 velocity = Input.GetAxis("Vertical") * transform.forward * desiredVelocity;

        velocity += Input.GetAxis("Horizontal") * transform.right * desiredVelocity;

        velocity = Vector3.ClampMagnitude(velocity, desiredVelocity);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpSpeed;
        }
        else
        {
            velocity.y = rb.velocity.y;
        }
        rb.velocity = velocity;

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity);
        cameraRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        cameraRotation = Mathf.Clamp(cameraRotation, -60f, 60f);
        Camera.main.transform.localEulerAngles = new Vector3(cameraRotation, 0f, 0f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position - new Vector3(0f, -0.1f, 0f), 0.25f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + new Vector3(0f, 1.5f, 0f), 0.5f);
    }


    bool IsGrounded()
    {
        return Physics.OverlapSphere(transform.position/* - new Vector3(0f, -0.1f, 0f)*/, 0.25f, 1).Length >= 2;
    }


    bool CanWakeUp()
    {
        return Physics.OverlapSphere(transform.position + new Vector3(0f, 1.5f, 0f), 0.5f, 1 + (1 << 9)).Length == 1;
    }

    IEnumerator ColorObstacles(Collision collision)
    {
        yield return new WaitForSeconds(1);
        collision.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.white;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            AudioSource.PlayClipAtPoint(clip, other.transform.position);
            Destroy(other.gameObject);
            ++nbItem;
            onControllerFps(nbItem);
        }
        if (other.gameObject.tag == "Start")
        {
            if (nbItem == nbItemToCollect)
            {
                isWin = true;
                onControllerFpsWin();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ++nbCollision;
            // Debug.Log("nbcoll" + nbCollision);


            collision.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
            StartCoroutine(ColorObstacles(collision));
            onControllerFpsNbCollision(nbCollision);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            ++nbCollisionWall;
            // Debug.Log("nbcoll" + nbCollision);
            onControllerFpsNbCollisionWall(nbCollisionWall);
        }
    }
}
