using NitroxClient.Communication.Abstract;
using NitroxClient.MonoBehaviours;
using NitroxModel.DataStructures;
using NitroxModel.Logger;
using NitroxModel.Packets;

namespace NitroxClient.GameLogic
{
    public class Precursor
    {
        private readonly IPacketSender packetSender;
        private readonly IMultiplayerSession multiplayerSession;

        public Precursor(IPacketSender packetSender, IMultiplayerSession multiplayerSession)
        {
            this.packetSender = packetSender;
            this.multiplayerSession = multiplayerSession;
        }

        public void OnDoorToggledByMe(PrecursorDoorway precursorDoorway, bool open)
        {
            NitroxId id = NitroxIdentifier.GetId(precursorDoorway.gameObject);

            PrecursorDoorwayChanged precursorDoor = new PrecursorDoorwayChanged(id, open);
            Log.Debug("Sending packet for Doorway" + precursorDoor.ToString());
            packetSender.Send(precursorDoor);
            Log.Debug("Sent packet for Doorway" + precursorDoor.ToString());
        }

        public void OnKeyColunm(PrecursorDoorKeyColumn precursorDoorway)
        {
            Log.Debug("Key has had " + precursorDoorway.name);
        }
    }
}
