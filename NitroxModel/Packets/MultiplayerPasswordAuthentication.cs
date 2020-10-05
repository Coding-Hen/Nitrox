using System;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerPasswordAuthentication : CorrelatedPacket
    {
        public MultiplayerSessionReservationState ReservationState { get; }

        public MultiplayerPasswordAuthentication(string correlationId) : base(correlationId)
        {
        }

        public MultiplayerPasswordAuthentication(string correlationId, MultiplayerSessionReservationState reservationState)
            : base(correlationId)
        {
            ReservationState = reservationState;
        }

        public override string ToString()
        {
            return $"ReservationState: {ReservationState}";
        }
    }
}
