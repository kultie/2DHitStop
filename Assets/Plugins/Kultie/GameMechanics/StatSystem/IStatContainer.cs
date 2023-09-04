using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.RPGSystem
{
    public interface IStatContainer
    {
        Stat GetStat(Stat.Key key);
        float GetStatValue(Stat.Key key);
    }
}