using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    [SerializeField] private float anchoImagen;
    private Vector3 posicionInicial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Se calcula el espacio a recorrer por múltiplos de anchoImagen.
        float resto = (velocidad * Time.time) % anchoImagen;

        //La posición cambiará según el resto, por lo que al ser 0 volverá al inicio.
        transform.position = posicionInicial + resto * direccion;
    }
}
