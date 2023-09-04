using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.RPGSystem
{
    public interface IStatModifierSource
    {
        string Name { get; }
        string Description { get; }
    }
}