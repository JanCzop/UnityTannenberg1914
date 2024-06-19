using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    private Hex neighbour_hex;
    private Edge_separator separator;
    private Edge_hex_connector connector;

    public Edge(Hex neighbour_hex){
        this.neighbour_hex = neighbour_hex;
    }

    public static int Opposite_edge_index(int index){
        if(index < 0 || index > 5) throw new ArgumentOutOfRangeException("Error: Index has to be in range from 0 to 5. This index is: " + index);
        int multiplier = index >= 3 ? -1 : 1;
        return index + (multiplier*3);
    }

    public enum Edge_separator{NONE,LAKE,SMALL_RIVER,HUGE_RIVER,VISTULA}
    public enum Edge_hex_connector{NONE,ROAD,RAILWAY,DESTROYED_ROAD,DESTROYED_RAILWAY}

    public Hex Neighbour_hex { get => neighbour_hex; set => neighbour_hex = value; }
    public Edge_separator Separator { get => separator; set => separator = value; }
    public Edge_hex_connector Connector { get => connector; set => connector = value; }
}
