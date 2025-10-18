using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Hook : MonoBehaviour
{
    private bool isPulling = false;
    private bool canCatch = true;

    public float itemOffsetY = 0.3f;

    private Transform player;
    private Transform hookedItem;

    private int totalGold = 0;
    public RopeRenderer rope;
    public Transform hookHead;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI GoldScore;

    private int pendingValue = 0; // 💰 giá trị chờ cộng (sau khi ẩn text)

    public HookMovement hookMovement;
    [SerializeField] private ThrowingDynamite throwingDynamite;

    // 🎵 Âm thanh
    [Header("Sound Effects")]
    public AudioClip valueSound;
    public AudioClip pullSound;
    public AudioClip coinSound;

    private AudioSource audioSource;
    private bool isPullSoundPlaying = false;

    void Start()
    {
        player = GameObject.Find("MinerSkeleton").transform;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.8f;

        totalGold = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateScoreUI();

        if (GoldScore != null)
            GoldScore.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPulling)
        {
            rope.RenderLine(hookHead.position, false);

            if (hookedItem != null)
                hookedItem.position = hookHead.position - hookHead.up * itemOffsetY;

            if (!isPullSoundPlaying)
                PlayPullLoop();

            // Khi gần Miner → kéo xong
            if (Vector2.Distance(hookHead.position, player.position) <= 100 &&
                Vector2.Distance(hookHead.position, player.position) >= 10)
            {
                if (hookedItem != null)
                {
                    Item item = hookedItem.GetComponent<Item>();

                    if (item != null)
                    {
                        // Gọi phần thưởng (nếu là Special Item)
                        item.GiveTreasureReward();

                        pendingValue = item.value;
                        Destroy(hookedItem.gameObject);

                        // Hiển thị giá trị item
                        ShowItemValue(pendingValue);
                    }
                }

                hookedItem = null;
                isPulling = false;
                StopPullLoop();
                EnableCatch();
                PlaySound(valueSound);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHookedItem(collision);
    }

    private void HandleHookedItem(Collider2D collision)
    {
        if (canCatch && !isPulling && collision.CompareTag("Item"))
        {
            rope.RenderLine(hookHead.position, true);
            hookedItem = collision.transform;

            Item item = hookedItem.GetComponent<Item>();
            hookMovement.ApplyWeight(item.weight);

            isPulling = true;
            hookMovement.HandleMoveBackOnHittingItem(collision);
        }
    }

    // 🔊 Phát âm
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    private void PlayPullLoop()
    {
        if (pullSound != null && audioSource != null)
        {
            audioSource.clip = pullSound;
            audioSource.loop = true;
            audioSource.Play();
            isPullSoundPlaying = true;
        }
    }

    private void StopPullLoop()
    {
        if (isPullSoundPlaying && audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
            isPullSoundPlaying = false;
        }
    }

    // ✅ Cập nhật điểm chỉ khi HideItemValue() chạy xong
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = totalGold.ToString();
            PlayerPrefs.SetInt("PlayerScore", totalGold);
            
            PlayerPrefs.Save();
        }
    }

    public void ShowItemValue(int value)
    {
        // Nếu GoldScore null thì thoát ngay, không làm gì cả
        if (GoldScore == null || value == 0)
            return;

        GoldScore.gameObject.SetActive(true);
        GoldScore.text = "$" + value.ToString();

        CancelInvoke(nameof(HideItemValue));
        Invoke(nameof(HideItemValue), 2f); // ⏱ ẩn sau 2 giây
    }



    private void HideItemValue()
    {
        if (GoldScore != null)
            GoldScore.gameObject.SetActive(false);

        if (pendingValue > 0)
        {
            totalGold += pendingValue;     // 💰 chỉ cộng ở đây
            pendingValue = 0;
            PlaySound(coinSound);          // 🔊 âm cộng tiền
            UpdateScoreUI();               // 🪙 cập nhật UI sau khi text ẩn
        }
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        PlaySound(coinSound);
        UpdateScoreUI();
    }

    public void DisableCatch() => canCatch = false;
    public void EnableCatch() => canCatch = true;
    public bool IsPullingItem() => isPulling;
}
