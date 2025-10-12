using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtItemSaver : MonoBehaviour
{
    private const string STRENGHT_KEY = "Strength_drink";
    private const string ROCK_KEY = "Rock_book";
    private const string CLOVER_KEY = "Luck_clover";
    private const string POLISH_KEY = "Diamond_polish";
    public void BuyStrength()
    {
        int Strength = 1;
        PlayerPrefs.SetInt(STRENGHT_KEY, Strength);
        PlayerPrefs.Save();
        Debug.Log("Người chơi đã mua Strength");        
    }

    public void BuyRock()
    {
        int Rock = 1;
        PlayerPrefs.SetInt(ROCK_KEY, Rock);
        PlayerPrefs.Save();
        Debug.Log("Người chơi đã mua Rock");
    }

    public void BuyClover()
    {
        int Clover = 1;
        PlayerPrefs.SetInt(CLOVER_KEY, Clover);
        PlayerPrefs.Save();
        Debug.Log("Người chơi đã mua Clover");
    }

    public void BuyPolish()
    {
        int Polish = 1;
        PlayerPrefs.SetInt(POLISH_KEY, Polish);
        PlayerPrefs.Save();
        Debug.Log("Người chơi đã mua Polish");
    }
}
