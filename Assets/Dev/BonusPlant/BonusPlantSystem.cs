using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker.Items;

namespace FlowerRoom.Core.Clicker.BonusPlant
{
    [EcsSystem(typeof(CoreModule))]
    public class BonusPlantSystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        private const float bonus_distance = 200;
        
        public void PreInit()
        {
            BonusPlantAction.EndMoveClickerItem += EndMoveClickerItem;
            BonusPlantAction.UpdateViewBonus += CalculateBonus;
        }

        private void EndMoveClickerItem()
        {
            var clickerItemEntities = _dataWorld.Select<ClickerItemComponent>().GetEntities();

            foreach (var clickerItemEntity in clickerItemEntities)
            {
                ref var clickerItemComponent = ref clickerItemEntity.GetComponent<ClickerItemComponent>();
                clickerItemComponent.BonusPlants.Clear();

                foreach (var neighboringClickerItemEntity in clickerItemEntities)
                {
                    var neighboringClickerItemComponent = neighboringClickerItemEntity.GetComponent<ClickerItemComponent>();

                    if (neighboringClickerItemComponent.GUID == clickerItemComponent.GUID)
                        continue;
                    
                    var distanceBetweenPlant = Vector2.Distance(clickerItemComponent.ClickerItemMono.RectTransform.anchoredPosition, 
                        neighboringClickerItemComponent.ClickerItemMono.RectTransform.anchoredPosition);

                    if (distanceBetweenPlant <= bonus_distance)
                    {
                        clickerItemComponent.BonusPlants.Add(neighboringClickerItemComponent.GUID);
                    }
                }
            }
            
            CalculateBonus();
        }

        private void CalculateBonus()
        {
            var clickerItemsEntities = _dataWorld.Select<ClickerItemComponent>().GetEntities();

            foreach (var clickerItemEntity in clickerItemsEntities)
            {
                ref var clickerItemComponent = ref clickerItemEntity.GetComponent<ClickerItemComponent>();
                clickerItemComponent.BonusValuePerSecond = 0;
                
                foreach (var bonusPlantGuid in clickerItemComponent.BonusPlants)
                {
                    var bonusPlantComponent = _dataWorld.Select<ClickerItemComponent>()
                        .Where<ClickerItemComponent>(item => item.GUID == bonusPlantGuid)
                        .SelectFirstEntity()
                        .GetComponent<ClickerItemComponent>();
                    
                    var valueBonus = bonusPlantComponent.AddCurrencyPerSecond * clickerItemComponent.PowerBonus;
                    clickerItemComponent.BonusValuePerSecond += valueBonus;
                }

                clickerItemComponent.ClickerItemMono
                    .UpdateValuePerSecondFlower(clickerItemComponent.AddCurrencyPerSecond, clickerItemComponent.BonusValuePerSecond);
            }
        }
        
        public void Destroy()
        {
            BonusPlantAction.EndMoveClickerItem -= EndMoveClickerItem;
            BonusPlantAction.UpdateViewBonus -= CalculateBonus;
        }
    }
}