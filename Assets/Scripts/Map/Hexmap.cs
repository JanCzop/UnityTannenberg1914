using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap
{
    public const int MAP_LENGTH = 11;
    public const int MAP_WIDTH = 5;
    private GameObject[][] hexes;

    public Hexmap(){
        if(MAP_LENGTH <= 0 || MAP_WIDTH <= 0)
        throw new ArgumentException("Error:Map's dimensions are not valid: x="+MAP_LENGTH+", y="+MAP_WIDTH);

        hexes = new GameObject[MAP_LENGTH][];
        for (int i = 0; i < MAP_LENGTH; i++){
            hexes[i] = new GameObject[MAP_WIDTH];
        }
    }


    public GameObject[][] Hexes { get => hexes; set => hexes = value; }
}
