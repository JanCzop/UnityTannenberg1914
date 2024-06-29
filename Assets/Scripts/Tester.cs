using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    int COUNTER = 0;

    public Hexmap map;
    public List<Unit> units = new();

    //////// MOVE TO BUTTON
    public Control_map control_map;
    public Hex_painter hex_painter;
    public Supply_map supply_map;
    /////////////////////////////
    void Start()
    {   
        hex_painter = GetComponent<Hex_painter>();
        control_map = GetComponent<Control_map>();
        supply_map = GetComponent<Supply_map>();
        control_map.Initialize();
        supply_map.Initialize();



        GeneratorHexmap map_gen = new();
        Hexmap map = map_gen.Generate_hexmap();
        hex_painter.Paint_hexes_edges(map);
        hex_painter.Paint_map_terrain(map);
        this.map = map;



        Create_test_units();

        //soldier_component.Move(hex_position,hex_travel_position,0.1f);


        //float cost = Movement.Calculate_movement_cost(soldier,Hex.Get_hex_component(map.Hexes[1][1]),Hex.Get_hex_component(map.Hexes[2][0]));
        //Debug.Log(cost);

        //List<Hex> path = A_star_movement.Get_reachable_hexes(map.Hexes[0][3].GetComponent<Hex>(),2f,Unit.Unit_type.INFANTRY);
        //foreach (Hex hex in path){
        //    Debug.Log(hex.Coordinates_x_y);
        //}


        control_map.Update_map(units);
        //hex_painter.Paint_control(map,control_map);

        map.Hexes[3][1].GetComponent<Hex>().Is_supply_hub = true;

        supply_map.Update_map(map,units);
        hex_painter.Paint_supply_lines(map,supply_map.Supply.german);



    }



    private void Create_test_units(){
        ////////////// UNIT 1 - GERMAN
        GameObject hex_to_put_soldier = map.Hexes[1][1];
        Vector3 hex_position = hex_to_put_soldier.transform.position;
        Vector3 coords = new(hex_position.x,0.5f,hex_position.z);
        GameObject soldier = Unit_generator.Generate_unit(
            coords,("GermanUnit","TestC","TestA"),(2,1),(2,1),(3,2),
            Unit.UNIT_BASE_HP,Unit.Unit_alliegance.GERMAN,Unit.Unit_condition.NORMAL,Unit.Unit_type.INFANTRY
        );
        Unit soldier_component = soldier.GetComponent<Unit>();
        soldier_component.Hex = hex_to_put_soldier.GetComponent<Hex>();
        hex_to_put_soldier.GetComponent<Hex>().Units.Add(soldier_component);
        units.Add(soldier_component);
        /////////////////////////////

        ////////////// UNIT 2 - RUSSIAN
        hex_to_put_soldier = map.Hexes[3][1];
        hex_position = hex_to_put_soldier.transform.position;
        coords = new(hex_position.x,0.5f,hex_position.z);
        GameObject soldier_2 = Unit_generator.Generate_unit(
            coords,("RussianUnit","TestC","TestA"),(2,1),(2,1),(3,2),
            Unit.UNIT_BASE_HP,Unit.Unit_alliegance.RUSSIAN,Unit.Unit_condition.NORMAL,Unit.Unit_type.INFANTRY
        );
        Unit soldier_2_component = soldier_2.GetComponent<Unit>();
        soldier_2_component.Hex = hex_to_put_soldier.GetComponent<Hex>();
        hex_to_put_soldier.GetComponent<Hex>().Units.Add(soldier_2_component);
        units.Add(soldier_2_component);
        /////////////////////////////
        ///

        ///////////////// UNIT 3 - GERMAN ARMY GENERAL
        /////////////////
        hex_to_put_soldier = map.Hexes[3][0];
        hex_position = hex_to_put_soldier.transform.position;
        coords = new(hex_position.x,0.5f,hex_position.z);
        GameObject soldier_3 = Unit_generator.Generate_unit(
            coords,("GermanArmyGeneral","TestC","TestA"),(0,0),(0,0),(0,0),
            Unit.UNIT_BASE_HP,Unit.Unit_alliegance.GERMAN,Unit.Unit_condition.NORMAL,Unit.Unit_type.GENERAL
        );
        Unit soldier_3_component = soldier_3.GetComponent<Unit>();
        soldier_3_component.General_data = new(Unit.General.General_initiative.ACTIVE, (3,1),
        Unit.General.General_rank.ARMY, Unit.General.General_order.MARCH);
        soldier_3_component.Hex = hex_to_put_soldier.GetComponent<Hex>();
        hex_to_put_soldier.GetComponent<Hex>().Units.Add(soldier_3_component);
        units.Add(soldier_3_component);

        StartListening();


    }

    /// przeniesc do TurnHandler:
    
    void StartListening()
    {
        Hex.On_hex_clicked += HandleObjectClicked;
    }

    void StopListening()
    {
        Hex.On_hex_clicked -= HandleObjectClicked;
    }

    void HandleObjectClicked(GameObject hex_object){
        Debug.Log(hex_object.GetComponent<Hex>().Coordinates_x_y);
        COUNTER++;
        if(COUNTER>100) StopListening();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private string List_viewer(List<Hex> hexes){
        string str = "";
        foreach(Hex hex in hexes) str += hex.ToString() + "\n";
        
        return str;
    }
    private string Edges_viewer(List<Hex.Edge_info> edges){
        string str = "";
        foreach (Hex.Edge_info edge in edges) str += "Connected with hex " + edge.Hex.ToString() + "\n";
        return str;
    }

}
