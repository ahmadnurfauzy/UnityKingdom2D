using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Origin_SkeltCreep => Resources.Load<GameObject>("Prefabs/Enemies/SkeltCreep");
    public GameObject Origin_CastLight => Resources.Load<GameObject>("Prefabs/Stuffs/CastLight");
    public GameObject Origin_DamageCreep => Resources.Load<GameObject>("Prefabs/Enemies/DamageCreep");
    public GameObject Origin_NativeCreep => Resources.Load<GameObject>("Prefabs/Enemies/NativeCreep");
    public GameObject Origin_WarriorCreep => Resources.Load<GameObject>("Prefabs/Enemies/WarriorCreep");
    public GameObject Origin_WitchCreep => Resources.Load<GameObject>("Prefabs/Enemies/WitchCreep");
    public GameObject Origin_Fire1 => Resources.Load<GameObject>("Prefabs/Stuffs/Fire_Style1");
    public GameObject Origin_Fire2 => Resources.Load<GameObject>("Prefabs/Stuffs/Fire_Style2");
    public GameObject Origin_Shell => Resources.Load<GameObject>("Prefabs/Stuffs/ShellEffect");
    public GameObject Origin_Damage => Resources.Load<GameObject>("Prefabs/Stuffs/DamagePop");

    public int GamePoint;
    public int KillPoint;

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

public enum SkillSet
{
    Default, SplashSwing, DemonShell,
}

public enum DamageState
{
    Default, 
    PlayerPhs, PlayerMag, 
    EnemyPhs, AllyHeal,
}