using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidadNave; 
    [SerializeField] private float ratio;        
    [SerializeField] private Disparo disparoPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private UIController uiController;
    private float temporizador = 0.5f;
    private float vidas = 3;
    private ObjectPool<Disparo> pool;
    
    private void Awake(){
       pool = new ObjectPool<Disparo>(CrearDisparo, GetDisparo, ReleaseDisparo, DestroyDisparo); 
    }

    // Update is called once per frame
    void Update()
    {
        temporizador += 1 * Time.deltaTime;
        MovimientoNave();
        DelimitarMovimiento();
        if(Input.GetKey(KeyCode.Space) && temporizador > ratio){
            Disparo disparoCopia = pool.Get();
            temporizador = 0;
        }
    }

    void MovimientoNave(){
        //Se usa la lectura de las teclas eje (WASD y flechas) para trasladar la nave.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY).normalized * velocidadNave * Time.deltaTime);
    }

    void DelimitarMovimiento(){
        //Clamp delimita un float entre un máximo y un mínimo.
        float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    private Disparo CrearDisparo(){
        Disparo disparoCopia = Instantiate(disparoPrefab, transform.position, Quaternion.identity);
        disparoCopia.MyPool = pool;
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

    private void OnTriggerEnter2D(Collider2D elOtro){
        if (elOtro.gameObject.CompareTag("DisparoEnemy") || elOtro.gameObject.CompareTag("Enemy")){
            vidas = vidas - 1;
            if(vidas < 0){
                Destroy(this.gameObject);
                uiController.GameOver(); 
                Time.timeScale = 0;
            } else {
                uiController.Vidas(vidas);
            }
        }
    }
}
