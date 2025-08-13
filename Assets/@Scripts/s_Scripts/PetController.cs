using UnityEngine;

public class PetController : MonoBehaviour
{
    [Header("Pet 설정")]
    public float moveSpeed = 10f;
    public float fireRate = 1f;

    private Transform currentTarget;
    private Vector3 attackPosition;
    private Vector3 homePosition;
    private float fireTimer = 0f;
    private PetAttack petAttack;

    private enum PetState { SearchingTarget, MovingToPosition, Attacking, ReturningHome }
    private PetState currentState;

    void Start()
    {
        petAttack = AttackManager.Instance.GetPetAttack();
        homePosition = AttackManager.Instance.firePositions[5].position;
        currentState = PetState.SearchingTarget;
    }

    void Update()
    {
        switch (currentState)
        {
            case PetState.SearchingTarget:
                SearchForTarget();
                break;
            case PetState.MovingToPosition:
                MoveToAttackPosition();
                break;
            case PetState.Attacking:
                AttackTarget();
                break;
            case PetState.ReturningHome:
                ReturnHome();
                break;
        }
    }

    private void SearchForTarget()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        // 카메라 영역 내에 있는 몬스터들만 필터링
        Monster[] validMonsters = System.Array.FindAll(monsters, monster => IsInCamera(monster.transform));

        if (validMonsters.Length > 0)
        {
            // 카메라 내 몬스터 중에서만 랜덤 선택
            currentTarget = validMonsters[Random.Range(0, validMonsters.Length)].transform;

            // 타겟 y-2 위치로 공격 위치 설정
            attackPosition = new Vector3(currentTarget.position.x, currentTarget.position.y - 2f, currentTarget.position.z);

            currentState = PetState.MovingToPosition;
        }
        else
        {
            // 카메라 내 몬스터 없으면 집으로
            currentState = PetState.ReturningHome;
        }
    }

    private bool IsInCamera(Transform target)
    {
        if (target == null) return false;

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);

        return screenPos.x >= 0 && screenPos.x <= Screen.width &&
               screenPos.y >= 0 && screenPos.y <= Screen.height;
    }

    private void MoveToAttackPosition()
    {
        // 타겟이 죽었거나 카메라 밖으로 나가면 재탐색
        if (currentTarget == null || IsTargetOutOfCamera())
        {
            currentState = PetState.SearchingTarget;
            return;
        }

        // 실시간으로 타겟 위치 업데이트
        attackPosition = new Vector3(currentTarget.position.x, currentTarget.position.y - 2f, currentTarget.position.z);

        // 공격 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, attackPosition, moveSpeed * Time.deltaTime);

        // 도착했으면 공격 상태로
        if (Vector3.Distance(transform.position, attackPosition) < 0.5f) // 0.1f -> 0.5f로 여유있게
        {
            currentState = PetState.Attacking;
        }
    }

    private void AttackTarget()
    {
        // 타겟이 죽었거나 카메라 밖으로 나가면 재탐색
        if (currentTarget == null || IsTargetOutOfCamera())
        {
            currentState = PetState.SearchingTarget;
            return;
        }

        // 펫 자체도 카메라 밖으로 나가면 집으로 복귀
        if (IsPetOutOfCamera())
        {
            currentState = PetState.ReturningHome;
            return;
        }

        // 실시간으로 타겟 위치 업데이트하면서 따라가기
        attackPosition = new Vector3(currentTarget.position.x, currentTarget.position.y - 2f, currentTarget.position.z);
        transform.position = Vector3.MoveTowards(transform.position, attackPosition, moveSpeed * Time.deltaTime);

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            FireBullet();
            fireTimer = 0f;
        }
    }

    private bool IsTargetOutOfCamera()
    {
        if (currentTarget == null) return true;

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(currentTarget.position);

        return screenPos.x < 0 || screenPos.x > Screen.width ||
               screenPos.y < 0 || screenPos.y > Screen.height;
    }

    private bool IsPetOutOfCamera()
    {
        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        return screenPos.x < 0 || screenPos.x > Screen.width ||
               screenPos.y < 0 || screenPos.y > Screen.height;
    }


    private void ReturnHome()
    {
        // 실시간으로 홈 위치 업데이트
        homePosition = AttackManager.Instance.firePositions[5].position;

        transform.position = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);

        // 집에 도착했으면 다시 탐색
        if (Vector3.Distance(transform.position, homePosition) < 0.1f)
        {
            currentState = PetState.SearchingTarget;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(petAttack.petBulletPrefab, transform.position, Quaternion.identity);
    }
}
