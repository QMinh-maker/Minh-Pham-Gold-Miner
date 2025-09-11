using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hook : MonoBehaviour
{
     
    
    private bool isPulling = false;
    public float itemOffsetY = 0.3f;

    private Transform player;          // Nhân vật Miner
    private Transform hookedItem;      // Item đang dính

    private int totalGold = 0;         // Tiền người chơi
    public RopeRenderer rope;          // script vẽ dây
    public Transform hookHead;         // đầu móc (gắn collider)

    [SerializeField] private TextMeshProUGUI scoreText; // tham chiếu đến UI tiền
    [SerializeField] private TextMeshProUGUI GoldScore;//UI giá trị item

    private int pendingValue = 0; //Lưu giá trị item vừa kéo đc

    public HookMovement hookMovement;

    void Start()
    {
        player = GameObject.Find("Miner").transform; 
        UpdateScoreUI();
        if (GoldScore != null)
            GoldScore.gameObject.SetActive(false); // ẩn text phụ lúc đầu
    }

    void Update()
    {                  
        if (isPulling)
        {
            rope.RenderLine(hookHead.position, false);

            // Nếu có item dính thì nó đi theo hookHead
            if (hookedItem != null)
            {
                hookedItem.position = hookHead.position - hookHead.up * itemOffsetY;
            }

            // Khi hookHead chạm Miner
            if (Vector2.Distance(hookHead.position, player.position) <= 76 
                && Vector2.Distance(hookHead.position, player.position) >= 31)
            {
                if (hookedItem != null)
                {
                    Item item = hookedItem.GetComponent<Item>();
                    pendingValue = item.value;

                    Debug.Log("Item giá trị: " + pendingValue);

                    // Hiện text giá trị item
                    ShowItemValue(pendingValue);

                    Destroy(hookedItem.gameObject);
                    UpdateScoreUI(); // cập nhật điểm sau khi kéo xong
                }

                // Reset hook
                hookedItem = null;
                isPulling = false;     
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHookedItem(collision);
    }

    private void HandleHookedItem(Collider2D collision)
    {
        if (!isPulling && collision.CompareTag("Item"))
        {
            Debug.Log("Móc đã chạm Item");
            rope.RenderLine(hookHead.position, true);
            hookedItem = collision.transform;

            // Tắt collider để nó không va đẩy lung tung
            collision.enabled = false;

            // Lấy trọng lượng item và giảm tốc độ hookMovement
            Item item = hookedItem.GetComponent<Item>();
            hookMovement.ApplyWeight(item.weight);

            isPulling = true;
            hookMovement.HandleMoveBackOnHittingItem(collision);
        }        
    }    
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "$" + totalGold.ToString();
        }
    }

    private void ShowItemValue(int value)
    {
        if (GoldScore != null)
        {
            GoldScore.gameObject.SetActive(true);
            GoldScore.text = "$" + value.ToString();

            // Ẩn sau 2 giây
            CancelInvoke(nameof(HideItemValue));
            Invoke(nameof(HideItemValue), 2f);
        }
    }

    private void HideItemValue()
    {
        if (GoldScore != null)
        {
            GoldScore.gameObject.SetActive(false);
        }
        if (pendingValue > 0)
        {
            totalGold += pendingValue;
            pendingValue = 0;
            UpdateScoreUI();
        }
    }
}