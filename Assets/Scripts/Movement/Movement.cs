using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static float Terrain_entering_cost(Unit unit ,Hex.Hex_terrain terrain){
        if(unit.Type == Unit.Unit_type.GENERAL || unit.Type == Unit.Unit_type.SUPPLY_WAGON) return 1;
            return terrain switch{
                Hex.Hex_terrain.FLAT => 0f,
                Hex.Hex_terrain.HILL => 0f,
                Hex.Hex_terrain.FOREST => 1f,
                Hex.Hex_terrain.SWAMP => 2f,
                Hex.Hex_terrain.SMALL_URBAN => 0f,
                Hex.Hex_terrain.CITY => 0f,
                _ => throw new ArgumentException("Error: Terrain is incorrect.")
            };
    }

    public static bool Is_edge_easily_passable(Edge.Edge_hex_connector connector){
        return (connector == Edge.Edge_hex_connector.ROAD || connector == Edge.Edge_hex_connector.RAILWAY);
    }

    public static float Edge_passing_cost(Edge.Edge_separator separator){
        return separator switch{
            Edge.Edge_separator.SMALL_RIVER => 1f,
            Edge.Edge_separator.HUGE_RIVER => 2f,
            _ => 0f
        };
    }

    public static bool Is_hex_enterable(Hex.Hex_terrain terrain){
        return (terrain != Hex.Hex_terrain.LAKE && terrain != Hex.Hex_terrain.SEA);
    }

    public static bool Is_edge_passable(Edge edge){
        return edge.Separator != Edge.Edge_separator.LAKE && 
           (edge.Separator != Edge.Edge_separator.VISTULA || 
            edge.Connector == Edge.Edge_hex_connector.ROAD || 
            edge.Connector == Edge.Edge_hex_connector.RAILWAY);
    }
    public static (bool,Edge) Are_hexes_connected(Hex hex_src, Hex hex_dest){
        foreach(Hex.Edge_info edge in hex_src.Edges) {
            if(edge.Value.Neighbour_hex == hex_dest) return (true,edge.Value);
        }
        return (false,null);
    }
    public static bool Movement_posibility(Hex hex_src, Hex hex_dest){
        (bool connection_possible, Edge edge) connection_info = Are_hexes_connected(hex_src, hex_dest);
        if(!connection_info.connection_possible) return false;
        if(!Is_hex_enterable(hex_dest.Terrain)) return false;
        if(!Is_edge_passable(connection_info.edge)) return false;
        return true;
    }
    public static float Calculate_movement_cost(Unit unit, Hex hex_src, Hex hex_dest){
        (bool connection_possible, Edge edge) connection_info = Are_hexes_connected(hex_src,hex_dest);
        if(Is_edge_easily_passable(connection_info.edge.Connector)) return unit.Type switch{
                Unit.Unit_type.SUPPLY_WAGON => 0.5f,
                _ => 1f,
            };
        else{
            float cost = 1f;
            cost += Edge_passing_cost(connection_info.edge.Separator);
            cost += Terrain_entering_cost(unit, hex_dest.Terrain);
            return cost;
        }    
    }

}
