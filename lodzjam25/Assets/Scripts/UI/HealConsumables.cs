using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealConsumables : MonoBehaviour
{
    public List<Image> images = new List<Image>();
    int availableHeals = 3;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            availableHeals--;
            SetAvailableHeals(availableHeals);
        }
    }

    public void SetAvailableHeals(int count)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = i < count ? Color.white : new Color(1, 1, 1, 0.0f);
        }
    }
}
