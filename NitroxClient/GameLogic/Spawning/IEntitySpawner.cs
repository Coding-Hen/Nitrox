﻿using System.Collections;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using UnityEngine;

namespace NitroxClient.GameLogic.Spawning
{
    /**
     * Allows us to create custom entity spawners for different entity types.
     */
    public interface IEntitySpawner
    {
#if SUBNAUTICA
        Optional<GameObject> Spawn(Entity entity, Optional<GameObject> parent, EntityCell cellRoot);
#elif BELOWZERO
        IEnumerator Spawn(TaskResult<Optional<GameObject>> result, Entity entity, Optional<GameObject> parent, EntityCell cellRoot);
#endif
        bool SpawnsOwnChildren();
    }
}
