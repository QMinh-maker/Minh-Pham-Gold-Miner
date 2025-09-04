using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Hook : MonoBehaviour
{
    public float normalSpeed = 5f;     // tốc độ kéo bình thường
    private float currentSpeed;
    private bool isPulling = false;

    private Transform player;          // Nhân vật Miner
    private Transform hookedItem;      // Item đang dính

    private int totalGold = 0;         // Tiền người chơi
    public RopeRenderer rope;          // script vẽ dây
    public Transform hookHead;         // đầu móc (gắn collider)

    void Start()
    {
        player = GameObject.Find("Miner").transform;
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // Vẽ dây từ Miner tới Hook
        rope.RenderLine(hookHead.position, true);

        if (isPulling)
        {
            // Thu hook về Miner
            hookHead.position = Vector3.MoveTowards(
                hookHead.position,
                player.position,
                currentSpeed * Time.deltaTime
            );

            // Nếu có item dính thì nó đi theo hookHead
            if (hookedItem != null)
            {
                hookedItem.position = hookHead.position;
            }

            // Khi hookHead chạm Miner
            if (Vector3.Distance(hookHead.position, player.position) < 0.5f)
            {
                if (hookedItem != null)
                {
                    Item item = hookedItem.GetComponent<Item>();
                    totalGold += item.value;
                    Debug.Log("Tiền hiện tại: " + totalGold);

                    Destroy(hookedItem.gameObject);
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
            hookedItem = collision.transform;

            // Tắt collider để nó không va đẩy lung tung
            collision.enabled = false;

            Item item = hookedItem.GetComponent<Item>();
            currentSpeed = normalSpeed / item.weight;

            isPulling = true;
        }
    }
}