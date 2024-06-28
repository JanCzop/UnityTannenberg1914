using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public const int UNIT_BASE_HP = 4;

    private (string division, string corp, string army) unit_name;
    private (int normal, int declassed) firepower;
    private (int normal, int declassed) efficiency;
    private (int normal, int declassed) movement;
    private bool is_commanded = false;

    private int hitpoints;
    private float remaining_movement;

    private Unit_alliegance alliegance;
    private Unit_condition condition;
    private Unit_type type;
    private General general_data;

    //////////////////////////////
    private Hex hex;
    ///////////////////////////

    private Unit_movement_handler movement_handler;


    public enum Unit_alliegance { GERMAN, RUSSIAN }
    public enum Unit_condition { NORMAL, DOWNGRADED }
    public enum Unit_type {INFANTRY, CAVALRY, ARTILLERY, GENERAL, SUPPLY_WAGON}


    public class General{
        private General_initiative initiative;
        private (int normal, int declassed) commmand_range;
        private General_rank rank;
        private General_order order;


        public enum General_initiative{ACTIVE,NORMAL,PASSIVE}
        public enum General_rank{ARMY,CORP}
        public enum General_order{MARCH,STOP}
        
        public General_rank Rank { get => rank; set => rank = value; }
        public (int normal, int declassed) Commmand_range { get => commmand_range; set => commmand_range = value; }
        public General_initiative Initiative { get => initiative; set => initiative = value; }
        public General_order Order { get => order; set => order = value; }
    }



    public string Get_unit_name(){
        return this.unit_name.ToString();
    }

    public int Get_current_firepower(){
        return condition == Unit_condition.NORMAL ? firepower.normal : firepower.declassed;
    }
    public int Get_current_efficiency(){
        return condition == Unit_condition.NORMAL ? efficiency.normal : efficiency.declassed;
    }
    public int Get_current_movement(){
        return condition == Unit_condition.NORMAL ? movement.normal : movement.declassed; 
    }

    public void Swap_status(){
        if(condition == Unit_condition.NORMAL) condition = Unit_condition.DOWNGRADED;
        else if (condition == Unit_condition.DOWNGRADED) condition = Unit_condition.NORMAL;
    }


    public void Move(Vector3 start, Vector3 end, float velocity)
    {
        if (movement_handler != null) movement_handler.MoveUnit(gameObject, start, end, velocity);
        else Debug.LogError("Error: MovementHandler is not assigned.");
    }


    public (int normal, int declassed) Firepower { get => firepower; set => firepower = value; }
    public (int normal, int declassed) Effeciency { get => efficiency; set => efficiency = value; }
    public (int normal, int declassed) Movement { get => movement; set => movement = value; }
    public int Hitpoints { get => hitpoints; set => hitpoints = value; }
    public float Remaining_movement { get => remaining_movement; set => remaining_movement = value; }
    public Unit_alliegance Alliegance { get => alliegance; set => alliegance = value; }
    public Unit_condition Condition { get => condition; set => condition = value; }
    public Unit_type Type { get => type; set => type = value; }
    public (string division, string corp, string army) Unit_name { get => unit_name; set => unit_name = value; }
    public Hex Hex { get => hex; set => hex = value; }
    public Unit_movement_handler Movement_handler { get => movement_handler; set => movement_handler = value; }
    public bool Is_commanded { get => is_commanded; set => is_commanded = value; }
    public General General_data { get => general_data; set => general_data = value; }
}
