using System;
using System.Collections.Generic;
using FlowerRoom.Core.Clicker.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlowerRoom.Core.Clicker
{
    [CreateAssetMenu(fileName = "ClickerItemsView", menuName = "ScriptableObject/Clicker Items View", order = 0)]
    public class ClickerItemsViewSO : SerializedScriptableObject
    {
        public Dictionary<string, ItemViewTraderow> ItemsView;
        public List<int> PowerSwitchView;
    }
    
    [Serializable]
    public struct ItemViewTraderow
    {
        public Sprite IconsItem;
        public ClickerItemsType ClickerItemsType;
        public ClickerItemMono ItemPrefab;
        
        public List<Sprite> StatePlantGrowth;
    }
}