using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";   // ← имя твоей игровой сцены

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
        // или по индексу: SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    
}