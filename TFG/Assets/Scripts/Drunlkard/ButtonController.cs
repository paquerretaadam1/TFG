using UnityEngine;
using UnityEngine.SceneManagement;



public class ButtonController : MonoBehaviour
{
    public string newGame = "SalasRandom";

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGame);
    }
}