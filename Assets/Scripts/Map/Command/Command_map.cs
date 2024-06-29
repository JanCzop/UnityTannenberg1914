using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_map : MonoBehaviour
{
    Control_map map_control;
    private bool[][] russian_command_map;
    private bool[][] german_command_map;
    
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
            if(general.General_data != null && general.Type == Unit.Unit_type.GENERAL){
                if(general.Alliegance == Unit.Unit_alliegance.GERMAN) german_generals.Add(general);
                else if (general.Alliegance == Unit.Unit_alliegance.RUSSIAN) russian_generals.Add(general);
            }
            else throw new ArgumentException("Error: Provided unit is not a general.");
        }
        Update_army_generals_commands(german_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.ARMY), Unit.Unit_alliegance.GERMAN);
        Update_army_generals_commands(russian_generals.FindAll(g => g.General_data.Rank == Unit.General.General_rank.ARMY), Unit.Unit_alliegance.RUSSIAN);

    }

    private void Update_army_generals_commands(List<Unit> army_generals, Unit.Unit_alliegance alliegance){
        HashSet<Hex> commanded_hexes = new();
        foreach (Unit general in army_generals){
            if(general.Hex != null) {
                List<Hex> temp_list = A_star_movement.Get_hexes_in_range_command_line(general.Hex,general.Get_current_control_range(),alliegance,map_control);
                foreach (Hex temp_hex in temp_list) commanded_hexes.Add(temp_hex);
            }
        }
        bool[][] updated_map = alliegance == Unit.Unit_alliegance.GERMAN ? german_command_map : russian_command_map;
        Unit.Unit_alliegance enemy = Unit.Get_enemy_alliegance(alliegance);
        foreach (Hex hex in commanded_hexes) {
            if(map_control.Control_hexes[hex.Coordinates_x_y.Item1][hex.Coordinates_x_y.Item2] != Control_map.Get_this_allegiance_control_type(enemy)) 
                updated_map[hex.Coordinates_x_y.Item1][hex.Coordinates_x_y.Item2] = true;
        }
    }


    public Control_map Map_control { get => map_control; set => map_control = value; }
    public bool[][] German_command_map { get => german_command_map; set => german_command_map = value; }
    public bool[][] Russian_command_map { get => russian_command_map; set => russian_command_map = value; }
}
