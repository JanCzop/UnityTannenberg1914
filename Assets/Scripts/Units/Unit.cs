using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{



    private (int normal, int declassed) firepower;
    private (int normal, int declassed) effeciency;
    private (int normal, int declassed) movement;

    private int hitpoints;
    private float remaining_movement;

    private Unit_alliegance alliegance;
    private Unit_condition condition;
    private Unit_type type;

    public enum Unit_alliegance { GERMAN, RUSSIAN }
    public enum Unit_condition { NORMAL, DOWNGRADED }
    public enum Unit_type {INFANTRY, CAVALRY, ARTILLERY, GENERAL, SUPPLY_WAGON}


    public (int normal, int declassed) Firepower { get => firepower; set => firepower = value; }
    public (int normal, int declassed) Effeciency { get => effeciency; set => effeciency = value; }
    public (int normal, int declassed) Movement { get => movement; set => movement = value; }
    public int Hitpoints { get => hitpoints; set => hitpoints = value; }
    public float Remaining_movement { get => remaining_movement; set => remaining_movement = value; }
    public Unit_alliegance Alliegance { get => alliegance; set => alliegance = value; }
    public Unit_condition Condition { get => condition; set => condition = value; }
    public Unit_type Type { get => type; set => type = value; }
}
