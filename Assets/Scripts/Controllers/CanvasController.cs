using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MudarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
