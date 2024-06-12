using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Hex_painter : MonoBehaviour
{
    public const string EDGE_MATERIAL_PATH = "Materials/Material_black";

    public const string FOREST_MATERIAL_PATH = "Materials/Material_green";
    public const string CITY_MATERIAL_PATH = "Materials/Material_yellow";
    public const string SWAMP_MATERIAL_PATH = "Materials/Material_swamplike";
    public const string HILL_MATERIAL_PATH = "Materials/Material_grey";
    

    public static void Paint_hexes_edges(GameObject hex){
        Transform edges_transform = hex.transform.Find("Edges");
        Renderer edges_renderer = edges_transform.GetComponent<Renderer>();
        edges_renderer.material = Resources.Load<Material>(EDGE_MATERIAL_PATH);
    }

    public static void Paint_hex_via_terrain(GameObject hex){
        Transform edges_transform = hex.transform.Find("Center");
        Renderer edges_renderer = edges_transform.GetComponent<Renderer>();
        Hex.Hex_terrain terrain = hex.GetComponent<Hex>().Terrain;
        string path = terrain switch{
            Hex.Hex_terrain.HILL => HILL_MATERIAL_PATH,
            Hex.Hex_terrain.FOREST => FOREST_MATERIAL_PATH,
            Hex.Hex_terrain.SWAMP => SWAMP_MATERIAL_PATH,
            Hex.Hex_terrain.CITY => CITY_MATERIAL_PATH,
            _ => EDGE_MATERIAL_PATH,
        };
        if(path == EDGE_MATERIAL_PATH) return;
        edges_renderer.material = Resources.Load<Material>(path);
    }

}
