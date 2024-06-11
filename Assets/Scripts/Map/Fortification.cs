using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fortification : MonoBehaviour
{
    public const string Fort_prefab_path = "";
    public const string Trench_prefab_path = "";
    
    
    private Edge[] edges;
    private int firepower;
    private Fortification_kind kind;
    private Fortification_status status;


    public enum Fortification_status{UNDAMAGED,DAMAGED,DESTROYED}
    public enum Fortification_kind{TRENCH,FORT}

    public Edge[] Edges { get => edges; set => edges = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public Fortification_kind Kind { get => kind; set => kind = value; }
    public Fortification_status Status { get => status; set => status = value; }




}