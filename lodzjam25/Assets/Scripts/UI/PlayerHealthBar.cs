using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public RectTransform healthBar;
    public float minX;
    public float maxX;

    void Start()
    {
        SetCurrentHealth(1.0f);
    }

    // expected value is between 0 and 1
    public void SetCurrentHealth(float value)
    {
        healthBar.anchoredPosition = new Vector2(Mathf.Lerp(minX, maxX, value), healthBar.anchoredPosition.y);
    }
}
