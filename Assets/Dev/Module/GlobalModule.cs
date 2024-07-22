using ModulesFramework.Attributes;
using ModulesFramework.Modules;
using ModulesFrameworkUnity;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.GameUI;
using FlowerRoom.Core.Traderow;
using Input;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EcsCore
{
    [GlobalModule]
    public class GlobalModule : EcsModule
    {
        private List<Object> _resource = new List<Object>();

        protected override async Task Setup()
        {
            var tasks = new List<Task>();

            var gameUI = Load<GameObject>("GameUI", tasks);
            var traderowConfigTask = Load<TraderowConfigSO>("TraderowConfig", tasks);
            var clickerConfigTask = Load<ClickerConfigSO>("ClickerConfig", tasks);
            var clickerItemsViewTask = Load<ClickerItemsViewSO>("ClickerItemsViewConfig", tasks);
            var input = Load<GameObject>("Input", tasks);

            var alltask = Task.WhenAll(tasks.ToArray());
            await alltask;

            var gameUIInstance = Object.Instantiate(gameUI.Result).GetComponent<GameUIMono>();
            var inputGOInstance = Object.Instantiate(input.Result);
            
            var clickerItemView = clickerItemsViewTask.Result;
            
            world.CreateOneData(new GameUIData { GameUI = gameUIInstance });
            world.CreateOneData(new InputData { PlayerInput = inputGOInstance.GetComponent<PlayerInput>() });
            world.CreateOneData(new ClickerConfigData
            {
                ClickerConfigSO = clickerConfigTask.Result,
                ItemView = clickerItemView.ItemsView,
                PowerSwitchView = clickerItemView.PowerSwitchView,
            });
            
            var traderowConfig = traderowConfigTask.Result;
            world.CreateOneData(new TraderowData
            {
                TraderowConfigSO = traderowConfig,
                TraderowConfig = traderowConfig.TraderowConfig,
                ItemInTraderowMonoPrefab = traderowConfig.ItemInTraderowMonoPrefab,
            });
            
            ModulesUnityAdapter.world.InitModule<CoreModule>(true);
        }

        private Task<T> Load<T>(string name, List<Task> tasks)
        {
            var task = Addressables.LoadAssetAsync<T>(name).Task;
            tasks.Add(task);
            return task;
        }

        public override void OnDeactivate()
        {
            foreach (var item in _resource)
                Object.Destroy(item);
        }
    }
}