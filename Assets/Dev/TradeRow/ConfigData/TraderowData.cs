using System.Collections.Generic;

namespace FlowerRoom.Core.Traderow
{
    public struct TraderowData
    {
        public TraderowConfigSO TraderowConfigSO;
        public TraderowConfig TraderowConfig;
        public Dictionary<string, ItemViewTraderow> ItemViewTraderows;
        public ItemInTraderowMono ItemInTraderowMonoPrefab;
    }
}