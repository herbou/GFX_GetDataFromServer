using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour {

    public void Restart() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
