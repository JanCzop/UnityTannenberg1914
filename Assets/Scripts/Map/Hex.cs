using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex : MonoBehaviour
{
    //public const int NUMBER_OF_EDGES = 6;

    private List<Edge_info> edges;
    private (int x,int y) coordinates_x_y;
    private Hex_allegiance allegiance;
    private Hex_terrain terrain;

    public enum Hex_allegiance{GERMAN,RUSSIAN}
    public enum Hex_terrain{FLAT,HILL,FOREST,SWAMP,LAKE,SEA,SMALL_URBAN,CITY}

    public String Get_neighbours_to_string(){
        string result = "Hex " + coordinates_x_y + "\n";
    foreach (var edge in Edges){
        int key = edge.Key;
        Hex neighbourHex = edge.Value.Neighbour_hex;
        if (neighbourHex != null){
            (int x, int y) coordinates = neighbourHex.Coordinates_x_y;
            result += $"Edge {key}, Neighbour Coordinates: x={coordinates.x}, y={coordinates.y}\n";
        }
        else result += $"Edge {key}, No Neighbour";
    } 
    return result;
}

public class Edge_info{
    private int key;
    private Edge value;
    public Edge_info(int key, Edge value){
        this.Key = key;
        this.Value = value;
    }
        public int Key { get => key; set => key = value; }
        public Edge Value { get => value; set => this.value = value; }
    }

    public static Hex Get_hex_component(GameObject hex){
        return hex.GetComponent<Hex>();
    }


    public (int, int) Coordinates_x_y { get => coordinates_x_y; set => coordinates_x_y = value; }
    public Hex_allegiance Allegiance { get => allegiance; set => allegiance = value; }
    public List<Edge_info> Edges { get => edges; set => edges = value; }
    public Hex_terrain Terrain { get => terrain; set => terrain = value; }
}

