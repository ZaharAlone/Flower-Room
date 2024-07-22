using FlowerRoom.Core.Clicker.AnimationsClick;
using UnityEngine;

namespace FlowerRoom.Core.Clicker
{
    [CreateAssetMenu(fileName = "ClickerConfig", menuName = "ScriptableObject/Clicker config", order = 0)]
    public class ClickerConfigSO : ScriptableObject
    {
        public ClickerAnimationsTextMono ClickerAnimationsTextMono;
        public TextAsset ProgreessionPriceUpgradeConfig;
    }
}