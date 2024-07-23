using System;
using EcsCore;
using FlowerRoom.Core.Clicker.BonusPlant;
using FlowerRoom.Core.Clicker.Items;
using FlowerRoom.Core.CurrencyFlower;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;

namespace FlowerRoom.Core.Clicker.UpgradeItems
{
    [EcsSystem(typeof(CoreModule))]
    public class UpgradeClickerItemSystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;
        
        public void PreInit()
        {
            UpgradeItemAction.UpgradeItem += UpgradeItem;
        }
        
        private void UpgradeItem(string guid, UpgradeClickerItemType typeUpgradeItem)
        {
            var selectClickerItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.GUID == guid)
                .SelectFirstEntity();
            ref var selectClickerItemComponent = ref selectClickerItemEntity.GetComponent<ClickerItemComponent>();
            var configPriceUpgrade = _dataWorld.OneData<ClickerConfigData>().ProgressionPriceUpgradeClickerItemConfig;

            var priceValueUpgrade = new ProgressionPriceValue();
            
            switch (typeUpgradeItem)
            {
                case UpgradeClickerItemType.Watering:
                    priceValueUpgrade = configPriceUpgrade.WateringPrices[selectClickerItemComponent.WateringCountBuy];
                    selectClickerItemComponent.WateringCountBuy++;
                    break;
                case UpgradeClickerItemType.Weeding:
                    priceValueUpgrade = configPriceUpgrade.WeedingPrices[selectClickerItemComponent.WeedingCountBuy];
                    selectClickerItemComponent.WeedingCountBuy++;
                    break;
                case UpgradeClickerItemType.Fertilizing:
                    priceValueUpgrade = configPriceUpgrade.FertilizingPrices[selectClickerItemComponent.FertilizingCountBuy];
                    selectClickerItemComponent.FertilizingCountBuy++;
                    break;
            }
            
            CurrencyFlowerAction.SubCurrencyFlower.Invoke(priceValueUpgrade.Price);
            selectClickerItemComponent.AddCurrencyPerSecond += priceValueUpgrade.Value;
            
            BonusPlantAction.UpdateViewBonus?.Invoke();
            UpgradeItemAction.UpdateUIUpgradeItem?.Invoke(guid);

            SwitchVisualGradePlant(guid);
        }
        
        private void SwitchVisualGradePlant(string guid)
        {
            var selectClickerItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.GUID == guid)
                .SelectFirstEntity();
            ref var selectClickerItemComponent = ref selectClickerItemEntity.GetComponent<ClickerItemComponent>();
            var clickerItemViewConfig = _dataWorld.OneData<ClickerConfigData>();

            var targetGradeIndex = selectClickerItemComponent.CurrentGradePlant + 1;

            if (selectClickerItemComponent.AddCurrencyPerSecond >= clickerItemViewConfig.PowerSwitchView[targetGradeIndex])
            {
                var newImageGrade = clickerItemViewConfig.ItemView[selectClickerItemComponent.KeyItem].StatePlantGrowth[targetGradeIndex];
                selectClickerItemComponent.ClickerItemMono.SetStatePlantGrowth(newImageGrade);
                selectClickerItemComponent.CurrentGradePlant++;
            }
        }

        public void Destroy()
        {
            UpgradeItemAction.UpgradeItem -= UpgradeItem;
        }
    }
}