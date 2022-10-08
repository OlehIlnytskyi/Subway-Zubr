using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
