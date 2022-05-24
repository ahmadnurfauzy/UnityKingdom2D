using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemSet Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gm = GameManager.Instance;
        if(collision.CompareTag("Player") && collision.isTrigger)
        {
            var player = collision.GetComponent<PlayerActive>();
            switch (Item)
            {
                case ItemSet.Power_Green:
                    player.HealthPoint.CurrentPoint += 10;
                    DamageActive.PopupDamage(gm.Origin_Damage, transform.position, 10, DamageState.AllyHeal);
                    break;
                case ItemSet.Power_Red:
                    player.StaminaPoint.CurrentProp += 20;
                    break;
                case ItemSet.Power_Blue:
                    player.MagicPoint.CurrentProp += 50;
                    break;
                case ItemSet.Item_Elixir:
                    gm.Item_Elixir++;
                    break;
                case ItemSet.Item_Scroll:
                    gm.Item_Scroll++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
