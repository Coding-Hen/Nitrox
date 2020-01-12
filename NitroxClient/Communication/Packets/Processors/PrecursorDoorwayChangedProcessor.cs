using NitroxClient.Communication.Abstract;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.MonoBehaviours;
using NitroxClient.Unity.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class PrecursorDoorwayChangedProcessor : ClientPacketProcessor<PrecursorDoorwayChanged>
    {
        private readonly IPacketSender packetSender;

        public PrecursorDoorwayChangedProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        /// <summary>
        /// Finds and executes <see cref="PrecursorDoorway.ToggleDoor(bool)"/>.
        /// </summary>
        public override void Process(PrecursorDoorwayChanged packet)
        {
            GameObject precursorDoorGameObject = NitroxIdentifier.RequireObjectFrom(packet.Id);

            Log.Debug("precursorDoorGameObject " + precursorDoorGameObject.name + " state: " + packet.Open);
            precursorDoorGameObject.RequireComponent<PrecursorDoorway>().ToggleDoor(packet.Open);
        }
    }
}
