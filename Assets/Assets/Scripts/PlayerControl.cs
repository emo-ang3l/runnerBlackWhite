using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private CameraJumpShake cameraShake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gameOver;
    
    public Text live;
    public int liveScore = 3;
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraJumpShake>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraShake != null)
                cameraShake.TriggerJumpShake();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 300f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            liveScore--;
            live.text = liveScore.ToString();
            if (liveScore <= 0)
            {
                gameOver.SetActive(true);
                Time.timeScale = 0f; // Останавливаем игру
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Перезагружаем текущую сцену
    }
}
