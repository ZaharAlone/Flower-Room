using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

        private string _guid;
        
        public void OnEnable()
        {
            _panelUpgradeItem.SetActive(false);
            _perValueSecondsFlowerText.gameObject.SetActive(false);
        }

        public void SetGUID(string guid)
        {
            _guid = guid;
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