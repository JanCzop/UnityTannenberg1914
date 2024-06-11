using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratorHexmap{

    public const float WIDTH_DISTANCE = 3;
    public static readonly float HEX_HEIGHT = (float)Math.Sqrt(3);
    public static readonly float HEX_WIDTH = 2;

    public const string hex_prefab_path = "Prefabs/Hexagon";

    public void Generate_hexmap(){
        Create_hexes_map();
    }

    private Hexmap Create_hexes_map(){
        Hexmap map = new();
        Vector3 position =  Vector3.zero;
        Quaternion rotation = Quaternion.Euler(0,90f,0);

        for(int i = 0; i < map.Hexes.Length; i++) {
            position.z += HEX_HEIGHT/2;
            if(i%2 == 0) position.x = 2.5f;
            else position.x = 1f;
            for(int j = 0; j < map.Hexes[i].Length; j++) {
                map.Hexes[i][j] = GameObject.Instantiate(Get_hex_prefab(),position,rotation);
                position.x += WIDTH_DISTANCE;
            }
        }

        return map;
    }

        public static GameObject Get_hex_prefab(){
        return Resources.Load<GameObject>(hex_prefab_path);
    }
}