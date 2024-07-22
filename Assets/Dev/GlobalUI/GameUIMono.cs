using FlowerRoom.Core.CurrencyFlower;
using FlowerRoom.Core.Traderow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlowerRoom.Core.GameUI
{
    public class GameUIMono : MonoBehaviour
    {
        [Required]
        public CurrencyFlowerUIMono CurrencyFlowerUIMono;
        [Required]
        public TrederowUIMono TrederowUIMono;
        [Required]
        public Transform ClickerItemsContainer;
        [Required]
        public Transform ClickerAnimationsContainer;
    }
}