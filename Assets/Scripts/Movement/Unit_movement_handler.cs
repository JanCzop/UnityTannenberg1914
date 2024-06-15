using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_movement_handler : MonoBehaviour
{
    public void MoveUnit(GameObject unit, Vector3 start, Vector3 end, float velocity){
        StartCoroutine(Simple_unit_move(unit, start, end, velocity));
    }
   private IEnumerator Simple_unit_move(GameObject unit, Vector3 start, Vector3 end, float velocity){
        float step = velocity * Time.deltaTime;
        float journey = 0f;

        while (journey <= 1f)
        {
            journey += step / Vector3.Distance(start, end);
            unit.transform.position = Vector3.Lerp(start, end, journey);
            yield return null;
        }

        unit.transform.position = end;
   }



}
