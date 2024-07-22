using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.Clicker.Items;
using FlowerRoom.Core.CurrencyFlower;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.Levels;
using FlowerRoom.Core.Traderow;
using Object = UnityEngine.Object;

namespace CyberNet.Core
{
    [EcsSystem(typeof(CoreModule))]
    public class TraderowSystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void PreInit()
        {
            TraderowAction.InitLevel += InitCurrentLevel;
            TraderowAction.BuyItem += CheckBuyNewItem;
        }
        
        private void InitCurrentLevel(int indexLevel)
        {
            var traderowUI = _dataWorld.OneData<GameUIData>().GameUI.TrederowUIMono;
            var configTraderowData = _dataWorld.OneData<TraderowData>();
            var configCurrentLevel = configTraderowData.TraderowConfig.LevelsTraderowConfig[indexLevel];
            var clickerItemView = _dataWorld.OneData<ClickerConfigData>().ItemView;
            
            foreach (var itemTraderow in configCurrentLevel.ItemsTraderow)
            {
                var itemInTraderowMono = Object.Instantiate(configTraderowData.ItemInTraderowMonoPrefab,
                    traderowUI.ContainerItemInTraderow);
                var iconsItem = clickerItemView[itemTraderow.KeyItem].IconsItem;
                
                itemInTraderowMono.SetViewItem(itemTraderow.KeyItem, iconsItem, itemTraderow.Price[0], itemTraderow.Price.Count);
            }
        }

        private void CheckBuyNewItem(string keyItem)
        {
            var countItemInScene = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.KeyItem == keyItem)
                .Count();

            var currentFlowerCurrency = _dataWorld.OneData<CurrencyFlowerData>().CountCurrencyFlower;
            var currentLevelIndex = _dataWorld.OneData<LevelData>().CurrentIndexLevel;
            
            var traderowData = _dataWorld.OneData<TraderowData>();
            var priceListNewItem = traderowData.TraderowConfig.LevelsTraderowConfig[currentLevelIndex]
                .ItemsTraderow.Find(item => item.KeyItem == keyItem).Price;
            var priceNewItem = priceListNewItem[countItemInScene];

            if (currentFlowerCurrency >= priceNewItem)
            {
                BuyNewItem(keyItem, priceNewItem);
            }
            
            //TODO: дописать краевые кейсы, когда покупка не происходит, когда все предметы данного типа уже куплены или нехватает денег
        }

        private void BuyNewItem(string keyNewItem, int PriceNewItem)
        {
            CurrencyFlowerAction.SubCurrencyFlower?.Invoke(PriceNewItem);
            ClickerAction.AddNewItemScene?.Invoke(keyNewItem);

            UpdateTraderowUI();
        }
        
        private void UpdateTraderowUI()
        {
            
        }

        public void Destroy()
        {
            TraderowAction.InitLevel -= InitCurrentLevel;
            TraderowAction.BuyItem -= CheckBuyNewItem;
        }
    }
}