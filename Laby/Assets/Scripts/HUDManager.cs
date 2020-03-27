using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] Text timerText;
    [SerializeField] Text objectCollectedText;
    [SerializeField] Text victoryText;
    [SerializeField] Text obstaclesHitText;
    [SerializeField] Text wallHitText;
    [SerializeField] Text simonTimerText;
    [SerializeField] Text finalTimerText;

    [SerializeField] Text obstaclesText;
    [SerializeField] Text wallText;

    [SerializeField] Image background;
    ControllerFPS player;
    bool stopTimer = false;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<ControllerFPS>();
        player.onControllerFps += UpdateCollectedText;
        player.onControllerFpsWin += Victory;
        player.onControllerFpsNbCollision += UpdateObstaclesText;
        player.onControllerFpsNbCollisionWall += UpdateWallText;
        objectCollectedText.text = "Objets collectés : 0/" + player.NbItemToCollect.ToString();
    }

    void Victory()
    {
        timerText.enabled = false;
        objectCollectedText.enabled = false;
        obstaclesText.enabled = false;
        wallText.enabled = false;

        background.enabled = true;
        stopTimer = true;
        victoryText.enabled = true;
        obstaclesHitText.enabled = true;
        wallHitText.enabled = true;
        simonTimerText.enabled = true;
        finalTimerText.enabled = true;
        finalTimerText.text = "Temps total : " + timer + " secondes";
        simonTimerText.text = "Temps pour le simon : " + ((int)FindObjectOfType<Simon>().timer).ToString() + " secondes";
    }

    void UpdateObstaclesText(int nbItem)
    {
        obstaclesHitText.text = "Obstacles touchés : " + nbItem;
        obstaclesText.text = "Obstacles touchés : " + nbItem;
    }

    void UpdateWallText(int nbItem)
    {
        wallHitText.text = "Murs touchés : " + nbItem;
        wallText.text = "Murs touchés : " + nbItem;
    }

    void UpdateCollectedText(int nbItem)
    {
        objectCollectedText.text = "Objets collectés : " + nbItem.ToString() + "/" + player.NbItemToCollect.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
        {
            timer += Time.deltaTime;
            timerText.text = "Temps : " + ((int)timer).ToString();
        }

    }
}
