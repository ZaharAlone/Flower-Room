using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.CurrencyFlower;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.MoveItem;
using Input;
using Object = UnityEngine.Object;

namespace FlowwerRoom.Core.Clicker
{
    [EcsSystem(typeof(CoreModule))]
    public class ClickerGameMechanicsSystem : IPreInitSystem, IInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        private const int start_value_power_click = 1;

        public void PreInit()
        {
            ClickerAction.ChangeAddPowerClick += ChangeAddPowerClick;
            ClickerAction.ClickItem += ClickItem;
        }

        public void Init()
        {
            InitFirstSessionPowerClick();
        }

        private void InitFirstSessionPowerClick()
        {
            _dataWorld.CreateOneData(new ClickerPowerData
            {
                ClickPower = start_value_power_click
            });
        }
        
        private void ChangeAddPowerClick(int addPowerClick)
        {
            ref var clickerPowerData = ref _dataWorld.OneData<ClickerPowerData>();
            clickerPowerData.ClickPower += addPowerClick;
        }

        private void ClickItem(string keyItem)
        {
            var isMoveItem = _dataWorld.Select<InteractiveMoveComponent>().Count() > 0;
            if (isMoveItem)
                return;
            
            var clickPower = _dataWorld.OneData<ClickerPowerData>().ClickPower;
            CurrencyFlowerAction.AddCurrencyFlower?.Invoke(clickPower);
            SpawnClickerAnimations(clickPower);
        }

        private void SpawnClickerAnimations(int value)
        {
            var uiContainerForSpawnElement = _dataWorld.OneData<GameUIData>().GameUI.ClickerAnimationsContainer;
            var spawnElement = _dataWorld.OneData<ClickerConfigData>().ClickerConfigSO.ClickerAnimationsTextMono;
            var spawnMono = Object.Instantiate(spawnElement, uiContainerForSpawnElement);

            var mousePositions = _dataWorld.OneData<InputData>().MousePosition;
            var gameUI = _dataWorld.OneData<GameUIData>().GameUI;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                gameUI.Canvas.transform as RectTransform, mousePositions, gameUI.Canvas.worldCamera, out Vector2 localPoint);
            spawnMono.RectTransform.anchoredPosition = localPoint;
            
            spawnMono.StartAnimations(value);
        }

        public void Destroy()
        {
            ClickerAction.ChangeAddPowerClick -= ChangeAddPowerClick;
            ClickerAction.ClickItem -= ClickItem;
        }
    }
}