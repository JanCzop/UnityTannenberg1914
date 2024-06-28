using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_map : MonoBehaviour
{
    Control_map map_control;
    private bool[][] russian_command_map;
    private bool[][] german_command_map;
    
    public void Initialize(Control_map map_control){
        this.map_control = map_control;
        russian_command_map = new bool[Hexmap.MAP_LENGTH][];
        german_command_map = new bool[Hexmap.MAP_LENGTH][];

        for (int i = 0; i < Hexmap.MAP_LENGTH; i++){
            russian_command_map[i] = new bool[Hexmap.MAP_HEIGHT];
            german_command_map[i] = new bool[Hexmap.MAP_HEIGHT];

            for (int j = 0; j < Hexmap.MAP_HEIGHT; j++){
                russian_command_map[i][j] = false;                
                german_command_map[i][j] = false;
            }
        }
    }

    public void Update(List<Unit> units){
        List<Unit> german_units = new();
        List<Unit> russian_units = new();
        foreach (Unit unit in units){
            if(unit.Alliegance == Unit.Unit_alliegance.GERMAN) german_units.Add(unit);
            else if (unit.Alliegance == Unit.Unit_alliegance.RUSSIAN) russian_units.Add(unit);
        }
        
    }


    public Control_map Map_control { get => map_control; set => map_control = value; }
    public bool[][] German_command_map { get => german_command_map; set => german_command_map = value; }
    public bool[][] Russian_command_map { get => russian_command_map; set => russian_command_map = value; }
}
