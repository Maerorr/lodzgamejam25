using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpikyOverlay : MonoBehaviour
{
    public Image overlay;

    void Start()
    {
        overlay.color = Color.black;
        FindFirstObjectByType<Player>().onDamage.AddListener(OnTakeDamage);
    }

    public void OnTakeDamage()
    {
        overlay.color = new Color(0.7f, 0, 0, 1.0f);
        overlay.DOColor(new Color(0, 0, 0, 1), 0.5f);
    }
}
