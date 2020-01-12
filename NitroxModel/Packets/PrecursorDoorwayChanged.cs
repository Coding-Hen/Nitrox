using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    /// <summary>
    /// Triggered when a Precursor Door has been opened/closed
    /// </summary>
    [Serializable]
    public class PrecursorDoorwayChanged : Packet
    {
        public NitroxId Id { get; }
        public bool Open { get; }

        /// <param name="id">The Fire id</param>
        /// <param name="open">Whether the door is open. True will disable the field calling <see cref="PrecursorDoorway.DisableField"/>. False will disable the field calling <see cref="PrecursorDoorway.EnableField"/>.</param>
        public PrecursorDoorwayChanged(NitroxId id, bool open)
        {
            Id = id;
            Open = open;
        }

        public override string ToString()
        {
            return "[PrecursorDoorwayChanged Id: " + Id + " Open: " + Open + "]";
        }
    }
}
