using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageActive : MonoBehaviour
{
    public static DamageActive PopupDamage(GameObject popup, Vector3 position, float amount, DamageState state)
    {
        var damage = Instantiate(popup, position, Quaternion.identity);
        var damageActive = damage.GetComponent<DamageActive>();
        damageActive.Setup((int)amount, state);
        return damageActive;
    }

    public void Setup(int damage, DamageState state)
    {
        timer = 1f;
        switch (state)
        {
            case DamageState.PlayerPhs:
                textcolor = Color.red;
                movevector = new Vector3(Random.Range(0.5f, 1.5f), 1) * Random.Range(4f, 6f);
                break;
            case DamageState.PlayerMag:
                textcolor = Color.blue;
                movevector = new Vector3(Random.Range(0.5f, 1.5f), 1) * Random.Range(4f, 6f);
                break;
            case DamageState.EnemyPhs:
                textcolor = Color.yellow;
                movevector = new Vector3(Random.Range(-0.5f, -1.5f), 1) * Random.Range(4f, 6f);
                break;
            case DamageState.AllyHeal:
                textcolor = Color.green;
                movevector = new Vector3(Random.Range(-0.5f, -1.5f), -1) * Random.Range(4f, 6f);
                break;
        }
        TxtPopup.SetText(damage.ToString());
        TxtPopup.color = textcolor;
    }

    private TextMeshPro TxtPopup;
    private Vector3 movevector;
    private Color textcolor;
    private float timer;

    private void Awake()
    {
        TxtPopup = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        var speed = 4f;
        transform.position += movevector * Time.deltaTime;
        movevector -= movevector * speed * Time.deltaTime;

        if (timer > 0.5f)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            textcolor.a -= speed * Time.deltaTime;
            TxtPopup.color = textcolor;
            if (textcolor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
