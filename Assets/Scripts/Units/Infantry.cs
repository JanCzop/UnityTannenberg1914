using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit
{
    public override string UNIT_PREFAB_FILEPATH_GERMAN => throw new System.NotImplementedException();
    public override string UNIT_PREFAB_FILEPATH_RUSSIAN => throw new System.NotImplementedException();

    public override GameObject Get_unit_prefab(Unit_alliegance alliegance)
    {
        return alliegance switch
        {
            Unit_alliegance.GERMAN => null,
            Unit_alliegance.RUSSIAN => null,
            _ => throw new ArgumentException("Error: Allegiance should be RUSSIAN/GERMAN but is -" + alliegance.ToString()),
        };
    }
}
