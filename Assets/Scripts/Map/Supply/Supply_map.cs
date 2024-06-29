using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply_map : MonoBehaviour
{
    private (bool[][] russian, bool[][] german) supply;

    public void Initialize(){
        supply.russian = new bool[Hexmap.MAP_HEIGHT][];
        supply.german = new bool[Hexmap.MAP_HEIGHT][];

        for (int i = 0; i < Hexmap.MAP_HEIGHT; i++){
            supply.russian[i] = new bool[Hexmap.MAP_WIDTH];
            supply.german[i] = new bool[Hexmap.MAP_WIDTH];

            for (int j = 0; j < Hexmap.MAP_WIDTH; j++){
                supply.russian[i][j] = false;                
                supply.german[i][j] = false;

            }
        }
    }

    public void Update_map(Hexmap map, List<Unit> units){
        for (int i = 0; i < Hexmap.MAP_HEIGHT; i++){
            for (int j = 0; j < Hexmap.MAP_WIDTH; j++){
                Hex hex = map.Hexes[i][j].GetComponent<Hex>();
                    if(hex.Is_supply_hub && hex.Allegiance == Hex.Hex_allegiance.GERMAN ||
                    hex.Terrain == Hex.Hex_terrain.SMALL_URBAN) supply.german[i][j] = true;

            }
        }
    }

    

    public (bool[][] russian, bool[][] german) Supply { get => supply; set => supply = value; }
}
