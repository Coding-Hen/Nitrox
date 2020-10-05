using NitroxModel.Logger;
using NitroxModel.MultiplayerSession;
using NitroxModel.Packets;
using NitroxModel.Server;
using NitroxServer.Communication.NetworkingLayer;
using NitroxServer.Communication.Packets.Processors.Abstract;

namespace NitroxServer.Communication.Packets.Processors
{
    public class MultiplayerPasswordAuthenticationRequestProcessor : UnauthenticatedPacketProcessor<MultiplayerPasswordAuthenticationRequest>
    {
        private readonly ServerConfig serverConfig;

        public MultiplayerPasswordAuthenticationRequestProcessor(ServerConfig serverConfig)
        {
            this.serverConfig = serverConfig;
        }

        public override void Process(MultiplayerPasswordAuthenticationRequest packet, NitroxConnection connection)
        {
            Log.Info("Processing password request...");

            string correlationId = packet.CorrelationId;
            AuthenticationContext authenticationContext = packet.AuthenticationContext;

            MultiplayerPasswordAuthentication passwordAuthenticationPacket;

            if (!string.IsNullOrEmpty(serverConfig.ServerPassword) && (!authenticationContext.ServerPassword.HasValue || authenticationContext.ServerPassword.Value != serverConfig.ServerPassword))
            {
                MultiplayerSessionReservationState rejectedState = MultiplayerSessionReservationState.REJECTED | MultiplayerSessionReservationState.AUTHENTICATION_FAILED;
                passwordAuthenticationPacket = new MultiplayerPasswordAuthentication(correlationId, rejectedState);
            }
            else
            {
                passwordAuthenticationPacket = new MultiplayerPasswordAuthentication(correlationId);
            }

            Log.Info($"Password Authentication processed successfully: {passwordAuthenticationPacket}...");

            connection.SendPacket(passwordAuthenticationPacket);
        }
    }
}
