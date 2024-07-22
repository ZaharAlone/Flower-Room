using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlowerRoom.Core.Clicker.UpgradeItems
{
    [Serializable]
    public struct ProgressionPriceUpgradeClickerItemConfig
    {
        [JsonProperty("watering_prices")]
        public List<ProgressionPriceValue> WateringPrices;
        [JsonProperty("weeding_prices")]
        public List<ProgressionPriceValue> WeedingPrices;
        [JsonProperty("fertilizing_prices")]
        public List<ProgressionPriceValue> FertilizingPrices;
    }

    [Serializable]
    public struct ProgressionPriceValue
    {
        [JsonProperty("value")]
        public float Value;
        [JsonProperty("price")]
        public int Price;
    }
}