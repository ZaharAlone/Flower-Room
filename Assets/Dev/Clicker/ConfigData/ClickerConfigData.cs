using System.Collections.Generic;
using FlowerRoom.Core.Clicker.UpgradeItems;

namespace FlowerRoom.Core.Clicker
{
    public struct ClickerConfigData
    {
        public ClickerConfigSO ClickerConfigSO;
        public Dictionary<string, ItemViewTraderow> ItemView;
        public List<int> PowerSwitchView;
        
        public ProgressionPriceUpgradeClickerItemConfig ProgressionPriceUpgradeClickerItemConfig;
    }
}