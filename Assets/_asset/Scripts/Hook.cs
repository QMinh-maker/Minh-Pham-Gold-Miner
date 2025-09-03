using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float normalSpeed = 5f;     // tốc độ kéo bình thường
    private float currentSpeed;        // tốc độ hiện tại (sẽ bị giảm khi kéo vật nặng)

    private bool isPulling = false;
    private Transform player;          // nhân vật đào vàng
    private Transform hookedItem;      // vật phẩm đang kéo

    private int totalGold = 0;         // tổng số tiền người chơi có

    void Start()
    {
        player = GameObject.Find("Miner").transform; // Nhân vật phải đặt tên "Miner"
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (isPulling && hookedItem != null)
        {
            // Kéo vật phẩm về phía Miner
            hookedItem.position = Vector3.MoveTowards(hookedItem.position, player.position, currentSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);

            // Nếu vật phẩm đã về tới Miner
            if (Vector3.Distance(hookedItem.position, player.position) < 0.5f)
            {
                // Cộng tiền
                Item item = hookedItem.GetComponent<Item>();
                totalGold += item.value;
                Debug.Log("Tiền hiện tại: " + totalGold);

                // Hủy vật phẩm
                Destroy(hookedItem.gameObject);

                // Reset lại móc
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
            hookedItem.SetParent(transform);

            // Lấy weight của vật phẩm để giảm tốc độ kéo
            Item item = hookedItem.GetComponent<Item>();
            currentSpeed = normalSpeed / item.weight;

            isPulling = true;
        }
    }
}

