using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control_map : MonoBehaviour
{
    private Control_type[][] control_hexes;

    

    public void Initialize(){
        control_hexes = new Control_type[Hexmap.MAP_HEIGHT][];
        for (int i = 0; i < Hexmap.MAP_HEIGHT; i++){
            control_hexes[i] = new Control_type[Hexmap.MAP_WIDTH];
            for (int j = 0; j < Hexmap.MAP_WIDTH; j++){
                control_hexes[i][j] = Control_type.NONE;
            }
        }
    }

    public void Update_map(List<Unit> units){
        foreach (Unit unit in units){
            if(unit.Type != Unit.Unit_type.SUPPLY_WAGON && unit.Type != Unit.Unit_type.ARTILLERY && unit.Type != Unit.Unit_type.GENERAL 
                && !unit.Is_train_transported){
                (int x, int y) = unit.Hex.Coordinates_x_y;
                control_hexes[x][y] = Assign_control(control_hexes[x][x],unit.Alliegance,true);
                foreach (Hex.Edge_info edge in unit.Hex.Edges){
                    if(Movement.Is_edge_passable(edge.Value) && Movement.Is_hex_enterable(edge.Hex.Terrain)){
                        Hex neighbourHex = edge.Hex;
                        if (neighbourHex.Units.Count == 0){
                            (int nx, int ny) = neighbourHex.Coordinates_x_y;
                            control_hexes[nx][ny] = Assign_control(control_hexes[nx][ny], unit.Alliegance,false);
                        }
                    }
                }
            }
        }
    }

    public static Control_map.Control_type Get_this_allegiance_control_type(Allegiance alliegance){
        return alliegance == Allegiance.GERMAN ? Control_type.GERMAN : Control_type.RUSSIAN;
    }

     public override string ToString(){
        string s = "";
        for (int i = 0; i < control_hexes.Length; i++){
            for (int j = 0; j < control_hexes[i].Length; j++){
                s += $"({i},{j}): {control_hexes[i][j]}\n";
            }
        }
        return s;
    }

    public void Print_controlled_hexes(){
        for (int i = 0; i < control_hexes.Length; i++){
            for (int j = 0; j < control_hexes[i].Length; j++){
                if(control_hexes[i][j] != Control_type.NONE) Debug.Log("("+i+", "+j+") -"+control_hexes[i][j]);
            }
        }
    }


    private Control_type Assign_control(Control_type type_in_hex, Allegiance alliegance, bool force){
        if(force) return alliegance == Allegiance.GERMAN ? Control_type.GERMAN : Control_type.RUSSIAN;
        if (alliegance == Allegiance.GERMAN){
            return type_in_hex switch
            {
                Control_type.GERMAN or Control_type.NONE => Control_type.GERMAN,
                Control_type.RUSSIAN or Control_type.MIXED => Control_type.MIXED,
                _ => throw new ArgumentOutOfRangeException(nameof(type_in_hex), type_in_hex, "Unexpected Control_type value"),
            };
        }
        else if (alliegance == Allegiance.RUSSIAN){
            return type_in_hex switch
            {
                Control_type.RUSSIAN or Control_type.NONE => Control_type.RUSSIAN,
                Control_type.GERMAN or Control_type.MIXED => Control_type.MIXED,
                _ => throw new ArgumentOutOfRangeException(nameof(type_in_hex), type_in_hex, "Unexpected Control_type value"),
            };
        }
        else throw new ArgumentOutOfRangeException(nameof(type_in_hex), type_in_hex, "Unexpected Control_type value");
    }
    




    public enum Control_type{NONE,GERMAN,RUSSIAN,MIXED}

    public Control_type[][] Control_hexes { get => control_hexes; set => control_hexes = value; }
}
