using System.Collections.Generic;
using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using FlowerRoom.Core.Clicker.UpgradeItems;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.MoveItem;
using Input;
using Tools;
using Object = UnityEngine.Object;

namespace FlowerRoom.Core.Clicker.Items
{
    [EcsSystem(typeof(CoreModule))]
    public class ClickerAddNewItemSystem : IPreInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        private GameObject _containerClickerItems;

        private const float power_bonus_plant = 0.2f;
        
        public void PreInit()
        {
            ClickerAction.AddNewItemScene += AddNewItemScene;
        }
        
        private void AddNewItemScene(string keyItem)
        {
            var viewNewItem = _dataWorld.OneData<ClickerConfigData>().ItemView[keyItem];
            var gameUI = _dataWorld.OneData<GameUIData>().GameUI;
            var containerItems = gameUI.ClickerItemsContainer;
            var mousePositions = _dataWorld.OneData<InputData>().MousePosition;
            var newClickerItemMono = Object.Instantiate(viewNewItem.ItemPrefab, containerItems.transform);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                gameUI.Canvas.transform as RectTransform, mousePositions, gameUI.Canvas.worldCamera, out Vector2 localPoint);

            newClickerItemMono.RectTransform.anchoredPosition = localPoint;
            
            var guidNewItem = CreateGUID.Create();
            newClickerItemMono.SetGUID(guidNewItem);

            var countClickerItemsInScene = _dataWorld.Select<ClickerItemComponent>().Count();
            
            var newItemComponent = new ClickerItemComponent
            {
                KeyItem = keyItem,
                GUID = guidNewItem,
                PowerBonus = power_bonus_plant,
                ClickerItemMono = newClickerItemMono,
                WateringCountBuy = countClickerItemsInScene,
                WeedingCountBuy = countClickerItemsInScene,
                FertilizingCountBuy = countClickerItemsInScene,
                BonusPlants = new List<string>(),
            };

            var entityNewItem = _dataWorld.NewEntity();
            entityNewItem.AddComponent(newItemComponent);

            entityNewItem.AddComponent(new InteractiveMoveComponent
            {
                StartMousePositions = mousePositions,
            });
            
            newClickerItemMono.ForceHidePanelUpgrade();
            
            UpgradeItemAction.UpdateUIUpgradeItem?.Invoke(guidNewItem);
        }

        public void Destroy()
        {
            ClickerAction.AddNewItemScene -= AddNewItemScene;
        }
    }
}