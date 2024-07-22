using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.Traderow;
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
        
        public void PreInit()
        {
            ClickerAction.AddNewItemScene += AddNewItemScene;
        }
        
        private void AddNewItemScene(string keyItem)
        {
            var viewNewItem = _dataWorld.OneData<TraderowData>().ItemViewTraderows[keyItem];
            var containerItems = _dataWorld.OneData<GameUIData>().GameUI.ClickerItemsContainer;
            var mousePositions = _dataWorld.OneData<InputData>().MousePosition;
            //TODO добавить перемещение и установку предмета
            var newClickerItemMono = Object.Instantiate(viewNewItem.ItemPrefab, containerItems.transform);
            //newItem.transform.position = mousePositions.Value;

            var guidNewItem = CreateGUID.Create();
            newClickerItemMono.SetGUID(guidNewItem);

            var countClickerItemsInScene = _dataWorld.Select<ClickerItemComponent>().Count();
            
            var newItemComponent = new ClickerItemComponent
            {
                KeyItem = keyItem,
                GUID = guidNewItem,
                ClickerItemMono = newClickerItemMono,
                WateringCountBuy = countClickerItemsInScene,
                WeedingCountBuy = countClickerItemsInScene,
                FertilizingCountBuy = countClickerItemsInScene,
            };

            _dataWorld.NewEntity().AddComponent(newItemComponent);
        }

        public void Destroy()
        {
            ClickerAction.AddNewItemScene -= AddNewItemScene;
        }
    }
}