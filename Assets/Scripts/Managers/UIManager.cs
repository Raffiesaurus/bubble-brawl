using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene(SceneNames.Game.ToString(), LoadSceneMode.Single);
    }
}
