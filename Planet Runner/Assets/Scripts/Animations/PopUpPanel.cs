using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField]
    private Vector2 ShowPanelPosition = Vector3.zero;

    [SerializeField]
    private Vector2 HidePanelPosition = Vector3.zero;

    [Range(0f, 255f), SerializeField]
    private float ShowOpacity = 100f;

    [Range(0f, 255f), SerializeField]
    private float HideOpacity = 0f;

    [SerializeField]
    private float MoveDuration = 1f;

    [SerializeField]
    private float OpacityDuration = 1f;

    [SerializeField]
    private Ease MoveEase = Ease.Linear;

    private RectTransform _rectTransform;
    private Image _panel;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        _rectTransform.TryGetComponent(out _panel);
    }

    public void ShowPanel()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(_rectTransform.DOAnchorPos(ShowPanelPosition, MoveDuration).SetEase(MoveEase));
        if (_panel != null)
        {
            Color newColor = _panel.color; newColor.a = ShowOpacity / 255f;
            sequence.Append(_panel.DOColor(newColor, OpacityDuration));
        }
    }

    public void HidePanel()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        if (_panel != null)
        {
            Color newColor = _panel.color; newColor.a = HideOpacity / 255f;
            sequence.Append(_panel.DOColor(newColor, OpacityDuration));
        }
        sequence.Append(_rectTransform.DOAnchorPos(HidePanelPosition, MoveDuration).SetEase(MoveEase));
    }
}
