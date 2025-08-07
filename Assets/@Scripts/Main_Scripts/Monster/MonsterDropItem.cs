using UnityEngine;

public class MonsterDropItem : MonoBehaviour
{
    // Dev_S: 생성만 하는 아이
    [SerializeField] GameObject[] items;

    public void DropItem(Vector3 dropPos)
    {
        int Index = Random.Range(0,2);
        GameObject item = Instantiate(items[Index], dropPos, Quaternion.identity);
    }

}