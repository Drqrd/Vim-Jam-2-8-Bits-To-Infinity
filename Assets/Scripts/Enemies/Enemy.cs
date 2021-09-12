using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public virtual void Init()
    {
        AddEnemy();
    }
    public void AddEnemy()
    {
        EnemyManager.Instance.myEnemies.Add(this);
    }

    public void RemoveEnemy()
    {
        EnemyManager.Instance.myEnemies.Remove(this);
    }

    public virtual void OnHit()
    {

    }
}
