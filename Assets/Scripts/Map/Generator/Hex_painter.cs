using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Hex_painter : MonoBehaviour
{
    public const string TEST_MATERIAL_PATH = "Materials/Material_testing";
    public const string EDGE_MATERIAL_PATH = "Materials/Material_black";
    public const string DEFAULT_MATERIAL_PATH = "Materials/Material_default";

    public const string SUPPLY_MATERIAL_PATH = "Materials/Supply/Material_supply";


    public const string FOREST_MATERIAL_PATH = "Materials/Material_green";
    public const string CITY_MATERIAL_PATH = "Materials/Material_yellow";
    public const string SWAMP_MATERIAL_PATH = "Materials/Material_swamplike";
    public const string HILL_MATERIAL_PATH = "Materials/Material_grey";
    public const string SEA_MATERIAL_PATH = "Materials/Material_blue";

    
    public const string GERMAN_MATERIAL_PATH = "Materials/Control/Material_GERMAN";
    public const string RUSSIAN_MATERIAL_PATH = "Materials/Control/Material_RUSSIAN";
    public const string MIXED_MATERIAL_PATH = "Materials/Control/Material_MIXED";

    

  
    private Renderer Get_renderer_reference(Hex hex, string part){
        Transform edges_transform = hex.transform.Find(part);
        return edges_transform.GetComponent<Renderer>();
    }

    

    public void Paint_hexes_edges(Hexmap map){
        for(int i = 0; i < map.Hexes.Length; i++) {
            for(int j = 0; j < map.Hexes[i].Length; j++){
                Renderer edges_renderer = Get_renderer_reference(map.Hexes[i][j].GetComponent<Hex>(),"Edges");
                edges_renderer.material = Resources.Load<Material>(EDGE_MATERIAL_PATH);
            }
        }
    }

    public void Reset_hexes_color(Hexmap map){
        for(int i = 0; i < map.Hexes.Length; i++) {
            for(int j = 0; j < map.Hexes[i].Length; j++){
            Hex hex = map.Hexes[i][j].GetComponent<Hex>();
            Renderer edges_renderer = Get_renderer_reference(hex,"Center");
            edges_renderer.material = Resources.Load<Material>(DEFAULT_MATERIAL_PATH);
            }
        }
    }

    public void Paint_map_terrain(Hexmap map){
        Reset_hexes_color(map);
        for(int i = 0; i < map.Hexes.Length; i++) {
            for(int j = 0; j < map.Hexes[i].Length; j++){
            Hex hex = map.Hexes[i][j].GetComponent<Hex>();
            Renderer edges_renderer = Get_renderer_reference(hex,"Center");
            Hex.Hex_terrain terrain = hex.Terrain;
            string path = terrain switch{
            Hex.Hex_terrain.HILL => HILL_MATERIAL_PATH,
            Hex.Hex_terrain.FOREST => FOREST_MATERIAL_PATH,
            Hex.Hex_terrain.SWAMP => SWAMP_MATERIAL_PATH,
            Hex.Hex_terrain.CITY => CITY_MATERIAL_PATH,
            Hex.Hex_terrain.SEA => SEA_MATERIAL_PATH,
            _ => ""
            };
            if(path != "") edges_renderer.material = Resources.Load<Material>(path);
            } 
        }
    }

    public void Paint_control(Hexmap map, Control_map control_map){
        Reset_hexes_color(map);
        for(int i = 0; i < map.Hexes.Length; i++) {
            for(int j = 0; j < map.Hexes[i].Length; j++){
                Hex hex = map.Hexes[i][j].GetComponent<Hex>();
                Renderer edges_renderer = Get_renderer_reference(hex, "Center");
                Control_map.Control_type type = control_map.Control_hexes[i][j];
                string path = type switch{
                    Control_map.Control_type.GERMAN => GERMAN_MATERIAL_PATH,
                    Control_map.Control_type.RUSSIAN => RUSSIAN_MATERIAL_PATH,
                    Control_map.Control_type.MIXED => MIXED_MATERIAL_PATH,
                    _ => ""
                };
                if(path != "") edges_renderer.material = Resources.Load<Material>(path);
            }
        }
    }

    public void Paint_supply_lines(Hexmap map, bool[][] supply_map){
        Reset_hexes_color(map);
        for(int i = 0; i < map.Hexes.Length; i++) {
            for(int j = 0; j < map.Hexes[i].Length; j++){
                Hex hex = map.Hexes[i][j].GetComponent<Hex>();
                Renderer edges_renderer = Get_renderer_reference(hex, "Center");
                if(supply_map[i][j]) edges_renderer.material = Resources.Load<Material>(SUPPLY_MATERIAL_PATH);
            }
        }
    }

    public void Paint_testing(Hexmap map, List<Hex> hexes){
        Reset_hexes_color(map);
        foreach(Hex list_hex in hexes) {
            (int x, int y) = list_hex.Coordinates_x_y;
            Hex hex = map.Hexes[x][y].GetComponent<Hex>();
            Renderer edges_renderer = Get_renderer_reference(hex,"Center");
            edges_renderer.material = Resources.Load<Material>(TEST_MATERIAL_PATH);
        }
    }

}
