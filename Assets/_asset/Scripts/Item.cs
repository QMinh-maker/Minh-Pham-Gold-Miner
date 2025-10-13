using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;
    public float weight;

    private const string ROCK_KEY = "Rock_book";
    private const string POLISH_KEY = "Diamond_polish";
    private const string STRENGTH_KEY = "Strength_drink";
    private const string CLOVER_KEY = "Luck_clover";
    private const string DYNAMITE_KEY = "DynamiteCount";

    void Start()
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
                    hook.StartCoroutine(AddMoneyAfterDelay(hook, value, 2f));
                }
                Debug.Log($"💰 TreasureBag thưởng tiền: {value}");
                break;

            // 💪 Strength
            case 1:
                PlayerPrefs.SetInt(STRENGTH_KEY, 1);
                PlayerPrefs.Save();
                if (hook != null)
                    hook.ShowSpecialReward("💪 Strength!");
                Debug.Log("💪 Nhận được Strength!");
                break;

            // 💣 Dynamite
            case 2:
                AddDynamite(1);
                if (hook != null)
                    hook.ShowSpecialReward("+1 💣 Dynamite!");
                Debug.Log("💣 Nhận được Dynamite!");
                
                break;
        }
    }

    private IEnumerator AddMoneyAfterDelay(Hook hook, int amount, float delay)
    {
        yield return new WaitForSeconds(delay);
        hook.AddGold(amount);
    }

    private void AddDynamite(int amount)
    {
        int current = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);
        current += amount;
        PlayerPrefs.SetInt(DYNAMITE_KEY, current);
        PlayerPrefs.Save();

        if (ThrowingDynamite.Instance != null)
            ThrowingDynamite.Instance.AddDynamite(0); // cập nhật lại UI
    }
}
