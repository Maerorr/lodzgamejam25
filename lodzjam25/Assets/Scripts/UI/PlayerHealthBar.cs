using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Slider slider;

    void Start()
    {
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponentInChildren<Slider>();
        SetMaxHealth(100.0f);
    }

    void Update()
    {
    }

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
        slider.value = value;
        SetText(value);
    }

    // this does not check whether the value is between 0 and max, player should handle that
    public void SetCurrentHealth(float value)
    {
        slider.value = value;
        SetText(value);
    }

    void SetText(float value)
    {
        healthText.text = string.Format("HP: {0}", (int)Mathf.Round(value));
    }
}
