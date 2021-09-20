using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public List<Enemy> myEnemies;
    void Update()
    {
        if (myEnemies.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public bool CircleCollisionEnemy(Vector3 aPosition, float aRadius)
    {
        bool Collided = false;
        foreach(Enemy enemy in myEnemies)
        {
            if(Vector3.Distance(aPosition, enemy.transform.position) < aRadius)
            {
                Collided = true;
                enemy.OnHit();
            }
        }
        return Collided;
    }

    public bool CircleCollisionEnemyDirect(Vector3 aPosition, float aRadius)
    {
        foreach (Enemy enemy in myEnemies)
        {
            if (Vector3.Distance(aPosition, enemy.transform.position) < aRadius)
            {
                enemy.OnHit();
                return true;
            }
        }
        return false;
    }
}
