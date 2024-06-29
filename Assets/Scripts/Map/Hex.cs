using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex : MonoBehaviour
{
    //public const int NUMBER_OF_EDGES = 6;

    public static event Action<GameObject> On_hex_clicked;

    private List<Edge_info> edges;
    private (int x,int y) coordinates_x_y;
    private bool is_supply_hub;
    private Hex_allegiance allegiance;
    private Hex_terrain terrain;
    private int victory_points;


    ///////////////////// TODO
    private List<Unit> units;
    ////////////////////

    public enum Hex_allegiance{GERMAN,RUSSIAN}
    public enum Hex_terrain{FLAT,HILL,FOREST,SWAMP,LAKE,SEA,SMALL_URBAN,CITY}

    public String Get_neighbours_to_string(){
        string result = "Hex " + coordinates_x_y + "\n";
    foreach (var edge in Edges){
        int key = edge.Key;
        Hex neighbourHex = edge.Hex;
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
    private Edge edge;

    private Hex hex;
    public Edge_info(int key, Hex hex){
        this.Key = key;
        this.Value = edge;
        this.Hex = hex;
    }
        public int Key { get => key; set => key = value; }
        public Edge Value { get => edge; set => this.edge = value; }
        public Hex Hex { get => hex; set => hex = value; }
    }

    public bool Set_mutual_edge_properties(int index, Edge.Edge_hex_connector connector, Edge.Edge_separator separator){
        Edge_info edge = edges.Find(e => e.Key == index);
        if(edge == null) return false;
        else {
            edge.Value.Connector = connector;
            edge.Value.Separator = separator;
            return true;
        }
    }

    public void OnMouseDown(){
        On_hex_clicked?.Invoke(gameObject);
    }


    public (int, int) Coordinates_x_y { get => coordinates_x_y; set => coordinates_x_y = value; }
    public Hex_allegiance Allegiance { get => allegiance; set => allegiance = value; }
    public List<Edge_info> Edges { get => edges; set => edges = value; }
    public Hex_terrain Terrain { get => terrain; set => terrain = value; }
    public List<Unit> Units { get => units; set => units = value; }
    public bool Is_supply_hub { get => is_supply_hub; set => is_supply_hub = value; }
    public int Victory_points { get => victory_points; set => victory_points = value; }
}

