using UnityEngine;

public class s_PlayerInfo : MonoBehaviour
{
    [Header("플레이어 능력치")]
    public float s_moveSpeed = 5f;
    public float s_hp = 3f;

    [Header("상태")]
    public bool s_isAttack = false;
    public int s_score = 0;
}
