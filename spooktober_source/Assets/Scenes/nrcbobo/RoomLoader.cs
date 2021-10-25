using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour {

    public Transform entering_room;
    public Transform on_room;

    Material[] on_materials;
    Material[] enter_materials;
    
    void Start() {
        on_materials = getMaterials(on_room);
        enter_materials = getMaterials(entering_room);
    }

    static Material[] getMaterials(Transform transform) {
        int sz = transform.transform.childCount;
        Material[] array = new Material[sz];
        
        int mat_space = 0;
        for (int i = 0; i < sz; i++) {
            Material newMat = transform.GetChild(i).GetComponent<MeshRenderer>().material;

            bool repeat = false;
            foreach (Material save_mat  in array){
                repeat =   newMat == save_mat;
                if(repeat) break;
            }
            
            if (repeat) {
                Debug.Log("textura repetida" + newMat.name);
            } else {
                Debug.Log("textura add" + newMat.name);
                array[mat_space++] = newMat;
            }
        }

        Material[] mats = new Material[mat_space];
        for (int i = 0; i < mat_space; i++) {
            mats[i] = array[i];
        }
        
        return mats;
    }

    bool finished = true;
    void OnTriggerEnter() {
        if (finished) {
            finished = false;
            StartCoroutine(leaveDisable());
            StartCoroutine(enterEnable());
        }
    }

    IEnumerator leaveDisable() {
        string[] render_types = new string[on_materials.Length]; 
        int i = 0;
        foreach (Material mat in on_materials) {
            render_types[i++] = mat.GetTag("RenderType",true);
            ChangeRenderMode(mat,BlendMode.Transparent);
        }

        float stop = 1;
        while (stop > 0) {

            foreach (Material mat in on_materials){
                Color x = mat.color;
                if (x.a > 0) {
                    x.a -= 0.1f;
                    mat.color = x;
                }
            } 
            stop -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        i = 0;
        foreach (Material mat in on_materials){
            if (render_types[i++] == "Opaque") {
                ChangeRenderMode(mat, BlendMode.Opaque);
            }
        }

        on_room.gameObject.SetActive(false);
    }
    
    IEnumerator enterEnable() {
        entering_room.gameObject.SetActive(true);

        string[] render_types = new string[enter_materials.Length];
        int i = 0;
        
        foreach (Material mat in enter_materials) {
            render_types[i++] = mat.GetTag("RenderType",true);
            ChangeRenderMode(mat,BlendMode.Transparent);
            Color x = mat.GetColor("_Color");
            x.a = 0;
            mat.color = x;
        }
        
        float stop = 0;
        while (stop<1) {
            foreach (Material mat in enter_materials){
                Color x = mat.GetColor("_Color");
                if (x.a < 1) {
                    x.a += 0.1f;
                    mat.color = x;
                }
            } 
            stop += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        i = 0;
        foreach (Material mat in enter_materials){
            if (render_types[i++] == "Opaque") {
                ChangeRenderMode(mat, BlendMode.Opaque);
            }
        }
        
        Transform save_on_room = on_room;
        Material[] save_on_mats = on_materials;
        
        on_materials = enter_materials;
        on_room = entering_room;
        
        enter_materials = save_on_mats;
        entering_room = save_on_room;
        finished = true;
    }
    
    public enum BlendMode {
         Opaque,
         Cutout,
         Fade,
         Transparent
    }
 
     public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode) {
         switch (blendMode) {
             case BlendMode.Opaque:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                 standardShaderMaterial.SetInt("_ZWrite", 1);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = -1;
                 break;
             case BlendMode.Cutout:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                 standardShaderMaterial.SetInt("_ZWrite", 1);
                 standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 2450;
                 break;
             case BlendMode.Fade:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 standardShaderMaterial.SetInt("_ZWrite", 0);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 3000;
                 break;
             case BlendMode.Transparent:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 standardShaderMaterial.SetInt("_ZWrite", 0);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 3000;
                 break;
         }
 
     }
    
}
