using eBayPulse.Tools;
using System;

namespace eBayPulse.eBayApi.Call
{
    public class GetItem: Call
    {
        public GetItem(Context context, string itemId)
            : base(context, CallName.GetItem)
        {
            CallSpecificInputXml = $"<ItemID>{itemId}</ItemID>";
        }

        public string ItemId {get; private set;}
        public string Name {get; private set;}
        public int HitCount {get; private set;}

        protected override bool ParseSpecificOutput(XmlReader xmlReader)
        {
            ItemId = xmlReader.GetValue("ItemID");
            Name = xmlReader.GetValue("Title");
            HitCount = Convert.ToInt32(xmlReader.GetValue("HitCount"));
            return true;
        }
    }
}
