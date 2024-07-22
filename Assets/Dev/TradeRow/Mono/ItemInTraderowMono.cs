using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace FlowerRoom.Core.Traderow
{
    public class ItemInTraderowMono : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private Image _iconsItem;
        [SerializeField]
        [Required]
        private TextMeshProUGUI _priceItemText;
        [SerializeField]
        private TextMeshProUGUI _countItemText;

        [SerializeField]
        private Color32 _colorAvailablePurchase;
        [SerializeField]
        private Color32 _colorNotAvailablePurchase;
        
        [SerializeField]
        private Color32 _colorPriceTextNotAvailablePurchase;
        
        private string _keyItem;
        private int _currentPriceItem;
        private bool _previousStatusAvailablePurchase;
        
        public void SetViewItem(string keyItem, Sprite iconsItem, int priceItem, int countItem)
        {
            _keyItem = keyItem;
            _currentPriceItem = priceItem;

            _iconsItem.sprite = iconsItem;
            
            UpdateCountItemAndPrice(countItem, priceItem);
        }

        public void UpdateCurrentCurrencyPlayer(int currentCurrencyPlayer)
        {
            var newStatusAvailablePurchase = currentCurrencyPlayer >= _currentPriceItem;
            
            if (_previousStatusAvailablePurchase == newStatusAvailablePurchase)
                return;
            
            _previousStatusAvailablePurchase = newStatusAvailablePurchase;
            
            if (currentCurrencyPlayer >= _currentPriceItem)
            {
                _iconsItem.color = _colorAvailablePurchase;
                _priceItemText.color = _colorAvailablePurchase;
            }
            else
            {
                _iconsItem.color = _colorNotAvailablePurchase;
                _priceItemText.color = _colorPriceTextNotAvailablePurchase;
            }
        }

        public void UpdateCountItemAndPrice(int countItem, int priceItem = 0)
        {
            _currentPriceItem = priceItem;
            
            _countItemText.text = countItem.ToString();
            _priceItemText.text = priceItem.ToString();

            var isActivePriceText = countItem > 0;
            _priceItemText.gameObject.SetActive(isActivePriceText);
        }

        public void BuyItem()
        {
            TraderowAction.BuyItem?.Invoke(_keyItem);
        }
    }
}