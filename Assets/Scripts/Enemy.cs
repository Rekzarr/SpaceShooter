using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private int score;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject disparoPrefab;
    public UIController uiController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnearDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidadEnemigo * Time.deltaTime);
    }

    IEnumerator SpawnearDisparos(){
        while(true){
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro){
        if (elOtro.gameObject.CompareTag("DisparoPlayer")){
            uiController.IncreaseScore(score);
            Destroy(this.gameObject);
        }
    }
}
