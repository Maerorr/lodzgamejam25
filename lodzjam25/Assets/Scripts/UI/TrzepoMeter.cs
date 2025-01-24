using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrzepoMeter : MonoBehaviour
{
    public RectTransform foam;
    public Image foamImage;
    public Gradient gradient;
    public TextMeshProUGUI watningText;
    public float minY;
    public float maxY;
    public float warningThreshold;

    Sequence textBlinkSequence;

    void Start()
    {
        

        textBlinkSequence = DOTween.Sequence();
        textBlinkSequence.Append(watningText.DOFade(0.3f, 0.1f));
        textBlinkSequence.Append(watningText.DOFade(1.0f, 0.1f));
        textBlinkSequence.SetLoops(-1, LoopType.Restart);
        SetPressure(0);
    }

    // value should be normalized 0-1
    public void SetPressure(float value)
    {
        foam.anchoredPosition = new Vector2(foam.anchoredPosition.x, Mathf.Lerp(minY, maxY, value));
        foamImage.color = gradient.Evaluate(value);

        if (value > warningThreshold)
        {
            textBlinkSequence.Play();
        }
        else
        {
            textBlinkSequence.Pause();
            watningText.DOFade(0.0f, 0.5f);
        }
    }
}
