using UnityEngine;

public class PetAttack : BaseAttack
{
    [Header("Pet 전용")]
    public GameObject petPrefab;
    public GameObject petBulletPrefab;

    private GameObject currentPet;
    private bool petSpawned = false;

    protected override void Initialize()
    {
        attackType = AttackType.PET;
        damage = 1f;
    }

    private void Update()
    {
        if (!isActive)
        {
            if (currentPet != null)
            {
                Destroy(currentPet);
                currentPet = null;
                petSpawned = false;
            }
            return;
        }

        if (!petSpawned)
        {
            SpawnPet();
            petSpawned = true;
        }
    }

    private void SpawnPet()
    {
        Vector3 petPos = AttackManager.Instance.firePositions[5].position;
        currentPet = Instantiate(petPrefab, petPos, Quaternion.identity);
    }

    public override void Upgrade(float damageIncrease, float spawnSpeedIncrease)
    {
        damage += damageIncrease;
    }

    protected override void Attack()
    {

    }

}