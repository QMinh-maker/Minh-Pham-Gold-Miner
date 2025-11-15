using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipLevelButton : MonoBehaviour
{
    [Header("UI References")]
    public GameObject skipLockedImage;   // Ảnh xám (bị khóa)
    public Button skipButtonGreen;       // Nút xanh thật để skip level

    
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
        Debug.Log(requiredScore);
        // Cập nhật trạng thái ban đầu
        UpdateSkipButtonState();

        // Gán sự kiện khi bấm nút xanh
        skipButtonGreen.onClick.AddListener(OnSkipLevel);
    }

    // Gọi khi nhấn nút Skip Level
    public void OnSkipLevel()
    {
        if (playerScore >= requiredScore)
        {   
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void UpdateSkipButtonState()
    {
        bool canSkip = playerScore >= requiredScore;

        // Nếu chưa đủ điểm
        skipLockedImage.SetActive(!canSkip);  // Hiện ảnh xám
        skipButtonGreen.gameObject.SetActive(canSkip);  // Ẩn/hiện nút xanh
    }
}
