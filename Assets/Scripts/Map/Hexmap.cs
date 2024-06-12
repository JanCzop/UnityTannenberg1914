using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap
{
    public const int MAP_LENGTH = 11;
    public const int MAP_HEIGHT = 5;
    private GameObject[][] hexes;

    public Hexmap(){
        if(MAP_LENGTH <= 0 || MAP_HEIGHT <= 0)
        throw new ArgumentException("Error:Map's dimensions are not valid: x="+MAP_LENGTH+", y="+MAP_HEIGHT);

        hexes = new GameObject[MAP_LENGTH][];
        for (int i = 0; i < MAP_LENGTH; i++){
            hexes[i] = new GameObject[MAP_HEIGHT];
        }
    }

    public void Print_hexes_coords(){
        for (int i = 0; i < hexes.Length; i++){
            for (int j = 0; j < hexes[i].Length; j++){
                GameObject hexGO = hexes[i][j];
                if (hexGO != null){
                    Hex hexComponent = hexGO.GetComponent<Hex>();
                    if (hexComponent != null){
                        (int x, int y) = hexComponent.Coordinates_x_y;
                        Debug.Log($"x={x}, y={y}");
                    }
                    else Debug.LogWarning($"Hex component not found on hex at position [{i},{j}].");   
                }
                else Debug.LogWarning($"GameObject at position [{i},{j}] is null.");
            }
        }
    }

    public void Print_hexes_neighbours(){
        for (int i = 0; i < hexes.Length; i++){
            for (int j = 0; j < hexes[i].Length; j++){
                GameObject hexGO = hexes[i][j];
                if (hexGO != null){
                    Hex hexComponent = hexGO.GetComponent<Hex>();
                    if (hexComponent != null){
                        Debug.Log(hexComponent.Get_neighbours_to_string());
                    }
                    else Debug.LogWarning($"Hex component not found on hex at position [{i},{j}].");   
                }
                else Debug.LogWarning($"GameObject at position [{i},{j}] is null.");
            }
        }
    }



    public GameObject[][] Hexes { get => hexes; set => hexes = value; }
}
