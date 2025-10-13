using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;
    public float weight;

    // PlayerPrefs keys
    private const string ROCK_KEY = "Rock_book";
    private const string POLISH_KEY = "Diamond_polish";
    private const string STRENGTH_KEY = "Strength_drink";
    private const string CLOVER_KEY = "Luck_clover";
    private const string DYNAMITE_KEY = "DynamiteCount";

    private void Start()
    {
        ApplyStoreEffects(); // Chỉ hiệu ứng buff, KHÔNG thưởng trực tiếp
    }

    // --------------------------------------
    // Áp dụng hiệu ứng từ cửa hàng (rock book, polish, v.v.)
    // --------------------------------------
    private void ApplyStoreEffects()
    {
        bool hasRockBook = PlayerPrefs.GetInt(ROCK_KEY, 0) == 1;
        bool hasPolish = PlayerPrefs.GetInt(POLISH_KEY, 0) == 1;
        bool hasStrength = PlayerPrefs.GetInt(STRENGTH_KEY, 0) == 1;
        bool hasClover = PlayerPrefs.GetInt(CLOVER_KEY, 0) == 1;

        string lowerName = name.ToLower();

        // 🪨 Rock Book → nhân đôi giá trị đá
        if (hasRockBook && (lowerName.Contains("bigstone") || lowerName.Contains("smallstone")))
        {
            value *= 2;
        }

        // 💎 Polish → +300 giá trị kim cương
        if (hasPolish && lowerName.Contains("diamond"))
        {
            value += 300;
        }

        // 💪 Strength Drink → giảm cân nặng toàn bộ item
        if (hasStrength)
        {
            weight = 1f;
        }
    }

    // --------------------------------------
    // Gọi khi item được kéo thành công
    // --------------------------------------
    public void GiveTreasureReward()
    {
        string lowerName = name.ToLower();
        int rewardType = Random.Range(0, 3); // 0: tiền, 1: strength, 2: dynamite
        int CloverReward = Random.Range(0, 3);
        bool hasClover = PlayerPrefs.GetInt(CLOVER_KEY, 0) == 1;

        // Chỉ áp dụng cho treasurebag
        if (!lowerName.Contains("treasurebag"))
            return;

        // 🍀 Nếu có Clover thì có cơ hội nhận phần thưởng cao hơn
        if (hasClover)
        {
            if (CloverReward == 0)
            {
                value = Random.Range(500, 801);
            }
            else if (CloverReward == 1)
            {
                PlayerPrefs.SetInt(STRENGTH_KEY, 1);
                PlayerPrefs.Save();
                Debug.Log("🍀 Clover thưởng Strength!");
                return;
            }
            else if (CloverReward == 2)
            {
                AddDynamite(1);
                Debug.Log("🍀 Clover thưởng Dynamite!");
                return;
            }
        }

        // 🎁 Bình thường (không có clover)
        if (rewardType == 0)
        {
            value = Random.Range(1, 801);
            weight = 1f;
        }
        else if (rewardType == 1)
        {
            PlayerPrefs.SetInt(STRENGTH_KEY, 1);
            PlayerPrefs.Save();
            Debug.Log("Nhận được Strength Drink!");
        }
        else if (rewardType == 2)
        {
            AddDynamite(1);
            Debug.Log("Nhận được Dynamite!");
        }
    }

    // --------------------------------------
    // Thêm dynamite + cập nhật UI
    // --------------------------------------
    private void AddDynamite(int amount)
    {
        int current = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);
        current += amount;
        PlayerPrefs.SetInt(DYNAMITE_KEY, current);
        PlayerPrefs.Save();

        // Gọi cập nhật UI nếu có ThrowingDynamite
        if (ThrowingDynamite.Instance != null)
        {
            ThrowingDynamite.Instance.AddDynamite(0); // chỉ refresh UI
        }
    }
}
