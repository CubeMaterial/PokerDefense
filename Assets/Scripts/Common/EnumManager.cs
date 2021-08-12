using UnityEngine;
using System.Collections;

public class EnumManager : MonoBehaviour 
{
}

public enum Status
{
    MinAttackPoint,
    MaxAttackPoint,
    FixedAttackPoint,
    AddAttackPoint,
    BossAttackPoint,
    AttackDelay,
    CriticalRate,
    CriticalFactor,
    Accuracy,
    End
}

public enum Tower_Ability
{
    CriticalHit = 0x00000001,
    BaseDmg = 0x00000002,
    FixedDmg = 0x00000004,
    AddDmg = 0x00000008,
    BossDmg = 0x00000010,
    ReduceDelay,
    AddfAccuracy,
    //Elemental_Sword = 0x00000020,
    //Elemental_Sphere = 0x00000040,
    //Elemental_Shot = 0x00000080,
    //Elemental_SubShot = 0x00000100,
    //Elemental_Bomb = 0x00000200,
    //Elemental_Buff = 0x00000400,
    //Elemental_Debuff = 0x00000800,
    //BlazeShot = 0x00001000,
    //FreezingShot = 0x00002000,
    //ToxicShot = 0x00004000,
    //ElectronicShot = 0x00008000,
    //LegShot = 0x00010000,
    //HeadShot = 0x00020000,
    //ReflectionShot = 0x00040000,
    //EntangleShot = 0x00080000,
    //Bash,
    //ThunderNova,
    //ShockWave,
    //WarHammer,
    //ThunderLightning,



}

//public enum Status_State
//{ 
//    Base,
//    Ability,
//    Card,
//    Upgrade,
//    Result,
//    End
//}

public enum EnemyState
{
    Ready = 1,
    Move = 2,
    Death = 4
}

public enum EnemyBuff
{
    DamageReduceRate = 0x01,
    DamageReduceFixed = 0x02,
    MeeleDamageReduceRate = 0x04,
    MagicDamageReduceRate = 0x08,

    Boss = 0x10,
    End = 0x80
}

public enum EnemyDebuff
{
    Burn = 0x0001,
    Ice = 0x0002,
    Poison = 0x0004,
    Lightning = 0x0008,
    Slow = 0x0010,
    Bleeding = 0x0020,
    Stun = 0x0040,
    Sleep = 0x0080,

    End
}

public enum EnemyMoveState
{
    AtPoint0 = 0,
    AtPoint1 = 1,
    AtPoint2 = 2,
    AtPoint3 = 3,
    AtPoint4 = 4,
    AtPoint5 = 5,
    AtPoint6 = 6,
    AtPoint7 = 7,
    AtPoint8 = 8,
    AtPoint9 = 9
}


public enum Enemy_Status
{
    Life,
    Speed,
    AvoidRate,
    FixedArmor,
    ReduceArmor,
    End
}


public enum CardLevel
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    J,
    Q,
    K,
    A
}

public enum CardShape
{
    Heart,
    Diamond,
    Clover,
    Spade,
    Joker
}


public enum Status_State
{ 
    Origin,
    Current,
    End
}
public enum DeckType
{ 
    Main,
    Sub,
    Quest
}

public enum ChipType
{
    White = 0,
    Blue = 1,
    Black = 2
}

public enum Grade
{ 
    Top = 1,
    OnePair = 2,
    TwoPair = 3,
    Triple = 4,
    Straight = 5,
    Flush = 6,
    FullHouse = 7,
    FourCard = 8
  //  R
}

