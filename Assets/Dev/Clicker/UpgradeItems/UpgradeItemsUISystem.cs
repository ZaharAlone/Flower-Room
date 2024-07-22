using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.Clicker.Items;
using FlowerRoom.Core.Clicker.UpgradeItems;
using FlowerRoom.Core.CurrencyFlower;

namespace CyberNet.Core.Clicker.UpgradeItems
{
    [EcsSystem(typeof(CoreModule))]
    public class UpgradeItemsUISystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void PreInit()
        {
            UpgradeItemAction.UpdateUIUpgradeItem += UpdatePriceButtonUpgradeNewItems;
        }
        
        private void UpdatePriceButtonUpgradeNewItems(string guid)
        {
            var selectClickerItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.GUID == guid)
                .SelectFirstEntity();
            ref var clickerItemComponent = ref selectClickerItemEntity.GetComponent<ClickerItemComponent>();
            
            var countCurrencyFlower = _dataWorld.OneData<CurrencyFlowerData>().CountCurrencyFlower;
            var configPriceUpgrade = _dataWorld.OneData<ClickerConfigData>().ProgressionPriceUpgradeClickerItemConfig;
            var clickerItemMono = clickerItemComponent.ClickerItemMono;
            
            var priceWatering = configPriceUpgrade.WateringPrices[clickerItemComponent.WateringCountBuy].Price;
            clickerItemMono.UpdatePriceButtonsUpgrade(UpgradeClickerItemType.Watering, priceWatering);
            
            var priceWeeding = configPriceUpgrade.WeedingPrices[clickerItemComponent.WeedingCountBuy].Price;
            clickerItemMono.UpdatePriceButtonsUpgrade(UpgradeClickerItemType.Weeding, priceWeeding);
            
            var priceFertilizing = configPriceUpgrade.FertilizingPrices[clickerItemComponent.FertilizingCountBuy].Price;
            clickerItemMono.UpdatePriceButtonsUpgrade(UpgradeClickerItemType.Fertilizing, priceFertilizing);

            clickerItemMono.UpdateCurrencyButtonsUpgrade(countCurrencyFlower);
        }

        public void Destroy()
        {
            UpgradeItemAction.UpdateUIUpgradeItem -= UpdatePriceButtonUpgradeNewItems;
        }
    }
}