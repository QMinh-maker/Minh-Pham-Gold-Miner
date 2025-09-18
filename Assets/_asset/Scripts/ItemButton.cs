using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public GameObject[] objects; // danh sách object có sẵn
    private GameObject currentObject;

    public void ShowObject(int index)
    {
        // Ẩn object cũ
        if (currentObject != null)
        {
            currentObject.SetActive(false);
        }

        // Hiện object mới
        if (index >= 0 && index < objects.Length)
        {
            currentObject = objects[index];
            currentObject.SetActive(true);
        }
    }
}
