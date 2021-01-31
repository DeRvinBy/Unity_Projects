using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Animations
{
    public class PlanetAnim : MonoBehaviour
    {
        [SerializeField]
        private float Offset = 10f;

        [SerializeField]
        private float Duration = 1.0f;

        [SerializeField]
        private Ease EaseType = Ease.Linear;

        private RectTransform _rectTransform;
        

        private void Start()
        {
            _rectTransform = transform as RectTransform;

            float startPositionY = _rectTransform.anchoredPosition.y;
            float endPositionY = _rectTransform.anchoredPosition.y + Offset;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectTransform.DOAnchorPosY(endPositionY, Duration).SetEase(EaseType));
            sequence.Append(_rectTransform.DOAnchorPosY(startPositionY, Duration).SetEase(EaseType));
            sequence.SetLoops(-1);
        }
    }
}
