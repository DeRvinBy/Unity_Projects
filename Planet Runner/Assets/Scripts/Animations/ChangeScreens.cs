using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Animations
{
    public class ChangeScreens : MonoBehaviour
    {
        [SerializeField]
        private Vector2 planetScreen = Vector2.zero;

        [SerializeField]
        private float Duration = 1f;

        private Vector2 _startScreen;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = transform as RectTransform;
            _startScreen = _rectTransform.position;
        }

        public void SwipeToPlanets()
        {
            _rectTransform.DOAnchorPos(planetScreen, Duration);
        }

        public void SwipeToStartScreen()
        {
            _rectTransform.DOAnchorPos(_startScreen, Duration);
        }
    }
}