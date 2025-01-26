using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public void OnStartGame() {
        SceneManager.LoadScene(SceneNames.Game.ToString());
    }

    public void OnExitGame() {
        Application.Quit();
    }
}
