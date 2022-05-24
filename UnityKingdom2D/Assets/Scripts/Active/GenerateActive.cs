using System;
using UnityEngine;

public class GenerateActive : MonoBehaviour
{
    public GameObject[] Creeps;
    public float DelayTime;

    public Action<Transform, string> GenerateAction { get; set; }

    public bool IsReadySpawn
    {
        set
        {
            if (value)
            {
                if (spawntime > 0)
                {
                    spawntime -= Time.deltaTime;
                }

                if (spawntime < 0)
                {
                    spawntime = 1f;
                    GenerateAction?.Invoke(transform, transform.tag);
                }
            }
        }
    }

    public int Stock
    {
        get
        {
            return stock;
        }
        set
        {
            stock = value;
            Creeps = new GameObject[stock];
        }
    }

    private int stock;
    private float castime;
    private float spawntime;

    private void Start()
    {
        castime = DelayTime;
    }

    private void Update()
    {
        if (castime > 0)
        {
            castime -= Time.deltaTime;
        }

        if (castime < 0)
        {
            castime = 0;
            spawntime = 1f;
        }

        IsReadySpawn = transform.childCount < Stock;
    }
}