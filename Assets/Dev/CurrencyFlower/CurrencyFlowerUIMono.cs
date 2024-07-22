using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace FlowerRoom.Core.CurrencyFlower
{
    public class CurrencyFlowerUIMono : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private TextMeshProUGUI _countCurrencyFlowerText;

        public void SetViewCountCurrencyFlower(string count)
        {
            _countCurrencyFlowerText.text = count;
        }
    }
}