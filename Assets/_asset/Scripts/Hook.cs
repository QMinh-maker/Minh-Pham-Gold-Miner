using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hook : MonoBehaviour
{
    public float normalSpeed ;     // tốc độ kéo bình thường
    private float currentSpeed;
    private bool isPulling = false;
    public float itemOffsetY = 0.3f;

    private Transform player;          // Nhân vật Miner
    private Transform hookedItem;      // Item đang dính

    private int totalGold = 0;         // Tiền người chơi
    public RopeRenderer rope;          // script vẽ dây
    public Transform hookHead;         // đầu móc (gắn collider)

    [SerializeField] private TextMeshProUGUI scoreText; // tham chiếu đến UI

    void Start()
    {
        player = GameObject.Find("Miner").transform;
        currentSpeed = normalSpeed;
        UpdateScoreUI();

    }

    void Update()
    {
        // Vẽ dây từ Miner tới Hook
        
        
        if (isPulling)
        {
            // Thu hook về Miner
            hookHead.position = Vector2.MoveTowards(
                hookHead.position,
                player.position,
                currentSpeed * Time.deltaTime
            );
            rope.RenderLine(hookHead.position, false);


            // Nếu có item dính thì nó đi theo hookHead
            if (hookedItem != null)
            {
                hookedItem.position = hookHead.position - hookHead.up 
                    * itemOffsetY;
                
            }

            // Khi hookHead chạm Miner
            if (Vector2.Distance(hookHead.position, player.position) <= 76 
                && Vector2.Distance(hookHead.position, player.position) >= 31)
            {
                if (hookedItem != null)
                {
                    Item item = hookedItem.GetComponent<Item>();
                    totalGold += item.value;
                    Debug.Log("Tiền hiện tại: " + totalGold);
                    
                    Destroy(hookedItem.gameObject);
                    UpdateScoreUI(); // cập nhật điểm sau khi kéo xong
                }

                // Reset hook
                hookedItem = null;
                isPulling = false;
                currentSpeed = normalSpeed;
                

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPulling && collision.CompareTag("Item"))
        {
            rope.RenderLine(hookHead.position, true);
            hookedItem = collision.transform;

            // Tắt collider để nó không va đẩy lung tung
            collision.enabled = false;

            Item item = hookedItem.GetComponent<Item>();
            currentSpeed = normalSpeed / item.weight;
            

            isPulling = true;
        }
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "$ " + totalGold.ToString();
        }
    }

}