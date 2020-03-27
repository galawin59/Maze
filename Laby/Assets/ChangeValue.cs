using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeValue : MonoBehaviour
{
    public enum TypeSlider
    {
        Grey,
        Contrast,
        Negative,
        Red,
        Green,
        Blue
    }
    [SerializeField] Slider[] sliders;
    [SerializeField] InputField[] inputFields;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            TypeSlider type = (TypeSlider)i;
            sliders[i].onValueChanged.AddListener(delegate { OnChange(type); });
            GetCorrectValue(type);
            /* sliders[(int)TypeSlider.Contrast].onValueChanged.AddListener(delegate { OnChange(TypeSlider.Contrast); });
             sliders[(int)TypeSlider.Negative].onValueChanged.AddListener(delegate { OnChange(TypeSlider.Negative); });
             sliders[(int)TypeSlider.Red].onValueChanged.AddListener(delegate { OnChange(TypeSlider.Red); });
             sliders[(int)TypeSlider.Green].onValueChanged.AddListener(delegate { OnChange(TypeSlider.Green); });
             sliders[(int)TypeSlider.Blue].onValueChanged.AddListener(delegate { OnChange(TypeSlider.Blue); });*/
        }
        inputFields[0].characterValidation = InputField.CharacterValidation.Decimal;
        inputFields[0].text = ParametrableManager.Instance.numberOfObjectToIlluminate.ToString();
        inputFields[0].onValueChanged.AddListener(delegate { OnChangeInteger(); });
        inputFields[1].characterValidation = InputField.CharacterValidation.Decimal;
        inputFields[1].onValueChanged.AddListener(delegate { OnChangeDecimal(); });
        inputFields[1].text = ParametrableManager.Instance.timerObjectStayIlluminate.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCorrectValue(TypeSlider type)
    {
        switch (type)
        {
            case TypeSlider.Grey:
                sliders[(int)type].value = ParametrableManager.Instance.grey;
                break;
            case TypeSlider.Contrast:
                sliders[(int)type].value = ParametrableManager.Instance.contrast;
                break;
            case TypeSlider.Negative:
                sliders[(int)type].value = ParametrableManager.Instance.negative;
                break;
            case TypeSlider.Red:
                sliders[(int)type].value = ParametrableManager.Instance.color.r;
                break;
            case TypeSlider.Green:
                sliders[(int)type].value = ParametrableManager.Instance.color.g;
                break;
            case TypeSlider.Blue:
                sliders[(int)type].value = ParametrableManager.Instance.color.b;
                break;
            default:
                break;
        }
    }

    public void OnChange(TypeSlider type)
    {
        switch (type)
        {
            case TypeSlider.Grey:
                ParametrableManager.Instance.grey = sliders[(int)type].value;
                break;
            case TypeSlider.Contrast:
                ParametrableManager.Instance.contrast = sliders[(int)type].value;
                break;
            case TypeSlider.Negative:
                ParametrableManager.Instance.negative = sliders[(int)type].value;
                break;
            case TypeSlider.Red:
                ParametrableManager.Instance.color.r = sliders[(int)type].value;
                break;
            case TypeSlider.Green:
                ParametrableManager.Instance.color.g = sliders[(int)type].value;
                break;
            case TypeSlider.Blue:
                ParametrableManager.Instance.color.b = sliders[(int)type].value;
                break;
            default:
                break;
        }

    }

    public void OnChangeInteger()
    {
        ParametrableManager.Instance.numberOfObjectToIlluminate = int.Parse(inputFields[0].text) == 0 ? 1 : int.Parse(inputFields[0].text);
    }

    public void OnChangeDecimal()
    {
        ParametrableManager.Instance.timerObjectStayIlluminate = float.Parse(inputFields[1].text) == 0 ? 2.0f : float.Parse(inputFields[1].text);
    }
}
