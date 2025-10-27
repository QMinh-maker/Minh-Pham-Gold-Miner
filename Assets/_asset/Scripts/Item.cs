using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;
    public float weight;

    public GameObject strengthNotify;   // Prefab hoặc UI hiện thông báo Strength
    public GameObject dynamiteNotify;   // Prefab hoặc UI hiện thông báo Dynamite

    private const string ROCK_KEY = "Rock_book";
    private const string POLISH_KEY = "Diamond_polish";
    private const string STRENGTH_KEY = "Strength_drink";
    private const string CLOVER_KEY = "Luck_clover";

    public AudioSource HighValueSound;

    

    void Update()
    {
        ApplyStoreEffects();
    }

    private void ApplyStoreEffects()
    {
        bool hasRockBook = PlayerPrefs.GetInt(ROCK_KEY, 0) == 1;
        bool hasPolish = PlayerPrefs.GetInt(POLISH_KEY, 0) == 1;
        bool hasStrength = PlayerPrefs.GetInt(STRENGTH_KEY, 0) == 1;

        string lowerName = name.ToLower();

        // Rock → nhân đôi giá trị đá
        if (hasRockBook && (lowerName.Contains("bigstone") || lowerName.Contains("smallstone")))
            value *= 2;

        // Polish → +300 giá trị kim cương
        if (hasPolish && lowerName.Contains("diamond"))
            value += 300;

        // Strength → giảm cân nặng
        if (hasStrength)
            weight = 1f;
    }

    // 🎁 Khi item là TreasureBag → thưởng ngẫu nhiên
    public void GiveTreasureReward()
    {
        string lowerName = name.ToLower();
        if (!lowerName.Contains("treasurebag")) return; // Chỉ chạy với treasure bag

        bool hasClover = PlayerPrefs.GetInt(CLOVER_KEY, 0) == 1;
        Hook hook = FindObjectOfType<Hook>();

        int rewardType = Random.Range(0, 3); // 0 = tiền, 1 = strength, 2 = dynamite

        switch (rewardType)
        {
            // 🪙 Tiền
            case 0:
                value = hasClover ? Random.Range(500, 801) : Random.Range(1, 801);
                if (hook != null)
                {
                    hook.ShowItemValue(value);
                }
                //Debug.Log($"💰 TreasureBag thưởng tiền: {value}");
                break;

            // 💪 Strength
            case 1:
                PlayerPrefs.SetInt(STRENGTH_KEY, 1);
                PlayerPrefs.Save();
                if (strengthNotify != null)
                {
                    ShowNotification(strengthNotify);
                    HighValueSound.Play();
                    
                }
                Animator minerAnim = FindObjectOfType<MinerAnimationControl>()?.GetComponent<Animator>();
                if (minerAnim != null)
                {
                    minerAnim.SetTrigger("Strong");
                }
                //Debug.Log("💪 Nhận được Strength!");
                break;

            // 💣 Dynamite
            case 2:
                ThrowingDynamite.Instance.AddDynamite(1);
                //Debug.Log("💣 Nhận được Dynamite!");
                if (dynamiteNotify != null)
                {
                    ShowNotification(dynamiteNotify);
                    HighValueSound.Play();
                    FindObjectOfType<TempoPause>()?.PauseForOneSecond();
                }   
                break;
        }
    }


    private void ShowNotification(GameObject notifyObj)
    {
        // Tạo bản sao thông báo tại giữa màn hình (hoặc vị trí hiện tại)
        GameObject clone = Instantiate(notifyObj);
       
        // Hiện thông báo trong 1 giây rồi ẩn đi
        Destroy(clone, 1f);
    }
}
