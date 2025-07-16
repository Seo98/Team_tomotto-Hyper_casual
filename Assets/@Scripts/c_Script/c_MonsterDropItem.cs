using UnityEngine;

public class c_MonsterDropItem : MonoBehaviour
{
    [SerializeField] GameObject[] c_items;

    void DropItem(Vector3 dropPos)
    {
        var randomIndex = Random.Range(0, c_items.Length);

        GameObject c_item = Instantiate(c_items[randomIndex], dropPos, Quaternion.identity);

        Rigidbody2D c_itemRb = c_item.GetComponent<Rigidbody2D>();

        c_itemRb.AddForceY(3f, ForceMode2D.Impulse);
    }

}
