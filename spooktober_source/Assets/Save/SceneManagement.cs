using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
   public void Load(int id) {
      SceneManager.LoadScene(id);
   }
}
