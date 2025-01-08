using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy enemigoPrefab;
    [SerializeField] private UIController uiController;
    [SerializeField] private Disparo disparoPrefab;
    private ObjectPool<Enemy> pool;

    private void Awake(){
       pool = new ObjectPool<Enemy>(CrearEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy); 
    }
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
        for(int i = 0; i < 2; i++){ //Niveles

            for(int j = 0; j < 3; j++){ //Oleadas

                uiController.SetTextoOleadas("Nivel " + (i+1) + " - Oleada " + (j+1));
                yield return new WaitForSeconds(3f);
                uiController.SetTextoOleadas("");

                for(int k = 0; k < 10; k++){ //Enemigos
                    Enemy enemigoCopia = pool.Get();
                    enemigoCopia.gameObject.SetActive(true);

                    if (enemigoCopia != null){
                        enemigoCopia.UiController = uiController;
                    }                    
                yield return new WaitForSeconds(1.5f); 
                }
            }
        }   
        uiController.SetTextoOleadas("Game Clear!");
    }

    private Enemy CrearEnemy(){

        Enemy enemigoCopia = Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
        enemigoCopia.MyPool = pool;
        return enemigoCopia;
    }

   private void GetEnemy(Enemy enemigo){
        enemigo.transform.position = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
        enemigo.gameObject.SetActive(true);
        enemigo.Timer = 0; 
    }

    private void ReleaseEnemy(Enemy enemigo){
        enemigo.gameObject.SetActive(false);
    }

    private void DestroyEnemy(Enemy enemigo){
        Destroy(enemigo.gameObject);
    }
}
