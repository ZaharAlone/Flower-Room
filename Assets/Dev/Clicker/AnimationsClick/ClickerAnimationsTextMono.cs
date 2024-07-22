using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace FlowerRoom.Core.Clicker.AnimationsClick
{
    public class ClickerAnimationsTextMono : MonoBehaviour
    {
        [Required]
        public RectTransform RectTransform;
        
        [SerializeField]
        [Required]
        private TextMeshProUGUI _textValue;

        private Sequence _sequence;
        
        public void StartAnimations(int value)
        {
            _textValue.text = "+ " + value;
            _textValue.color = Color.white;

            _sequence = DOTween.Sequence();
            _sequence.Append(_textValue.DOFade(0.2f, 2.5f))
                .Join(RectTransform.DOLocalMoveY(220, 2.5f))
                .OnComplete(() => RemoveObject());
        }

        private void RemoveObject()
        {
            _sequence.Kill();
            Object.Destroy(gameObject);
        }
    }
}