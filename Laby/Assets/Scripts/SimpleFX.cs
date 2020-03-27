using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleFX : MonoBehaviour
{
    public Material[] FXMaterial;

    private void Start()
    {
        
    }

    public void SetMaterials()
    {
        FXMaterial[0].SetFloat("_Contrast",ParametrableManager.Instance.contrast);
        FXMaterial[1].SetColor("_Color", ParametrableManager.Instance.color);
        FXMaterial[2].SetFloat("_Negative", ParametrableManager.Instance.negative);
        FXMaterial[3].SetFloat("_Grayscale", ParametrableManager.Instance.grey);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) //Fonction appelée par unity à chaque fin de rendu. C'est maintenant qu'on fait le post-effet
    {
        int w = Screen.width;
        int h = Screen.height;
        RenderTexture rt = RenderTexture.GetTemporary(w, h);
        RenderTexture rt2 = RenderTexture.GetTemporary(w, h);

        for (int i = 0; i < FXMaterial.Length; i++)
        {
            if (i == 0)
                Graphics.Blit(source, rt, FXMaterial[i]);
            else if (i == FXMaterial.Length - 1)
            {
                if (i % 2 == 0)
                {
                    Graphics.Blit(rt2, destination, FXMaterial[i]);
                }
                else
                {
                    Graphics.Blit(rt, destination, FXMaterial[i]);
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    Graphics.Blit(rt2, rt, FXMaterial[i]);
                }
                else Graphics.Blit(rt, rt2, FXMaterial[i]);
            }
        }

        //COUCOU c'est moi
        //mais en faite nn c'etait pas moi c'etait plus pire

        RenderTexture.ReleaseTemporary(rt);
        RenderTexture.ReleaseTemporary(rt2);
    }
}

