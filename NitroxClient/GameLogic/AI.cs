using NitroxClient.Communication.Abstract;
using NitroxClient.GameLogic.Creature;
using NitroxModel.DataStructures;
using NitroxModel_Subnautica.DataStructures.GameLogic.Creatures.Actions;
using NitroxModel_Subnautica.Packets;

namespace NitroxClient.GameLogic
{
    public class AI
    {
        private readonly IPacketSender packetSender;

        public AI(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void CreatureActionChanged(NitroxId id, CreatureAction newAction)
        {
            SerializableCreatureAction creatureAction = null;

            /*
            Example for next implementation:

            if (newAction.GetType() == typeof(SwimToPoint))
            {
                creatureAction = new SwimToPointAction(((SwimToPoint)newAction).Target);
            }*/

            if (newAction.GetType() == typeof(AttackCyclops))
            {
                AttackCyclops action = (AttackCyclops)newAction;
                NitroxId currentTargetId = action.currentTarget.GetComponent<NitroxId>();
                Log.Debug($"Attacking cyclops {currentTargetId} {action.currentTargetIsDecoy}");
                creatureAction = new AttackCyclopsAction(currentTargetId, action.currentTargetIsDecoy);
            }

            if (creatureAction != null)
            {
                CreatureActionChanged actionChanged = new CreatureActionChanged(id, creatureAction);
                packetSender.Send(actionChanged);
            }
        }
    }
}
