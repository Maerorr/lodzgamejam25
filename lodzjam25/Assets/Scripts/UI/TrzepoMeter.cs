using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrzepoMeter : MonoBehaviour
{
    public Image image;
    Material mat;
    public float minY;
    public float maxY;

    void Start()
    {
        mat = image.material;
        SetPressure(0);

    }

    // value should be normalized 0-1
    public void SetPressure(float value)
    {
        mat.SetFloat("_value", value);
    }
}
