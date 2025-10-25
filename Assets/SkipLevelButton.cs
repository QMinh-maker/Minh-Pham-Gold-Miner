using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipLevelButton : MonoBehaviour
{
    public Button skipButton;             // Nút skip
    public Image buttonImage;             // Hình ảnh của nút (để đổi màu hoặc sprite)
    public Sprite lockedSprite;           // Sprite khi bị khóa (xám)
    public Sprite unlockedSprite;         // Sprite khi mở (xanh)


    public string nextSceneName;
    private int n;                          //Màn chơi hiện tại
    private int requiredScore;              // Điểm yêu cầu để mở
    private int playerScore;              // Điểm hiện tại của người chơi

    void Start()
    {
        // Lấy điểm từ PlayerPrefs (nếu bạn đã lưu trước đó)
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        // Lấy n từ PlayerPrefs, mặc định 2 nếu chưa có
        n = PlayerPrefs.GetInt("LevelIndex", 1);


        // Tính điểm yêu cầu theo công thức
        requiredScore = 135 * n * n + 140 * n + 375;
        UpdateButtonState();
    }

    // Gọi khi nhấn nút Skip Level
    public void OnSkipLevel()
    {
        if (playerScore >= requiredScore)
        {   
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void UpdateButtonState()
    {
        bool canSkip = playerScore >= requiredScore;

        skipButton.interactable = canSkip;
        buttonImage.sprite = canSkip ? unlockedSprite : lockedSprite;
    }
}
