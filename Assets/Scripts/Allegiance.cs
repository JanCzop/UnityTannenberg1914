using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allegiance
{
   RUSSIAN,GERMAN
}

public class Allegiance_helper{
   public static Allegiance Get_enemy_alliegance(Allegiance alliegance){
        return alliegance == Allegiance.GERMAN ? Allegiance.RUSSIAN : Allegiance.GERMAN;
    }
}


