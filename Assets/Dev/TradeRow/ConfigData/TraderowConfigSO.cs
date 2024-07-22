using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlowerRoom.Core.Traderow
{
    [CreateAssetMenu(fileName = "TraderowConfig", menuName = "ScriptableObject/Traderow config", order = 0)]
    public class TraderowConfigSO : SerializedScriptableObject
    {
        public TraderowConfig TraderowConfig;
        public ItemInTraderowMono ItemInTraderowMonoPrefab;
    }

    [Serializable]
    public struct TraderowConfig
    {
        public List<LevelTraderowConfig> LevelsTraderowConfig;
    }

    [Serializable]
    public struct LevelTraderowConfig
    {
        public List<ItemTraderowConfig> ItemsTraderow;
    }

    [Serializable]
    public struct ItemTraderowConfig
    {
        public string KeyItem;
        public List<int> Price;
    }
}