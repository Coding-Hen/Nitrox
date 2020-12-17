﻿using System;
using System.Collections.Generic;
using Mono.Nat;
using NitroxModel.DataStructures;
using NitroxModel.Logger;
using NitroxModel.Packets;
using NitroxServer.Communication.Packets;
using NitroxServer.GameLogic;
using NitroxServer.GameLogic.Entities;
using NitroxServer.Serialization;

namespace NitroxServer.Communication.NetworkingLayer
{
    public abstract class NitroxServer
    {
        protected bool isStopped = true;
        protected int portNumber, maxConn;

        protected readonly PacketHandler packetHandler;
        protected readonly EntitySimulation entitySimulation;
        protected readonly Dictionary<long, NitroxConnection> connectionsByRemoteIdentifier = new Dictionary<long, NitroxConnection>();
        protected readonly PlayerManager playerManager;

        public NitroxServer(PacketHandler packetHandler, PlayerManager playerManager, EntitySimulation entitySimulation, ServerConfig serverConfig)
        {
            this.packetHandler = packetHandler;
            this.playerManager = playerManager;
            this.entitySimulation = entitySimulation;

            portNumber = serverConfig.ServerPort;
            maxConn = serverConfig.MaxConnections;
            SetupUPNP();
        }

        public abstract bool Start();

        public abstract void Stop();

        protected void ClientDisconnected(NitroxConnection connection)
        {
            Player player = playerManager.GetPlayer(connection);

            if (player != null)
            {
                playerManager.PlayerDisconnected(connection);

                Disconnect disconnect = new Disconnect(player.Id);
                playerManager.SendPacketToAllPlayers(disconnect);

                List<SimulatedEntity> ownershipChanges = entitySimulation.CalculateSimulationChangesFromPlayerDisconnect(player);

                if (ownershipChanges.Count > 0)
                {
                    SimulationOwnershipChange ownershipChange = new SimulationOwnershipChange(ownershipChanges);
                    playerManager.SendPacketToAllPlayers(ownershipChange);
                }
            }
        }

        protected void ProcessIncomingData(NitroxConnection connection, Packet packet)
        {
            try
            {
                packetHandler.Process(packet, connection);
            }
            catch (Exception ex)
            {
                Log.Error("Exception while processing packet: " + packet + " " + ex);
            }
        }

        private void SetupUPNP()
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.StartDiscovery();
        }

        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                device.CreatePortMapAsync(new Mapping(Protocol.Udp, portNumber, portNumber, 0, "Nitrox Server - Subnautica"));
            }
            catch (Exception e)
            {
                Log.Error("Error occurred setting up UPNP");
            }
        }
    }
}
