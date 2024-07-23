using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker.Items;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.Traderow;

namespace FlowerRoom.Core.CurrencyFlower
{
    [EcsSystem(typeof(CoreModule))]
    public class CurrencyFlowerSystem : IPreInitSystem, IInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void PreInit()
        {
            CurrencyFlowerAction.AddCurrencyFlower += AddCurrencyFlower;
            CurrencyFlowerAction.SubCurrencyFlower += SubCurrencyFlower;
        }
        
        public void Init()
        {
            _dataWorld.CreateOneData(new CurrencyFlowerData
            {
                CountCurrencyFlower = 0
            });

            UpdateUICurrencyFlower();
        }

        private void AddCurrencyFlower(int addValueCurrencyFlower)
        {
            ref var currencyFlowerData = ref _dataWorld.OneData<CurrencyFlowerData>();
            currencyFlowerData.CountCurrencyFlower += addValueCurrencyFlower;

            UpdateUICurrencyFlower();
        }
        
        private void SubCurrencyFlower(int subValueCurrencyFlower)
        {
            ref var currencyFlowerData = ref _dataWorld.OneData<CurrencyFlowerData>();
            currencyFlowerData.CountCurrencyFlower -= subValueCurrencyFlower;

            UpdateUICurrencyFlower();
        }
        
        private void UpdateUICurrencyFlower()
        {
            var currencyFlowerData = _dataWorld.OneData<CurrencyFlowerData>();
            var currencyFlowerUI = _dataWorld.OneData<GameUIData>().GameUI.CurrencyFlowerUIMono;
            
            //TODO Округлять значение до тысяч, десятков тысяч, и добавлять букву, 1к, 10к, 1кк, и так далее
            
            currencyFlowerUI.SetViewCountCurrencyFlower(currencyFlowerData.CountCurrencyFlower.ToString());

            UpdateUIAllElementGameplay();
        }

        private void UpdateUIAllElementGameplay()
        {
            var countCurrencyFlower = _dataWorld.OneData<CurrencyFlowerData>().CountCurrencyFlower;

            var clickerItemsEntities = _dataWorld.Select<ClickerItemComponent>().GetEntities();

            foreach (var clickerItemsEntity in clickerItemsEntities)
            {
                var clickerItemComponent = clickerItemsEntity.GetComponent<ClickerItemComponent>();
                clickerItemComponent.ClickerItemMono.UpdateCurrencyButtonsUpgrade(countCurrencyFlower);
            }

            var traderowItemsEntities = _dataWorld.Select<TraderowItemComponent>().GetEntities();

            foreach (var traderowItemEntity in traderowItemsEntities)
            {
                var traderowItemComponent = traderowItemEntity.GetComponent<TraderowItemComponent>();
                traderowItemComponent.ItemInTraderowMono.UpdateCurrentCurrencyPlayer(countCurrencyFlower);
            }
        }

        public void Destroy()
        {
            CurrencyFlowerAction.AddCurrencyFlower -= AddCurrencyFlower;
            CurrencyFlowerAction.SubCurrencyFlower -= SubCurrencyFlower;
        }
    }
}