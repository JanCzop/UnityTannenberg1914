using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_map : MonoBehaviour
{
    Control_map map_control;
    private bool[][] russian_command_map;
    private bool[][] german_command_map;

    private static System.Random randomizer = new System.Random();
    public const int DICE_MINIMUM_VALUE = 1;
    public const int DICE_MAXIMUM_VALUE = 6;
    public const int MAX_OWN_COMMANDMENT_RESULT_BARRIER = 4;
    
    public void Initialize(){
        russian_command_map = new bool[Hexmap.MAP_HEIGHT][];
        german_command_map = new bool[Hexmap.MAP_HEIGHT][];

        for (int i = 0; i < Hexmap.MAP_HEIGHT; i++){
            russian_command_map[i] = new bool[Hexmap.MAP_WIDTH];
            german_command_map[i] = new bool[Hexmap.MAP_WIDTH];

            for (int j = 0; j < Hexmap.MAP_WIDTH; j++){
                russian_command_map[i][j] = false;                
                german_command_map[i][j] = false;
            }
        }
    }

    public void Update_map(List<Unit> generals, Control_map map_control){
        this.map_control = map_control;
        List<Unit> german_generals = new();
        List<Unit> russian_generals = new();
        foreach (Unit general in generals){
            if(general.General_data != null && general.Type == Unit.Unit_type.GENERAL && !general.Is_train_transported){
                if(general.Alliegance == Allegiance.GERMAN) german_generals.Add(general);
                else if (general.Alliegance == Allegiance.RUSSIAN) russian_generals.Add(general);
            }
            else throw new ArgumentException("Error: Provided unit is not a general.");
        }
        Update_army_generals_commands(german_generals,russian_generals);
        Update_corp_generals_commands(german_generals,russian_generals);
    }

    private void Update_army_generals_commands(List<Unit> german_generals, List<Unit> russian_generals){
        foreach(Unit german_general in german_generals) german_general.General_data.Order = Unit.General.General_order.MARCH;
        foreach(Unit russian_general in russian_generals) russian_general.General_data.Order = Unit.General.General_order.MARCH;
        Update_commands(german_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.ARMY), Allegiance.GERMAN);
        Update_commands(russian_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.ARMY), Allegiance.RUSSIAN);
    }

    private void Update_corp_generals_commands(List<Unit> german_generals, List<Unit> russian_generals){
        Update_corp_generals_orders(german_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.CORP), Allegiance.GERMAN);
        Update_corp_generals_orders(russian_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.CORP), Allegiance.RUSSIAN);
    }



    private void Update_corp_generals_orders(List<Unit> corp_generals, Allegiance alliegance){
        Set_commanded_corp_generals_to_march(corp_generals, alliegance);
        Update_commands(corp_generals.FindAll(g => g.General_data.Order == Unit.General.General_order.MARCH),alliegance);
        List<Unit> own_command_successfull_generals = new();
        foreach(Unit general in corp_generals.FindAll(g => g.General_data.Order == Unit.General.General_order.STOP)){
            if(Simulate_corp_general_self_orders(general)) {
                general.General_data.Order = Unit.General.General_order.MARCH;
                own_command_successfull_generals.Add(general);
            }
            else general.General_data.Order = Unit.General.General_order.STOP;
        }
        Update_commands(own_command_successfull_generals,alliegance);
    }
    private void Set_commanded_corp_generals_to_march(List<Unit> corp_generals, Allegiance alliegance){
        bool[][] command_map_temp = Get_proper_command_map(alliegance);
        foreach(Unit general in corp_generals){
            if(command_map_temp[general.Hex.Get_x()][general.Hex.Get_y()]) general.General_data.Order = Unit.General.General_order.MARCH;
            else general.General_data.Order = Unit.General.General_order.STOP;
        }
    }

    private bool Simulate_corp_general_self_orders(Unit corp_generals){
        Debug.Log("Commander tries to command by own");
        int result = 0;
        result += randomizer.Next(DICE_MINIMUM_VALUE,DICE_MAXIMUM_VALUE+1);

        if(corp_generals.General_data.Last_turn_order == Unit.General.General_order.MARCH) result += -1;
        else if(corp_generals.General_data.Last_turn_order == Unit.General.General_order.STOP) result += 1;

        if (corp_generals.General_data.Initiative == Unit.General.General_initiative.ACTIVE) result += -1;
        else if (corp_generals.General_data.Initiative == Unit.General.General_initiative.PASSIVE) result += 1;
        Debug.Log("Command result = " + result);
        return result <= MAX_OWN_COMMANDMENT_RESULT_BARRIER;
    }

    private void Update_commands(List<Unit> generals, Allegiance alliegance){
        HashSet<Hex> commanded_hexes = new();
        foreach (Unit general in generals){
            if(general.Hex != null) {
                List<Hex> temp_list = A_star_movement.Get_hexes_in_range_command_line(general.Hex,general.Get_current_control_range(),alliegance,map_control);
                foreach (Hex temp_hex in temp_list) commanded_hexes.Add(temp_hex);
            }
        }
        bool[][] updated_map = Get_proper_command_map(alliegance);
        Allegiance enemy = Allegiance_helper.Get_enemy_alliegance(alliegance);
        foreach (Hex hex in commanded_hexes) {
            if(map_control.Control_hexes[hex.Get_x()][hex.Get_y()] != Control_map.Get_this_allegiance_control_type(enemy))
                updated_map[hex.Get_x()][hex.Get_y()] = true;
        }
    }


    public bool[][] Get_proper_command_map(Allegiance alliegance){
        return alliegance == Allegiance.GERMAN ? this.German_command_map : this.Russian_command_map;
    }


    public Control_map Map_control { get => map_control; set => map_control = value; }
    public bool[][] German_command_map { get => german_command_map; set => german_command_map = value; }
    public bool[][] Russian_command_map { get => russian_command_map; set => russian_command_map = value; }
}
