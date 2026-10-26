using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5f;
    public int health = 10;

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool inSpikeArea = false;
    private float spikeTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Debug.Log("Health ban đầu: " + health);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (inSpikeArea)
        {
            spikeTimer += Time.deltaTime;

            if (spikeTimer >= 3f)
            {
                spikeTimer = 0f;

                if (health > 0)
                {
                    health--;
                    Debug.Log("Health hiện tại: " + health);
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + movement.normalized * speed * Time.fixedDeltaTime
        );
    }

    // Phát hiện kim cương và khu vực vật nhọn bằng Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            health++;
            Debug.Log("Health hiện tại: " + health);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Spike"))
        {
            inSpikeArea = true;
            spikeTimer = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            inSpikeArea = false;
            spikeTimer = 0f;
        }
    }

    // Hỗ trợ nhặt kim cương nếu sử dụng va chạm Collision thông thường
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Diamond"))
        {
            health++;
            Debug.Log("Health hiện tại: " + health);

            Destroy(collision.gameObject);
        }
    }
}
