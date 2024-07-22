using System;
using System.Collections.Generic;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.Clicker.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace FlowerRoom.Core.Traderow
{
    [CreateAssetMenu(fileName = "TraderowConfig", menuName = "ScriptableObject/Traderow config", order = 0)]
    public class TraderowConfigSO : SerializedScriptableObject
    {
        public TraderowConfig TraderowConfig;
        public Dictionary<string, ItemViewTraderow> ItemViewTraderows;
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

    [Serializable]
    public struct ItemViewTraderow
    {
        public Sprite IconsItem;
        [FormerlySerializedAs("ItemType")]
        public ClickerItemsType ClickerItemsType;
        public ClickerItemMono ItemPrefab;
    }
}