using System;
using System.Collections.Generic;
using FlowerRoom.Core.Clicker.UpgradeItems;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlowerRoom.Core.Clicker.Items
{
    public class ClickerItemMono : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField]
        private string _keyItem;

        [SerializeField]
        [Required]
        private GameObject _panelUpgradeItem;
        
        [SerializeField]
        [Required]
        private TextMeshProUGUI _perValueSecondsFlowerText;

        [SerializeField]
        private List<UpgradeItemButtonUIMono> _upgradeItemsButtonList = new List<UpgradeItemButtonUIMono>();

        [SerializeField]
        [Required]
        private Image _imagePlantGrowth;
        
        private string _guid;
        
        public void OnEnable()
        {
            _panelUpgradeItem.SetActive(false);
            _perValueSecondsFlowerText.gameObject.SetActive(false);
        }

        public void SetGUID(string guid)
        {
            _guid = guid;
            
            foreach (var updateButton in _upgradeItemsButtonList)
                updateButton.SetGuid(guid);
        }

        public void UpdateCurrencyButtonsUpgrade(int currentCurrency)
        {
            foreach (var updateButton in _upgradeItemsButtonList)
            {
                updateButton.UpdateCurrentCurrencyPlayer(currentCurrency);
            }
        }

        public void UpdatePriceButtonsUpgrade(UpgradeClickerItemType targetTypeItem, int currentPrice)
        {
            foreach (var updateButton in _upgradeItemsButtonList)
            {
                updateButton.SetCurrentPriceItem(targetTypeItem, currentPrice);
            }
        }
        
        public void UpdateValuePerSecondFlower(float value)
        {
            _perValueSecondsFlowerText.text = "x" + value.ToString("F1");
        }
        
        public void SetStatePlantGrowth(Sprite imageState)
        {
            _imagePlantGrowth.gameObject.SetActive(imageState!= null);
            _imagePlantGrowth.sprite = imageState;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            
            ClickerAction.ClickItem?.Invoke(_keyItem);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _panelUpgradeItem.SetActive(true);
            _perValueSecondsFlowerText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _panelUpgradeItem.SetActive(false);
            _perValueSecondsFlowerText.gameObject.SetActive(false);
        }
    }
}