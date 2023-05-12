using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void GoTo(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}