using UnityEngine;

public class ThrowingDynamite : MonoBehaviour
{
    
    public GameObject DynamitePrefab;   // Prefab dynamite
    public Transform hookTarget;        // Hook target (gắn HookHead hoặc Hook)
    public Vector3 DynamiteOffset;      // Lệch khi tạo dynamite
    public Hook hookScript;             // Tham chiếu đến Hook

    

    public void Throwing()
    {
        // Nếu chưa gán hook hoặc hook chưa bắt item → không cho ném
        if (hookScript == null || !hookScript.IsPullingItem())
        {
            Debug.Log("Hook chưa bắt item — không thể ném dynamite!");
            return;
        }

       

        // Tạo dynamite
        GameObject dynamite = Instantiate(DynamitePrefab, transform.position + DynamiteOffset, Quaternion.identity);

        // Thêm script điều khiển bay dọc dây hook
        var moveScript = dynamite.AddComponent<DynamiteMoveAlongRope>();
        moveScript.Setup(transform, hookTarget);

        Debug.Log("Ném dynamite!");

        
    }

   
}


