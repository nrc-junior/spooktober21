using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
    // area jogavel q o player pode andar
    bool player_full;
    Vector3 game_zone = new Vector3(-5.8f,18.5f, 25); 
    public GameObject[] gostosuras;
    public score points;

    void Start(){
        StartCoroutine(CandyRain());
    }

    
    private IEnumerator CandyRain() {
        
        while (points.isHungry()) {
            int burst = Random.Range(8,30);
            float random_time = Random.Range(4, 10);
            for (int i = 0; i < burst; i++) {
                Instantiate(gostosuras[Random.Range(0,gostosuras.Length)], new Vector3(Random.Range(game_zone.x, game_zone.y), Random.Range(20,25),1), Quaternion.identity);
            }
            yield return new WaitForSeconds(random_time);
	    }
        points.ExplodeDoggo();
    }

}
