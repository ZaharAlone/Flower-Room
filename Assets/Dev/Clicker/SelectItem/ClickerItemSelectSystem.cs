using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker.Items;

namespace FlowerRoom.Core.Clicker.SelectItem
{
    [EcsSystem(typeof(CoreModule))]
    public class ClickerItemSelectSystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void PreInit()
        {
            ClickerAction.SelectItem += SelectItem;
            ClickerAction.DeselectItem += DeselectItem;
        }
        
        private void SelectItem(string guid)
        {
            DeselectItem();
            
            var selectItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.GUID == guid)
                .SelectFirstEntity();

            selectItemEntity.AddComponent(new ClickerItemSelectComponent());
            EnableViewBonus(true);
        }

        private void EnableViewBonus(bool isShow)
        {
            var selectItemEntity = _dataWorld.Select<ClickerItemSelectComponent>().SelectFirstEntity();
            var selectItemComponent = selectItemEntity.GetComponent<ClickerItemComponent>();
            
            selectItemComponent.ClickerItemMono.EnablePerSecondValueCurrency(isShow);

            foreach (var bonusPlantGuid in selectItemComponent.BonusPlants)
            {
                var bonusPlantComponent = _dataWorld.Select<ClickerItemComponent>()
                    .Where<ClickerItemComponent>(item => item.GUID == bonusPlantGuid)
                    .SelectFirstEntity()
                    .GetComponent<ClickerItemComponent>();

                bonusPlantComponent.ClickerItemMono.EnablePerSecondValueCurrency(isShow);
            }
        }

        private void DeselectItem()
        {
            var selectItemsQuery = _dataWorld.Select<ClickerItemSelectComponent>();

            if (selectItemsQuery.Count() > 0)
            {
                EnableViewBonus(false);

                var selectItemEntities = selectItemsQuery.GetEntities();

                foreach (var selectItemEntity in selectItemEntities)
                {
                    selectItemEntity.RemoveComponent<ClickerItemSelectComponent>();
                }
            }
        }

        public void Destroy()
        {
            ClickerAction.SelectItem -= SelectItem;
            ClickerAction.DeselectItem -= DeselectItem;
        }
    }
}