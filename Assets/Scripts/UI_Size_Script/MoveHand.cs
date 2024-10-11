using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class MoveHand : MonoBehaviour
{
    public float moveDistance = 5.0f;
    public float moveDuration = 2.0f;

    DG.Tweening.Sequence sequence;

    private void OnEnable()
    {
        StartLoopingAnimation();
    }

    private void OnDisable()
    {
        StopLoopingAnimation();
    }

    void StartLoopingAnimation()
    {
        sequence = DOTween.Sequence();
        Vector3 startPosition = transform.position;

        sequence.Append(transform.DOMoveX(startPosition.x + moveDistance, moveDuration).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOMoveX(startPosition.x, moveDuration).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOMoveX(startPosition.x - moveDistance, moveDuration).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOMoveX(startPosition.x, moveDuration).SetEase(Ease.InOutSine));

        sequence.SetLoops(-1, LoopType.Restart);
    }

    void StopLoopingAnimation()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
    }
}