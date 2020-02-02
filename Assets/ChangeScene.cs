using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public string targetSceneName;

    public void LoadScene() { SceneManager.LoadScene(targetSceneName); }
}
