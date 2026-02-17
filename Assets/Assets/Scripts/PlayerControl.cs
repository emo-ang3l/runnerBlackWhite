using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private CameraJumpShake cameraShake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gameOver;
    private AudioSource audioSource;
    public AudioClip damageSound;



    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    public Text live;
    public int liveScore = 3;

    private Rigidbody2D rb;

    
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraJumpShake>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        
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

            if (audioSource != null && damageSound != null)
                audioSource.PlayOneShot(damageSound);
                StartCoroutine(FlashRedDamage());
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);
            
            if (liveScore <= 0)
            {
                gameOver.SetActive(true);// Останавливаем игру
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Перезагружаем текущую сцену
    }
    IEnumerator FlashRedDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
