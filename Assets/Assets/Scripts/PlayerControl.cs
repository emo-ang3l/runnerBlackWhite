using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private CameraJumpShake cameraShake;
    public GameObject gameOver;
    private AudioSource audioSource;
    public AudioClip damageSound;

    [Header("Ground Check")]
    public Transform groundCheck;              // пустой GameObject чуть ниже ног
    public LayerMask groundMask;               // должен включать только слой "Ground"
    [SerializeField] private float groundRadius = 0.2f;  // обычно 0.1–0.3 хватает

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Health Bar")]
    public Image healthBarImage;
    public Sprite healthFull;
    public Sprite healthMedium;
    public Sprite healthLow;

    [Header("Score")]
    public Text scoreText;
    public float pointsPerSecond = 10f;

    public int liveScore = 3;
    private float score = 0f;
    private float gameTime = 0f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraJumpShake>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();

        UpdateHealthBar();

        if (scoreText != null)
            scoreText.text = "0";
    }

    void Update()
    {
        // Проверка земли
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        if (gameOver.activeInHierarchy)
            return;

        // Счёт по времени
        gameTime += Time.deltaTime;
        score = gameTime * pointsPerSecond;
        int displayScore = Mathf.FloorToInt(score);

        if (scoreText != null)
            scoreText.text = displayScore.ToString();

        // Прыжок только если на земле
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0f, 300f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            liveScore--;
            UpdateHealthBar();

            if (audioSource != null && damageSound != null)
                audioSource.PlayOneShot(damageSound);

            StartCoroutine(FlashRedDamage());

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);

            if (liveScore <= 0)
            {
                gameOver.SetActive(true);
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage == null) return;

        switch (liveScore)
        {
            case 3:
                healthBarImage.sprite = healthFull;
                break;
            case 2:
                healthBarImage.sprite = healthMedium;
                break;
            case 1:
                healthBarImage.sprite = healthLow;
                break;
            default:
                healthBarImage.enabled = false;
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    // Для отладки в сцене (визуализация круга проверки земли)
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}