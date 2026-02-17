using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Animator animator;           // ← должен быть привязан в инспекторе!

    private int moveHash;
    private int jumpHash;   // лучше тоже вынести в хэш

    void Awake()
    {
        // Инициализируем хэши один раз (лучше, чем в Update)
        moveHash = Animator.StringToHash("Move");
        jumpHash = Animator.StringToHash("Jump");
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");   // Raw — резче и лучше для 2D/платформеров
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(0, 0, 0).normalized; // ← normalized — чтобы диагональ не была быстрее

        // Двигаем персонажа
        transform.position += move * moveSpeed * Time.deltaTime;

        // Проверяем, движется ли персонаж
        bool isMoving = move.magnitude == 0f;     // ← было step → исправлено на move

        // Передаём в аниматор
        animator.SetBool(moveHash, isMoving);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(jumpHash);
            jump();
            
        }
    }

    public void jump()
    {
        // Логика прыжка (если нужна)
    }
}