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

    private int hitpoints;
    private float remaining_movement;

    private Allegiance alliegance;
    private Unit_condition condition;
    private Unit_type type;
    private General general_data;
    private bool is_train_transported;

    //////////////////////////////
    private Hex hex;
    ///////////////////////////

    private Unit_movement_handler movement_handler;


    public enum Unit_condition { NORMAL, DOWNGRADED }
    public enum Unit_type {INFANTRY, CAVALRY, ARTILLERY, GENERAL, SUPPLY_WAGON}
    public static Allegiance Get_enemy_alliegance(Allegiance alliegance){
        return alliegance == Allegiance.GERMAN ? Allegiance.RUSSIAN : Allegiance.GERMAN;
    }

    public class General{
        public const int MAX_ADDITIONAL_UNIT_CAP_CORP = 2;
        public const int MAX_ADDITIONAL_UNIT_CAP_ARMY = 5;
        private General_initiative initiative;
        private General_order last_turn_order;
        private (int normal, int declassed) commmand_range;
        private General_rank rank;
        private General_order order;
        private List<Unit> additional_commanded_units;

        public General(General_initiative initiative, (int normal, int declassed) commmand_range, General_rank rank, General_order order)
        {
            this.initiative = initiative;
            this.commmand_range = commmand_range;
            this.rank = rank;
            this.order = order;
            this.Last_turn_order = order;
            this.additional_commanded_units = new();
        }

        public enum General_initiative{ACTIVE,NORMAL,PASSIVE}
        public enum General_rank{ARMY,CORP}
        public enum General_order{MARCH,STOP}
        
        public General_rank Rank { get => rank; set => rank = value; }
        public (int normal, int declassed) Commmand_range { get => commmand_range; set => commmand_range = value; }
        public General_initiative Initiative { get => initiative; set => initiative = value; }
        public General_order Order { get => order; set => order = value; }
        public List<Unit> Additional_commanded_units { get => additional_commanded_units; set => additional_commanded_units = value; }
        public General_order Last_turn_order { get => last_turn_order; set => last_turn_order = value; }
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

    public int Get_current_control_range(){
        if(this.general_data == null) return 0;
        return condition == Unit_condition.NORMAL ? General_data.Commmand_range.normal : General_data.Commmand_range.declassed;
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
    public Allegiance Alliegance { get => alliegance; set => alliegance = value; }
    public Unit_condition Condition { get => condition; set => condition = value; }
    public Unit_type Type { get => type; set => type = value; }
    public (string division, string corp, string army) Unit_name { get => unit_name; set => unit_name = value; }
    public Hex Hex { get => hex; set => hex = value; }
    public Unit_movement_handler Movement_handler { get => movement_handler; set => movement_handler = value; }
    public General General_data { get => general_data; set => general_data = value; }
    public bool Is_train_transported { get => is_train_transported; set => is_train_transported = value; }
}
