using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GeneratorHexmap map_gen = new();
        Hexmap map = map_gen.Generate_hexmap();
        

        GameObject hex_to_put_soldier = map.Hexes[1][1];
        Vector3 hex_position = hex_to_put_soldier.transform.position;
        GameObject hex_to_travel = map.Hexes[2][4];
        Vector3 hex_travel_position = hex_to_travel.transform.position;

        Vector3 coords_1_1 = new Vector3(hex_position.x,0.5f,hex_position.z);
        GameObject soldier = Unit_generator.Generate_unit(
            coords_1_1,("TestU","TestC","TestA"),(2,1),(2,1),(3,2),
            Unit.UNIT_BASE_HP,Unit.Unit_alliegance.GERMAN,Unit.Unit_condition.NORMAL,Unit.Unit_type.INFANTRY
        );
        Unit soldier_component = soldier.GetComponent<Unit>();
        soldier_component.Move(hex_position,hex_travel_position,0.1f);


        //float cost = Movement.Calculate_movement_cost(soldier,Hex.Get_hex_component(map.Hexes[1][1]),Hex.Get_hex_component(map.Hexes[2][0]));
        //Debug.Log(cost);

        List<Hex> path = A_star_movement.Get_reachable_hexes(map.Hexes[0][3].GetComponent<Hex>(),2f,Unit.Unit_type.INFANTRY);
        foreach (Hex hex in path){
            Debug.Log(hex.Coordinates_x_y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
