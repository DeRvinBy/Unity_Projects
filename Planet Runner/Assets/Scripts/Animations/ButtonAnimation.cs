using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float ScaleMultiply = 1.5f;

    [SerializeField]
    private float Duration = 1f;

    [SerializeField]
    private Ease EaseScale = Ease.Linear;

    private Vector3 _startScale;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        _startScale = _rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOScale(_startScale * ScaleMultiply, Duration).SetEase(EaseScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(_startScale, Duration).SetEase(EaseScale);
    }
}
