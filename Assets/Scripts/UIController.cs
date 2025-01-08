using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoOleadas;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI vidas;

    private int scorePoints = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidas.text = "Vidas: 3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextoOleadas(string texto){
        textoOleadas.text = texto;
    }

    public void IncreaseScore(int i){
        scorePoints += i;
        score.text = "Score: " + scorePoints;
    }

    public void Vidas(float i){
        vidas.text = "Vidas: " + i;
    }

    public void GameOver(){
        vidas.text = "Game Over";
    }
}
