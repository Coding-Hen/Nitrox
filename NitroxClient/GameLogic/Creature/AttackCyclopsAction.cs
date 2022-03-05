using NitroxClient.MonoBehaviours;
using NitroxModel.DataStructures;
using NitroxModel_Subnautica.DataStructures.GameLogic.Creatures.Actions;
using UnityEngine;

namespace NitroxClient.GameLogic.Creature
{
    internal class AttackCyclopsAction : SerializableCreatureAction
    {
        public NitroxId currentTargetId;
        public bool currentTargetIsDecoy;

        public AttackCyclopsAction(NitroxId currentTargetId, bool currentTargetIsDecoy)
        {
            this.currentTargetId = currentTargetId;
            this.currentTargetIsDecoy = currentTargetIsDecoy;
        }

        public CreatureAction GetCreatureAction(GameObject gameObject)
        {
            AttackCyclops attackCyclops = gameObject.GetComponent<AttackCyclops>();
            GameObject target = NitroxEntity.RequireObjectFrom(currentTargetId);
            attackCyclops.SetCurrentTarget(target, currentTargetIsDecoy);
            return attackCyclops;
        }
    }
}
