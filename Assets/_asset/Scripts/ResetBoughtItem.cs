using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoughtItem : MonoBehaviour
{
    private const string ROCK_KEY = "Rock_book";
    private const string POLISH_KEY = "Diamond_polish";
    private const string STRENGTH_KEY = "Strength_drink";
    private const string CLOVER_KEY = "Luck_clover";
    //private const string DYNAMITE_KEY = "DynamiteCount";


    private void Awake()
    {
        ResetAllBoughtItems();
    }
    // Gọi hàm này khi cần reset toàn bộ item về mặc định
    public void ResetAllBoughtItems()
    {
        PlayerPrefs.SetInt(ROCK_KEY, 0);
        PlayerPrefs.SetInt(POLISH_KEY, 0);
        PlayerPrefs.SetInt(STRENGTH_KEY, 0);
        PlayerPrefs.SetInt(CLOVER_KEY, 0);
        //PlayerPrefs.SetInt(DYNAMITE_KEY, 0);
        PlayerPrefs.Save();

        Debug.Log("Đã reset toàn bộ item store về 0!");
    }

}
