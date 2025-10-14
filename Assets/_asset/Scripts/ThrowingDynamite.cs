using UnityEngine;
using TMPro;

public class ThrowingDynamite : MonoBehaviour
{
    public static ThrowingDynamite Instance; // 👈 dùng Singleton để gọi từ script khác

    public GameObject DynamitePrefab;
    public Transform hookTarget;
    public Vector3 DynamiteOffset;
    public Hook hookScript;
    [SerializeField] private TextMeshProUGUI dynamiteCountText;

    private int dynamiteCount;
    private const string DYNAMITE_KEY = "DynamiteCount";

    void Awake()
    {
        // Đảm bảo chỉ có 1 instance
        Instance = this;
    }

    void Start()
    {
        // Lấy dữ liệu dynamite đã lưu
        dynamiteCount = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);
        UpdateUI();
    }

    // 👇 Hàm này rất quan trọng — để Item có thể cộng thêm dynamite
    public void AddDynamite(int amount)
    {
        dynamiteCount += amount;
        if (dynamiteCount < 0) dynamiteCount = 0;

        PlayerPrefs.SetInt(DYNAMITE_KEY, dynamiteCount);
        PlayerPrefs.Save();

        UpdateUI();
    }

    public void Throwing()
    {
        // Không có hook hoặc hook chưa bắt item → không ném
        if (hookScript == null || !hookScript.IsPullingItem())
        {
            Debug.Log("Hook chưa bắt item — không thể ném dynamite!");
            return;
        }

        if (dynamiteCount <= 0)
        {
            Debug.Log("Hết dynamite, không thể ném!");
            return;
        }

        // Giảm số lượng và cập nhật UI
        AddDynamite(-1);

        // Tạo vật dynamite bay
        GameObject dynamite = Instantiate(DynamitePrefab, transform.position + DynamiteOffset, Quaternion.identity);
        var moveScript = dynamite.AddComponent<DynamiteMoveAlongRope>();
        moveScript.Setup(transform, hookTarget);

        Debug.Log("Ném dynamite!");
    }

    private void UpdateUI()
    {
        
        if (dynamiteCountText != null)
        {
            dynamiteCountText.text = "x" + dynamiteCount;
        }
    }
}
