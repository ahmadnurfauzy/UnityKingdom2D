using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Origin_SkeltCreep => Resources.Load<GameObject>("Prefabs/Enemies/SkeltCreep");
    public GameObject Origin_CastLight => Resources.Load<GameObject>("Prefabs/Stuffs/CastLight");


    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }
}

public enum UnitState
{
    Idle, Move, Attack, Cast, Dead,
}

public enum EnemySet
{
    Default, Damage, Native, Warrior, Witch, Skeleton,
}
