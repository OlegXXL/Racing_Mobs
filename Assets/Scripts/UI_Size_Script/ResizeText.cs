using UnityEngine;
using DG.Tweening;

public class ResizeText : MonoBehaviour
{
    public float bigScale = 1.5f;
    public float smallScale = 0.5f;
    public float duration = 1.0f;

    void Start()
    {
        StartLoopingAnimation();
    }

    void StartLoopingAnimation()
    {
        // Scale up
        transform.DOScale(bigScale, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}