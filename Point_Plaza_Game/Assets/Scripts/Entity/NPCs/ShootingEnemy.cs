using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float fireDelay = 2f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private Transform projectileSpawnPos = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(fireDelay);
        GameObject temp_projectile = Instantiate(projectilePrefab, projectileSpawnPos);
        temp_projectile.GetComponent<IProjectile>();
        StartCoroutine(Shoot());
    }
   

}
