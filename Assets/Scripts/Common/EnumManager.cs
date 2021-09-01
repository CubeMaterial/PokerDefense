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
    ResistBurning = 0x0001,
    ResistPoison = 0x0002,
    ResistBleeding = 0x0004,
    ResistAcid = 0x0008,
    ResistStun = 0x0010,
    ResistFreezing = 0x0020,
    ResistSlow = 0x0040,
    ResistSleep = 0x0080,
    Barrier = 0x0100,
    WindShield = 0x0200,
    HolyShield = 0x0400,
    MagicBarrier = 0x0800
    
}

public enum EnemyBossBuff
{

}

public enum EnemyDebuff
{
    Burnning = 0x0001,
    Poison = 0x0002,
    Bleeding = 0x0004,
    Acid = 0x0008,
    Stun = 0x0010,
    Freezing = 0x0020,
    Slow = 0x0040,
    Sleep = 0x0080,
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
    AtPoint9 = 9,
    AtPoint10 = 10,
    AtPoint11 = 11,
    AtPoint12 = 12,
    AtPoint13 = 13,
    AtPoint14 = 14,
    AtPoint15 = 15,
    AtPoint16 = 16,
    AtPoint17 = 17,
    AtPoint18 = 18,
    AtPoint19 = 19
}


public enum Enemy_Status
{
    Life,
    Speed,
    AvoidRate,
    Armor,
    ReduceDamageRate,
    End
}


public enum CardLevel
{
    Two = 0,
    Three = 1,
    Four = 2,
    Five = 3,
    Six = 4,
    Seven = 5,
    Eight = 6,
    Nine = 7,
    Ten = 8,
    J = 9,
    Q = 10,
    K = 11,
    A = 12
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
    Top = 0,
    OnePair = 1,
    TwoPair = 2,
    Triple = 3,
    Straight = 4,
    Flush = 5,
    FullHouse = 6,
    FourCard = 7,
    StraightFlush = 8
  //  R
}

