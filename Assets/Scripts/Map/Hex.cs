using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public const string hex_prefab_path = "Prefabs/Hexagon";
    public const int NUMBER_OF_EDGES = 6;

    private Edge[] edges;
    private (int,int) coordinates_x_y;

    public Edge[] Edges { get => edges; set => edges = value; }
    public (int, int) Coordinates_x_y { get => coordinates_x_y; set => coordinates_x_y = value; }
    public static GameObject Get_hex_prefab(){
        return Resources.Load<GameObject>(hex_prefab_path);
    }
}
