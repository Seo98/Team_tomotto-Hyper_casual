using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    [Header("공돌 능력치")]
    public AttackType attackType;
    public float damage;
    public float spawnTime;
    public bool isActive = false;

    protected float timer = 0f;

    protected virtual void Start()
    {
        Initialize();
    }

    // 자식에서 초기값 등 따로따로 세팅
    protected abstract void Initialize();
    protected abstract void Attack();
    //업글
    public abstract void Upgrade(float damageIncrease, float spawnSpeedIncrease);

    #region 활성화 비활성화 // 초기화 + 비활성화 함수
    public virtual void Activate()
    {
        isActive = true;
    }
    public virtual void Deactivate()
    {
        isActive = false;
    }
    public void ResetToDefault()
    {
        Initialize(); // protected를 public에서 호출
        Deactivate();
    }
    #endregion
}