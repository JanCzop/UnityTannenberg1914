using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control_map : MonoBehaviour
{
    private Control_type[][] control_hexes;

    

    public void Initialize(){
        control_hexes = new Control_type[Hexmap.MAP_LENGTH][];
        for (int i = 0; i < Hexmap.MAP_LENGTH; i++){
            control_hexes[i] = new Control_type[Hexmap.MAP_HEIGHT];
            for (int j = 0; j < Hexmap.MAP_HEIGHT; j++){
                control_hexes[i][j] = Control_type.NONE;
            }
        }
    }

    public void Update_map(Hexmap map, List<Unit> units){
        foreach (Unit unit in units){
            (int x, int y) = unit.Hex.Coordinates_x_y;
            control_hexes[x][y] = Assign_control(control_hexes[x][x],unit.Alliegance,true);
            foreach (Hex.Edge_info edge in unit.Hex.Edges){
                Hex neighbourHex = edge.Value.Neighbour_hex;
                if (neighbourHex.Unit == null){
                                    if(x==1&&y==1) Debug.Log("Im here");

                    (int nx, int ny) = neighbourHex.Coordinates_x_y;
                    control_hexes[nx][ny] = Assign_control(control_hexes[nx][ny], unit.Alliegance,false);
                }
                else Debug.Log("Unit detected at "+neighbourHex.Coordinates_x_y);
            }
        }
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


    private Control_type Assign_control(Control_type type_in_hex, Unit.Unit_alliegance alliegance, bool force){
        if(force) return alliegance == Unit.Unit_alliegance.GERMAN ? Control_type.GERMAN : Control_type.RUSSIAN;
        if (alliegance == Unit.Unit_alliegance.GERMAN){
            return type_in_hex switch
            {
                Control_type.GERMAN or Control_type.NONE => Control_type.GERMAN,
                Control_type.RUSSIAN or Control_type.MIXED => Control_type.MIXED,
                _ => throw new ArgumentOutOfRangeException(nameof(type_in_hex), type_in_hex, "Unexpected Control_type value"),
            };
        }
        else if (alliegance == Unit.Unit_alliegance.RUSSIAN){
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
