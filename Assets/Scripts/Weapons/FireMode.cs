using System.Collections;
using System.Collections.Generic;
using GunGame.Guns;
using UnityEngine;

namespace GunGame.Guns
{
    public abstract class FireMode : ScriptableObject
    {
        public abstract string modeName { get; }
        public abstract void FireType(GunStats gunStats);

    }
}

