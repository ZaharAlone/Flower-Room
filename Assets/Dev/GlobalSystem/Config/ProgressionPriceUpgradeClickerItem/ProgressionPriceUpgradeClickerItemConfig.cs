using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlowerRoom.Core.Clicker.UpgradeItems
{
    [Serializable]
    public struct ProgressionPriceUpgradeClickerItemConfig
    {
        [JsonProperty("watering_prices")]
        public List<int> WateringPrices;
        [JsonProperty("weeding_prices")]
        public List<int> WeedingPrices;
        [JsonProperty("fertilizing_prices")]
        public List<int> FertilizingPrices;
    }
}