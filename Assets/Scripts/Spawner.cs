using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private UIController uiController;
    [SerializeField] private Disparo disparoPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < 5; i++){ //Niveles

            for(int j = 0; j < 3; j++){ //Oleadas

                uiController.SetTextoOleadas("Nivel " + (i+1) + " - Oleada " + (j+1));
                yield return new WaitForSeconds(2f);
                uiController.SetTextoOleadas("");

                for(int k = 0; k < 10; k++){ //Enemigos
                    Vector3 random = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    enemigoPrefab = Instantiate(enemigoPrefab, random, Quaternion.identity);

                if (enemigoPrefab != null){
                    enemigoPrefab.GetComponent<Enemy>().uiController = uiController;
                }                    
                yield return new WaitForSeconds(1.5f); 
                }
            }
        }   
    }
}
