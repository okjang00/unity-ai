using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float bulletSpawnOffset = 0.5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            TryFire();
        }
    }

    private void TryFire()
    {
        if (BulletPool.Instance == null)
        {
            return;
        }

        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null)
        {
            return;
        }

        Vector2 direction = (nearestEnemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Vector3 spawnPosition = transform.position + (Vector3)(direction * bulletSpawnOffset);

        BulletPool.Instance.Get(spawnPosition, rotation);
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }
}
