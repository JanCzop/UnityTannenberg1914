using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GeneratorHexmap map_gen = new GeneratorHexmap();
        map_gen.Generate_hexmap();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
