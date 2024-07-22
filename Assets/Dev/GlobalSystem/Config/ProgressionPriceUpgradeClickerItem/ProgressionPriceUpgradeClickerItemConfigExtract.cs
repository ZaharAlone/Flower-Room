using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using FlowerRoom.Core.Clicker;
using FlowerRoom.Core.Clicker.UpgradeItems;
using Newtonsoft.Json;

namespace FlowerRoom.GlobalSystem.Config
{
    [EcsSystem(typeof(GlobalModule))]
    public class ProgressionPriceUpgradeClickerItemConfigExtract : IInitSystem
    {
        private DataWorld _dataWorld;

        public void Init()
        {
            ref var clickerConfigData = ref _dataWorld.OneData<ClickerConfigData>();
            clickerConfigData.ProgressionPriceUpgradeClickerItemConfig =
                JsonConvert.DeserializeObject<ProgressionPriceUpgradeClickerItemConfig>(clickerConfigData.ClickerConfigSO.ProgreessionPriceUpgradeConfig.text);
        }
    }
}