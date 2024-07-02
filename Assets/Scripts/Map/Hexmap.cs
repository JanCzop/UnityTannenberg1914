using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap
{
    public const int MAP_HEIGHT = 11;
    public const int MAP_WIDTH = 5; // ITS DOUBLED ON MAP
    private Hex[][] hexes;

    public Hexmap(){
        if(MAP_HEIGHT <= 0 || MAP_WIDTH <= 0)
        throw new ArgumentException("Error: Map's dimensions are not valid: x=" + MAP_HEIGHT +", y=" + MAP_WIDTH);

        Hexes = new Hex[MAP_HEIGHT][];
        for (int i = 0; i < MAP_HEIGHT; i++){
            Hexes[i] = new Hex[MAP_WIDTH];
        }
    }

    public void Print_hexes_coords(){
        for (int i = 0; i < Hexes.Length; i++){
            for (int j = 0; j < Hexes[i].Length; j++){
                Hex hex = Hexes[i][j];
                    (int x, int y) = hex.Coordinates_x_y;
                    Debug.Log($"x={x}, y={y}");
            }
        }
    }

    public void Print_hexes_neighbours(){
        for (int i = 0; i < Hexes.Length; i++){
            for (int j = 0; j < Hexes[i].Length; j++){
                Hex hex = Hexes[i][j];
                        Debug.Log(hex.Get_neighbours_to_string());
            }
        }
    }

        public Hex[][] Hexes { get => hexes; set => hexes = value; }




}
