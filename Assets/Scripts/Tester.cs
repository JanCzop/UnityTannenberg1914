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
        
        GameObject unit_obj = new();
        Unit soldier = unit_obj.AddComponent<Unit>();
        soldier.Type = Unit.Unit_type.INFANTRY;


        //float cost = Movement.Calculate_movement_cost(soldier,Hex.Get_hex_component(map.Hexes[1][1]),Hex.Get_hex_component(map.Hexes[2][0]));
        //Debug.Log(cost);

        List<Hex> path = A_star_movement.Get_reachable_hexes(Hex.Get_hex_component(map.Hexes[0][3]),2f,soldier);
        foreach (Hex hex in path){
            Debug.Log(hex.Coordinates_x_y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
