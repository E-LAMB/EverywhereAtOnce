using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMaterials : MonoBehaviour
{

    public Renderer my_renderer;
    public Material[] my_materials;

    // Update is called once per frame
    void Update()
    {
        my_renderer.material = my_materials[Mind.plane];
    }
}
