using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlowerRoom.Core.Clicker.UpgradeItems
{
    public class UpgradeItemButtonUIMono : MonoBehaviour
    {
        [SerializeField]
        private UpgradeClickerItemType _upgradeClickerItemType;
        
        [SerializeField]
        [Required]
        private Image _iconsItem;

        [SerializeField]
        [Required]
        private TextMeshProUGUI _priceText;

        [SerializeField]
        [Required]
        private Button _button;
        
        [SerializeField]
        private Color32 _colorAvailablePurchase;
        
        [SerializeField]
        private Color32 _colorPriceTextNotAvailablePurchase;

        private int _currentPrice;
        private int _currentCurrency;
        private bool _previousStatusAvailablePurchase = true;
        private string _guid;

        public void SetGuid(string guid)
        {
            _guid = guid;
        }
        
        public void SetCurrentPriceItem(UpgradeClickerItemType targetTypeItem, int currentPrice)
        {
            if (targetTypeItem != _upgradeClickerItemType)
                return;

            _currentPrice = currentPrice;
            _priceText.text = currentPrice.ToString();
            
            UpdateCurrentCurrencyPlayer(_currentCurrency);
        }
        
        public void UpdateCurrentCurrencyPlayer(int currentCurrencyPlayer)
        {
            var newStatusAvailablePurchase = currentCurrencyPlayer >= _currentPrice;
            _currentCurrency = currentCurrencyPlayer;
            
            if (_previousStatusAvailablePurchase == newStatusAvailablePurchase)
                return;
            
            _previousStatusAvailablePurchase = newStatusAvailablePurchase;
            
            if (currentCurrencyPlayer >= _currentPrice)
            {
                _priceText.color = _colorAvailablePurchase;
                _button.interactable = true;
            }
            else
            {
                _priceText.color = _colorPriceTextNotAvailablePurchase;
                _button.interactable = false;
            }
        }

        public void BuyUpgrade()
        {
            UpgradeItemAction.UpgradeItem?.Invoke(_guid, _upgradeClickerItemType);
        }
    }
}