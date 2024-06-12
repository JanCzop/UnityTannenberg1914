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

    public enum Edge_separator{NONE,LAKE,SMALL_RIVER,HUGE_RIVER,VISTULA}
    public enum Edge_hex_connector{NONE,ROAD,RAILWAY,DESTROYED_ROAD,DESTROYED_RAILWAY}

    public Hex Neighbour_hex { get => neighbour_hex; set => neighbour_hex = value; }
    public Edge_separator Separator { get => separator; set => separator = value; }
    public Edge_hex_connector Connector { get => connector; set => connector = value; }
}
