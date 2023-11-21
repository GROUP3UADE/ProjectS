using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{

    //POOL PARA PROJECTILES DEL JEFE (ARROWS)
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private Queue<GameObject> pool;

    void Start()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            pool.Enqueue(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        if (pool.Count > 0)
        {
            GameObject projectile = pool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            return Instantiate(projectilePrefab);
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        pool.Enqueue(projectile);
    }

}
