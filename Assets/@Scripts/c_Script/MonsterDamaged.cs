using UnityEngine;

public class MonsterDamaged : MonoBehaviour
{
    public GameObject hudDamageText;
    public Transform hudPos;
    public override void TakeDamaged(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
        base.TakeDamaged(damage);
    }
}
