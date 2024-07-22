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
        private Color32 _colorAvailablePurchase;
        [SerializeField]
        private Color32 _colorNotAvailablePurchase;
        
        [SerializeField]
        private Color32 _colorPriceTextNotAvailablePurchase;

        private int _currentPrice;
        private bool _previousStatusAvailablePurchase;
        
        public void UpdateCurrentCurrencyPlayer(int currentCurrencyPlayer)
        {
            var newStatusAvailablePurchase = currentCurrencyPlayer >= _currentPrice;
            
            if (_previousStatusAvailablePurchase == newStatusAvailablePurchase)
                return;
            
            _previousStatusAvailablePurchase = newStatusAvailablePurchase;
            
            if (currentCurrencyPlayer >= _currentPrice)
            {
                _iconsItem.color = _colorAvailablePurchase;
                _priceText.color = _colorAvailablePurchase;
            }
            else
            {
                _iconsItem.color = _colorNotAvailablePurchase;
                _priceText.color = _colorPriceTextNotAvailablePurchase;
            }
        }

        public void BuyUpgrade()
        {
            UpgradeItemAction.UpgradeItem?.Invoke(_upgradeClickerItemType);
        }
    }
}