using System;
using System.Collections.Generic;
using FlowerRoom.Core.Clicker.UpgradeItems;
using FlowerRoom.Core.MoveItem;
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
        private GameObject _panelText;
        
        [SerializeField]
        [Required]
        private TextMeshProUGUI _perValueSecondsFlowerText;

        [SerializeField]
        private List<UpgradeItemButtonUIMono> _upgradeItemsButtonList = new List<UpgradeItemButtonUIMono>();

        [SerializeField]
        [Required]
        private Image _imagePlantGrowth;

        public RectTransform RectTransform;
        
        private string _guid;
        private bool _notShowPanelUpgrade;
        
        public void OnEnable()
        {
            _panelUpgradeItem.SetActive(false);
            _panelText.SetActive(false);
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
        
        public void UpdateValuePerSecondFlower(float value, float bonusValue)
        {
            var textValue = value.ToString("F1");

            if (bonusValue > 0)
                textValue += $"+<color=green> {bonusValue.ToString("F1")} </color>";
            _perValueSecondsFlowerText.text = textValue;
        }
        
        public void SetStatePlantGrowth(Sprite imageState)
        {
            _imagePlantGrowth.gameObject.SetActive(imageState!= null);
            _imagePlantGrowth.sprite = imageState;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                ClickerAction.ClickItem?.Invoke(_keyItem);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                MoveItemAction.StartMoveItem?.Invoke(_guid);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_notShowPanelUpgrade)
                return;
            
            ClickerAction.SelectItem?.Invoke(_guid);
            ShowHidePanelUpgrade(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ClickerAction.DeselectItem?.Invoke();
            ShowHidePanelUpgrade(false);
        }

        private void ShowHidePanelUpgrade(bool isShow)
        {
            _panelUpgradeItem.SetActive(isShow);
            _panelText.SetActive(isShow);
        }

        public void ForceHidePanelUpgrade()
        {
            _notShowPanelUpgrade = true;
            ShowHidePanelUpgrade(false);
        }
        
        public void ForceShowPanelUpgrade()
        {
            _notShowPanelUpgrade = false;
            ShowHidePanelUpgrade(true);
        }

        public void EnablePerSecondValueCurrency(bool isShow)
        {
            _panelText.SetActive(isShow);
        }
    }
}