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

    public Hexmap Generate_hexmap(){
        Hexmap map = Create_hexes_map();
        Assign_Edges(map);
        Initialize_hexes_default_properties(map);
        Setup_test_terrains(map);
        //map.Print_hexes_neighbours();
        //map.Print_hexes_coords();
        return map;
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
                GameObject hex_object = GameObject.Instantiate(Get_hex_prefab(),position,rotation);
                hex_object.name = $"Hex_{i}_{j}";
                Hex hex_component = hex_object.AddComponent<Hex>();
                hex_component.Coordinates_x_y = (i,j);
                map.Hexes[i][j] = hex_object;
                position.x += WIDTH_DISTANCE;
            }
        }

        return map;
    }
private void Assign_Edges(Hexmap map)
{
    for (int i = 0; i < map.Hexes.Length; i++)
    {
        for (int j = 0; j < map.Hexes[i].Length; j++)
        {
            GameObject hex_object = map.Hexes[i][j];
            if (hex_object != null)
            {
                Hex hex_component = hex_object.GetComponent<Hex>();
                hex_component.Edges = new List<Hex.Edge_info>();
                
                if (hex_component != null)
                {
                    if (i != Hexmap.MAP_HEIGHT - 1 && i != Hexmap.MAP_HEIGHT - 2)
                        hex_component.Edges.Add(new Hex.Edge_info(0, map.Hexes[i + 2][j].GetComponent<Hex>()));
                    
                    if (i != Hexmap.MAP_HEIGHT - 1 && (j != Hexmap.MAP_WIDTH - 1 || i % 2 == 1))
                    {
                        if (i % 2 == 0)
                            hex_component.Edges.Add(new Hex.Edge_info(1, map.Hexes[i + 1][j + 1].GetComponent<Hex>()));
                        else
                            hex_component.Edges.Add(new Hex.Edge_info(1, map.Hexes[i + 1][j].GetComponent<Hex>()));
                    }
                    
                    if (i != 0 && (j != Hexmap.MAP_WIDTH - 1 || i % 2 == 1))
                    {
                        if (i % 2 == 0)
                            hex_component.Edges.Add(new Hex.Edge_info(2, map.Hexes[i - 1][j + 1].GetComponent<Hex>()));
                        else
                            hex_component.Edges.Add(new Hex.Edge_info(2, map.Hexes[i - 1][j].GetComponent<Hex>()));
                    }
                    
                    if (i != 0 && i != 1)
                        hex_component.Edges.Add(new Hex.Edge_info(3, map.Hexes[i - 2][j].GetComponent<Hex>()));
                    
                    if (i != 0 && (j != 0 || i % 2 != 1))
                    {
                        if (i % 2 == 0)
                            hex_component.Edges.Add(new Hex.Edge_info(4, map.Hexes[i - 1][j].GetComponent<Hex>()));
                        else
                            hex_component.Edges.Add(new Hex.Edge_info(4, map.Hexes[i - 1][j - 1].GetComponent<Hex>()));
                    }
                    
                    if (i != Hexmap.MAP_HEIGHT - 1 && (j != 0 || i % 2 != 1))
                    {
                        if (i % 2 == 0)
                            hex_component.Edges.Add(new Hex.Edge_info(5, map.Hexes[i + 1][j].GetComponent<Hex>()));
                        else
                            hex_component.Edges.Add(new Hex.Edge_info(5, map.Hexes[i + 1][j - 1].GetComponent<Hex>()));
                    }
                }
                else
                {
                    Debug.LogWarning($"Hex component not found on hex at position [{i},{j}].");
                }
            }
            else
            {
                Debug.LogWarning($"GameObject at position [{i},{j}] is null.");
            }
        }
    }
}



private void Initialize_hexes_default_properties(Hexmap map){
    for(int i = 0; i < map.Hexes.Length; i++) {
        for(int j = 0; j < map.Hexes[i].Length; j++){
            Hex hex = map.Hexes[i][j].GetComponent<Hex>();
            hex.Allegiance = Hex.Hex_allegiance.GERMAN;
            hex.Terrain = Hex.Hex_terrain.FLAT;
            hex.Victory_points = 0;
            hex.Units = new();
            Initialize_edges_default_properties(hex);
        } 
    }
}

private void Initialize_edges_default_properties(Hex hex){
    foreach(Hex.Edge_info edge in hex.Edges){
            edge.Value = new Edge{
                Separator = Edge.Edge_separator.NONE,
                Connector = Edge.Edge_hex_connector.NONE
            };
        }
}

private void Setup_test_terrains(Hexmap map){
    map.Hexes[0][2].GetComponent<Hex>().Terrain = Hex.Hex_terrain.FOREST;
    map.Hexes[1][2].GetComponent<Hex>().Terrain = Hex.Hex_terrain.CITY;
    map.Hexes[1][4].GetComponent<Hex>().Terrain = Hex.Hex_terrain.SWAMP;
    map.Hexes[1][3].GetComponent<Hex>().Terrain = Hex.Hex_terrain.HILL;
    map.Hexes[2][0].GetComponent<Hex>().Terrain = Hex.Hex_terrain.FOREST;
    map.Hexes[4][3].GetComponent<Hex>().Terrain = Hex.Hex_terrain.SMALL_URBAN;
    map.Hexes[5][0].GetComponent<Hex>().Terrain = Hex.Hex_terrain.SEA;
    map.Hexes[4][0].GetComponent<Hex>().Terrain = Hex.Hex_terrain.SEA;


}

    


    public static GameObject Get_hex_prefab(){
        return Resources.Load<GameObject>(hex_prefab_path);
    }
}


//if(i!=0 || i!=1) {Debug.Log(map.Hexes[i-2][j].GetComponent<Hex>().Coordinates_x_y); ;hex_component.Edges.Add(
 //                           new Tuple<int, Edge>(0,new Edge(map.Hexes[i-2][j].GetComponent<Hex>())));}