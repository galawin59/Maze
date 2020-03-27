using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametrableManager : MonoBehaviour
{
    public static ParametrableManager Instance { get; private set; }

    public int numberOfObjectToIlluminate = 3;
    public float timerObjectStayIlluminate = 2.0f;
    public float contrast = 1.0f;
    public float negative = 0.0f;
    public float grey = 0.0f;
    public Color color = Color.white;
    SimpleFX postFxCtrl;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        postFxCtrl = GameObject.FindObjectOfType<SimpleFX>();
    }

    // Update is called once per frame
    void Update()
    {
        postFxCtrl.SetMaterials();
    }
}
