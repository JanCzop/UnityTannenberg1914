using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    private Edge_separator separator;
    private Edge_hex_connector connector;

    public Edge(){
    }


    public enum Edge_separator{NONE,LAKE,SMALL_RIVER,HUGE_RIVER,VISTULA}
    public enum Edge_hex_connector{NONE,ROAD,RAILWAY,DESTROYED_ROAD,DESTROYED_RAILWAY}

    public Edge_separator Separator { get => separator; set => separator = value; }
    public Edge_hex_connector Connector { get => connector; set => connector = value; }
}
