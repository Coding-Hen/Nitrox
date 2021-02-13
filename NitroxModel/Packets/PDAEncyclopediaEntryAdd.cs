using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PDAEncyclopediaEntryAdd : Packet
    {
        public string Key;
        public bool PostNotification;

        public PDAEncyclopediaEntryAdd(string key, bool postNotification)
        {
            Key = key;
            PostNotification = postNotification;
        }

        public override string ToString()
        {
            return $"[PDAEncyclopediaEntryAdd - Key: {Key}, PostNotification: {PostNotification}]";
        }
    }
}
