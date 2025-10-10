using UnityEngine;
using TMPro;

public class ThrowingDynamite : MonoBehaviour
{
    
    public GameObject DynamitePrefab;   // Prefab dynamite
    public Transform hookTarget;        // Hook target (gắn HookHead hoặc Hook)
    public Vector3 DynamiteOffset;      // Lệch khi tạo dynamite
    public Hook hookScript;             // Tham chiếu đến Hook
    [SerializeField] private TextMeshProUGUI dynamiteCountText;
    
    private int dynamiteCount;          // Số dynamite hiện có
    private const string DYNAMITE_KEY = "DynamiteCount"; // Key lưu dữ liệu

    void Start()
    {
        // Lấy số dynamite đã lưu (mặc định 0)
        dynamiteCount = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);
        UpdateUI();
    }

    public void Throwing()
    {
        // Nếu chưa gán hook hoặc hook chưa bắt item → không cho ném
        if (hookScript == null || !hookScript.IsPullingItem())
        {
            Debug.Log("Hook chưa bắt item — không thể ném dynamite!");
            return;
        }

         if (dynamiteCount <= 0)
        {
            Debug.Log("❌ Hết dynamite, không thể ném!");
            return;
        }

        dynamiteCount--;
        PlayerPrefs.SetInt(DYNAMITE_KEY, dynamiteCount);
        PlayerPrefs.Save();
        UpdateUI();

        // Tạo dynamite
        GameObject dynamite = Instantiate(DynamitePrefab, transform.position + DynamiteOffset, Quaternion.identity);

        // Thêm script điều khiển bay dọc dây hook
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


