using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    [SerializeField] GameObject[] objectToIllumine;
    [SerializeField] Material lightOn;
    [SerializeField] Material lightOff;
    [SerializeField] float timerWait = 2.0f;
    [SerializeField] int nbObjectToIlluminate = 3;
    [SerializeField] GameObject itemToWin;
    [SerializeField] AudioClip[] clips;
    public List<GameObject> orderToCollectItems;
    public float timer;
    bool isPlayerInside = false;
    bool hasFailed = false;
    bool isOrderReady = false;
    bool isResolved = false;
    bool isRealyResolved = false;
    bool isAnimOver = true;
    // Use this for initialization
    void Start()
    {
        orderToCollectItems = new List<GameObject>();
        itemToWin.gameObject.SetActive(false);
        nbObjectToIlluminate = ParametrableManager.Instance.numberOfObjectToIlluminate;
        Debug.Log(nbObjectToIlluminate);
        timerWait = ParametrableManager.Instance.timerObjectStayIlluminate;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside && !isRealyResolved)
        {
            timer += Time.deltaTime;
        }
        if (hasFailed)
        {
            hasFailed = false;
            StartCoroutine(ExtinctAll());
        }
        if (isResolved)
        {
            StartCoroutine(Congratz());
            isResolved = false;
            isRealyResolved = true;
        }
    }

    IEnumerator ExtinctAll()
    {
        for (int i = 0; i < objectToIllumine.Length; i++)
        {
            objectToIllumine[i].GetComponent<Renderer>().material = lightOff;
        }
        orderToCollectItems.Clear();
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(ShowObjectToCollect());
    }

    IEnumerator ShowObjectToCollect()
    {
        for (int i = 0; i < nbObjectToIlluminate; i++)
        {
            int item = Random.Range(0, 5);
            orderToCollectItems.Add(objectToIllumine[item]);
            objectToIllumine[item].GetComponent<Renderer>().material = lightOn;
            yield return new WaitForSeconds(timerWait);
            objectToIllumine[item].GetComponent<Renderer>().material = lightOff;
            yield return new WaitForSeconds(0.5f);
        }
        isOrderReady = true;
    }

    IEnumerator IlluminateObject(bool isCorrect, GameObject go = null)
    {
        if (isCorrect)
        {
            orderToCollectItems[0].GetComponent<Renderer>().material = lightOn;
            orderToCollectItems[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            AudioSource.PlayClipAtPoint(clips[0], orderToCollectItems[0].transform.position);
            orderToCollectItems.RemoveAt(0);
        }
        else
        {
            go.GetComponent<Renderer>().material = lightOn;
            go.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            AudioSource.PlayClipAtPoint(clips[1], orderToCollectItems[0].transform.position);
        }

        yield return new WaitForSeconds(0.5f);
        go.GetComponent<Renderer>().material = lightOff;
        if (isCorrect)
        {
            if (orderToCollectItems.Count == 0)
            {
                isResolved = true;
            }
        }
        else
        {
            hasFailed = true;
            isOrderReady = false;
        }
        isAnimOver = true;
    }

    IEnumerator Congratz()
    {
        AudioSource.PlayClipAtPoint(clips[2], objectToIllumine[2].transform.position);
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < objectToIllumine.Length; i++)
            {
                objectToIllumine[i].GetComponent<Renderer>().material = lightOn;
                objectToIllumine[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < objectToIllumine.Length; i++)
            {
                objectToIllumine[i].GetComponent<Renderer>().material = lightOff;
            }
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < objectToIllumine.Length; i++)
        {
            objectToIllumine[i].GetComponent<Renderer>().material = lightOn;
            objectToIllumine[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }
        itemToWin.gameObject.SetActive(true);
    }

    public void IsCorrectItem(GameObject go)
    {
        if (isAnimOver && isOrderReady && orderToCollectItems.Count != 0)
        {
            if (orderToCollectItems[0] == go)
            {
                StartCoroutine(IlluminateObject(true, go));
            }
            else
            {
                StartCoroutine(IlluminateObject(false, go));
            }
            isAnimOver = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInside = true;
            if (!isRealyResolved)
            {
                StartCoroutine(ShowObjectToCollect());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isRealyResolved)
        {
            for (int i = 0; i < objectToIllumine.Length; i++)
            {
                objectToIllumine[i].GetComponent<Renderer>().material = lightOff;
            }
            orderToCollectItems.Clear();
            StopAllCoroutines();
            isPlayerInside = false;
        }
    }
}
