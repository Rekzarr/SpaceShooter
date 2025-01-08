using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private int score;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Disparo disparoPrefab;
    private UIController uiController;
    private float timer;
    public float Timer { get => timer; set => timer = value; }
    public UIController UiController { get => uiController; set => uiController = value; }
    private ObjectPool<Enemy> myPool;
    public ObjectPool<Enemy> MyPool { get => myPool; set => myPool = value; }
    private ObjectPool<Disparo> poolDisparos;

    private void Awake(){
       poolDisparos = new ObjectPool<Disparo>(CrearDisparo, GetDisparo, ReleaseDisparo, DestroyDisparo); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        StartCoroutine(SpawnearDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidadEnemigo * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer >= 5){
            myPool.Release(this);
        }
    }

    IEnumerator SpawnearDisparos(){
        while(true){
            Disparo disparoCopia = poolDisparos.Get();
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro){
        if (elOtro.gameObject.CompareTag("DisparoPlayer") || elOtro.gameObject.CompareTag("Player")){
            uiController.IncreaseScore(score);
            myPool.Release(this);
        }
    }
    private Disparo CrearDisparo(){
        Disparo disparoCopia = Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
        disparoCopia.MyPool = poolDisparos;
        return disparoCopia;
    }

    private void GetDisparo(Disparo disparo){
        disparo.transform.position = transform.position;
        disparo.gameObject.SetActive(true);
        disparo.Timer = 0;
    }

    private void ReleaseDisparo(Disparo disparo){
        disparo.gameObject.SetActive(false);
    }

    private void DestroyDisparo(Disparo disparo){
        Destroy(disparo.gameObject);
    }
}