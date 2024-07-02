using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_painter
{
        public const string GERMAN_MATERIAL_PATH = "Materials/Material_grey";
        public const string RUSSIAN_MATERIAL_PATH = "Materials/Material_green";

        public static void Paint_unit(GameObject unit){
            Renderer renderer = unit.transform.GetComponent<Renderer>();
            Allegiance alliegance = unit.GetComponent<Unit>().Alliegance;
            string path = alliegance switch{
                Allegiance.GERMAN => GERMAN_MATERIAL_PATH,
                Allegiance.RUSSIAN => RUSSIAN_MATERIAL_PATH,
                _ => ""
            };
            if(path == "") return;
            renderer.material = Resources.Load<Material>(path);
        }

}
