using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MainMenu
{
    public class BuyManager : MonoBehaviour
    {
        [Header("Buy Panels")]
        [SerializeField]
        private PopUpPanel PanelConfirmBuy;

        [SerializeField]
        private PopUpPanel PanelErrorBuy;

        private int _currentCostToBuy;
        private GameObject _currentPlanetHidePanel;
        private MenuManager _menuManager;

        private void Awake()
        {
            _menuManager = GetComponent<MenuManager>();
        }

        public void BuyPlanet(int value)
        {
            if (_menuManager.GetSpaceStoneValue() >= value)
            {
                _currentCostToBuy = value;
                PanelConfirmBuy.ShowPanel();
            }
            else
            {
                PanelErrorBuy.ShowPanel();
            }
        }

        public void SetPlanetToBuy(GameObject target)
        {
            _currentPlanetHidePanel = target;
        }

        public void ApplyBuyPlanet()
        {
            _menuManager.ChangeSpaceStoneValue(-_currentCostToBuy);
            _currentPlanetHidePanel.SetActive(false);
        }
    }
}