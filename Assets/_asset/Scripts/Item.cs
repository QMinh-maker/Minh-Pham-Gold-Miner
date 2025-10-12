using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;
    public float weight;

    [Header("Special Item Settings")]
    public bool isSpecialItem = false; // Bật nếu là item đặc biệt

    // PlayerPrefs keys
    private const string ROCK_KEY = "Rock_book";
    private const string POLISH_KEY = "Diamond_polish";
    private const string STRENGTH_KEY = "Strength_drink";
    private const string CLOVER_KEY = "Luck_clover";
    private const string DYNAMITE_KEY = "DynamiteCount";
    private int dynamiteCount;

    void Start()
    {
        // Nếu là Special Item → random cơ bản trước
        dynamiteCount = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);

        int rewardType = Random.Range(0, 3);
        if (isSpecialItem && rewardType == 0)
        {
            value = Random.Range(1, 801);
            weight = 1f;
        }
        if (isSpecialItem && rewardType == 1)
        {
            PlayerPrefs.SetInt(STRENGTH_KEY, 1);
            Debug.Log("Nhan dc strength");
        }
        else if (isSpecialItem && rewardType == 2)
        {
            dynamiteCount += 1;
            Debug.Log("Nhan dc 1 dynamite. Tong so la "+ dynamiteCount);
        }


            ApplyStoreEffects();
    }

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

        // 🍀 Clover → chỉ ảnh hưởng đến giá trị Special Item (500–800)
        if (hasClover && isSpecialItem)
        {
            value = Random.Range(500, 801);
            
        }  
    }
}
