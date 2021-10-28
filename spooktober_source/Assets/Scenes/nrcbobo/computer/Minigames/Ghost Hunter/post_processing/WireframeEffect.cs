using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeEffect : MonoBehaviour
{
    void OnPreRender() {
        GL.wireframe = true;
    }
    void OnPostRender() {
        GL.wireframe = false;
        
    }
}
