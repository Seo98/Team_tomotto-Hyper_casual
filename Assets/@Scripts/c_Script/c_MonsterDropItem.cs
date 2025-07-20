using UnityEngine;

public class c_MonsterDropItem : MonoBehaviour
{
    [SerializeField] GameObject[] c_items;

    //몬스터 체력 받아옴

    private void Start()
    {
        //만약 몬스터 체력이 0일 때 현재 위치에서 드랍 아이템
    }

    void DropItem(Vector3 dropPos)
    {
        var randomIndex = Random.Range(0, c_items.Length);

        GameObject c_item = Instantiate(c_items[randomIndex], dropPos, Quaternion.identity);

        Rigidbody2D c_itemRb = c_item.GetComponent<Rigidbody2D>();

        c_itemRb.AddForceY(3f, ForceMode2D.Impulse);
    }

}
