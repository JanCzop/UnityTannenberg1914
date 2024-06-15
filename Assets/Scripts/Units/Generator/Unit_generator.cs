using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_generator
{

    public const string UNIT_PREFAB_DUMMY = "Prefabs/Soldier_dummy";

    public static GameObject Generate_unit(Vector3 position,(string division, string corp, string army) unit_name, (int normal, int declassed) firepower, (int normal, int declassed) effeciency,
    (int normal, int declassed) movement, int hitpoints, Unit.Unit_alliegance alliegance,
    Unit.Unit_condition condition, Unit.Unit_type type){
        GameObject unit_object = GameObject.Instantiate(Get_unit_Prefab(type,alliegance),position,Quaternion.identity);
        unit_object.name = unit_name.ToString();
        Unit unit_component = unit_object.AddComponent<Unit>();
        Unit_movement_handler unit_movement_handler = unit_object.AddComponent<Unit_movement_handler>();
        Update_unit_component(unit_component,unit_movement_handler,firepower,effeciency,movement,hitpoints,alliegance,condition,type);
        Unit_painter.Paint_unit(unit_object);
        return unit_object;
    }

    private static void Update_unit_component(Unit unit,Unit_movement_handler unit_movement_handler, (int normal, int declassed) firepower, (int normal, int declassed) effeciency,
    (int normal, int declassed) movement, int hitpoints, Unit.Unit_alliegance alliegance,
    Unit.Unit_condition condition, Unit.Unit_type type){
        unit.Movement_handler = unit_movement_handler;
        unit.Firepower = firepower;
        unit.Effeciency = effeciency;
        unit.Movement = movement;
        unit.Hitpoints = hitpoints;
        unit.Alliegance = alliegance;
        unit.Condition = condition;
        unit.Type = type;
    }

    public static GameObject Get_unit_Prefab(Unit.Unit_type type, Unit.Unit_alliegance alliegance){
        return Resources.Load<GameObject>(UNIT_PREFAB_DUMMY);
    }

    public static bool Assign_unit_to_hex(Unit unit, Hex hex){
        if(unit == null || hex == null) return false;
        else{
            unit.Hex = hex;
            hex.Unit = unit;
            return true;
        }
    }
}
