using System;
using System.Collections.Generic;
using GUIPackage;
using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    public class ItemInfo
    {
        [JsonProperty("Num")]
        public int Number = 1;
        [JsonProperty("ItemID")]
        public int Id = 10000;
        [JsonProperty("UUID")]
        public string Uuid = "无";
        public item ToItem()
        {
            item item;
            try
            {
                item = new item(Id);
                item.itemNum = Number;
                item.UUID = Uuid;
            }
            catch (Exception e)
            {
                item = new item(10000);
                item.UUID = "无";
                item.itemNum = 1;
            }
            return item;
        }
    }
}