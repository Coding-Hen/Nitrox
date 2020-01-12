using NitroxModel.Logger;
using NitroxModel.Packets;
using NitroxServer.Communication.Packets.Processors.Abstract;
using NitroxServer.GameLogic;

namespace NitroxServer.Communication.Packets.Processors
{
    class PrecursorDoorwayChangedProcessor : AuthenticatedPacketProcessor<PrecursorDoorwayChanged>
    {
        private readonly PlayerManager playerManager;

        public PrecursorDoorwayChangedProcessor(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public override void Process(PrecursorDoorwayChanged packet, Player simulatingPlayer)
        {
            Log.Debug("Sending packet to all players " + packet.ToString());
            playerManager.SendPacketToOtherPlayers(packet, simulatingPlayer);
            Log.Debug("Sent packet to all players " + packet.ToString());
        }
    }
}
