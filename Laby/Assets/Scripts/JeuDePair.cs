using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class JeuDePair : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Button buttons;
    [SerializeField] Canvas canvas;
    [SerializeField] Sprite[] sprite;
    UnityEngine.UI.Button[] newButton;
    bool isReturn = false;
    bool isVerif = false;
    bool isCorroutine = false;
    int tempId;
    // Use this for initialization


    void Start()
    {
        newButton = new UnityEngine.UI.Button[16];
        Init();
    }
    IEnumerator Lauch(int id)
    {
        isCorroutine = true;
        yield return new WaitForSeconds(2);
        if (newButton[tempId].image.sprite.name == newButton[id].image.sprite.name)
        {

            newButton[id].image.enabled = false;
            newButton[tempId].image.enabled = false;
            isReturn = false;
            isVerif = false;
            isCorroutine = false;
        }
        else
        {

            newButton[id].image.sprite = sprite[sprite.Length - 1];
            newButton[tempId].image.sprite = sprite[sprite.Length - 1];
            newButton[tempId].enabled = true;
            newButton[id].enabled = true;
            isReturn = false;
            isVerif = false;
            isCorroutine = false;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        Canvas newCanvas = Instantiate(canvas) as Canvas;
        List<int> possibleId = new List<int>();
        for (int i = 0; i < sprite.Length-1; i++)
        {
            possibleId.Add(i);
            possibleId.Add(i);
        }
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                int random = UnityEngine.Random.Range(0, possibleId.Count);
                int idSprite = possibleId[random];
                int id = i + j * 4;
                possibleId.RemoveAt(random);


                newButton[id] = Instantiate(buttons, new Vector2(-300 + (i * 82 + i * 100), +200 - (j * 118 + j * 50)), Quaternion.identity);
                newButton[id].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { ReturnCard(id, idSprite); });
                newButton[id].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { VerifCard(id, idSprite); });



                newButton[id].image.sprite = sprite[sprite.Length - 1];
                newButton[id].transform.SetParent(newCanvas.transform, false);

            }
        }
    }

    public void ReturnCard(int id, int idSprite)
    {
        if (!isCorroutine)
        {
            newButton[id].image.sprite = sprite[idSprite];
            newButton[id].enabled = false;

            if (!isReturn)
            {
                tempId = id;
                isReturn = true;
            }
        }



    }
    public void VerifCard(int id, int idSprite)
    {
        if (!isCorroutine)
        {
            if (isVerif)
            {
                StartCoroutine(Lauch(id));

            }
            if (isReturn)
            {
                isVerif = true;

            }
        }
    }
}
