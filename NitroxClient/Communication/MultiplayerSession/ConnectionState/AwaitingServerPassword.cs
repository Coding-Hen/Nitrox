using System;
using NitroxClient.Communication.Abstract;
using NitroxModel.Helper;
using NitroxModel.Logger;

namespace NitroxClient.Communication.MultiplayerSession.ConnectionState
{
    class AwaitingServerPassword : ConnectionNegotiatingState
    {
        public override MultiplayerSessionConnectionStage CurrentStage => MultiplayerSessionConnectionStage.AWAITING_SERVER_PASSWORD;
        public override void NegotiateReservation(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            try
            {
                Log.Debug("About to negociate state validation");
                ValidateState(sessionConnectionContext);
                AwaitReservationCredentials(sessionConnectionContext);
            }
            catch (Exception)
            {
                Disconnect(sessionConnectionContext);
                throw;
            }
        }

        private void ValidateState(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            ClientIsConnected(sessionConnectionContext);
            AuthenticationContextIsNotNull(sessionConnectionContext);
        }

        private static void ClientIsConnected(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            if (!sessionConnectionContext.Client.IsConnected)
            {
                throw new InvalidOperationException("The client is not connected.");
            }
        }

        private static void AuthenticationContextIsNotNull(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            try
            {
                Validate.NotNull(sessionConnectionContext.AuthenticationContext);
            }
            catch (ArgumentNullException ex)
            {
                throw new InvalidOperationException("The context does not contain an authentication context.", ex);
            }
        }

        private void AwaitReservationCredentials(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            Log.Debug("Negociating next state");
            AwaitingReservationCredentials nextState = new AwaitingReservationCredentials();
            sessionConnectionContext.UpdateConnectionState(nextState);
        }
    }
}
