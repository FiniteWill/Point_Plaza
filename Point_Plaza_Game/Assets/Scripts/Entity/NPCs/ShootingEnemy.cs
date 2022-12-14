using System.Collections;
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
        IProjectile temp_projBehavior = temp_projectile.GetComponent<IProjectile>();
        temp_projBehavior.Direction = direction;
        StartCoroutine(Shoot());
    }
   

}
