using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
public class Disparo : MonoBehaviour
{
    [SerializeField] private float velocidadDisparo;
    [SerializeField] private Vector3 direccion;
    private float timer;
    private ObjectPool<Disparo> myPool;

    public ObjectPool<Disparo> MyPool { get => myPool; set => myPool = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidadDisparo * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer >= 5){
            myPool.Release(this);
            timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D elOtro){
        if (elOtro.gameObject.CompareTag("Enemy")){
            myPool.Release(this);
        }
    }
}
