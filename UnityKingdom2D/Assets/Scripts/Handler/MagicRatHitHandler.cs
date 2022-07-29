using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRatHitHandler : MonoBehaviour
{
    public float AttackPoint;
    public bool HitArea;

    private int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var position = collision.transform.position;
        var gm = GameManager.Instance;
        if (collision.CompareTag("Enemy") && collision.isTrigger)
        {
            var enemy = collision.GetComponent<EnemyActive>();
            if (HitArea)
            {

                enemy.HealthPoint -= AttackPoint;
                DamageActive.PopupDamage(gm.Origin_Damage, position, AttackPoint, DamageState.PlayerMag);
            }
            else
            {
                if (count == 0)
                {
                    enemy.HealthPoint -= AttackPoint;
                    DamageActive.PopupDamage(gm.Origin_Damage, position, AttackPoint, DamageState.PlayerMag);
                }
                count++;
            }
        }
    }
    private void OnDisable()
    {
        count = 0;
    }
}
