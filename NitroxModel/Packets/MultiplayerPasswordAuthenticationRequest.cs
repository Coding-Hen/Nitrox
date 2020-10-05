using System;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerPasswordAuthenticationRequest: CorrelatedPacket
    {
        public AuthenticationContext AuthenticationContext { get; }

        public MultiplayerPasswordAuthenticationRequest(string reservationCorrelationId, AuthenticationContext authenticationContext)
            : base(reservationCorrelationId)
        {
            AuthenticationContext = authenticationContext;
        }
    }
}
