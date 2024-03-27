using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnumManager
{
    public enum PLATFORM
    {
        android,
        ios,
        webgl,
        standalone,
    }


    #region KIND OF MAPS
    public enum KIND_OF_MAPS
    {
        GlassMap,
        RedLand,
        SnowLand,
    }
    #endregion



    #region DIFFICUFT
    public enum DIFFICUFT
    {
        Normal,
        Hard,
        Nightmate,
    }
    public enum LEVEL_DIFFICIFT
    {
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5,
        Level_6,
        Level_7,
        Level_8,
        Level_9,
        Level_10,
    }
    #endregion


    #region ITEMS
    public enum ITEMS
    {
        Background,
        PointBuilding,
        Tower,
        Hero,
        StartingWaveMark,
    }
    public enum ITEMS_POOLING
    {
        Bullet_TowerStone,
        Bullet_TowerArcher,
        Bullet_TowerMagic,
        BowDetail,
        CrackHole,
        BloodOfEnemy,
        Explosion_of_mine,
        FreezeEff,
        Smock,
        FireFromSky,
        CircleLight_Freeze,
        BloodMark,
        CircleLight_Hero,
        Bullet_RocketLaucher,
        Explosion_of_Rocket,
        Bullet_Posion,
        Explosion_Poision,
        EffEnemyText,
    }
    #endregion



    #region SCENE
    public enum SCENE
    {
        Gameplay,
        LevelSelection,
        Menu,
        Upgrade,
        EndGame,
        Encyclopedia,
    }
    #endregion




    #region TOWER
    public enum TOWER
    {
        tower_soldier,
        tower_magic,
        tower_archer,
        tower_cannonner,
        soldier,
        tower_gunmen,
        tower_poison,
        tower_rocket_laucher,
        tower_thunder,

    }

    public enum TOWER_LEVEL
    {
        level_1,
        level_2,
        level_3,
        level_4,

    }
    #endregion


    #region PRODUCT
    public enum GEM_PACK
    {
        gempack_1,
        gempack_2,
        gempack_3,
        gempack_4,
        gempack_5,
        gempack_6,
    }
    public enum COIN_PACK
    {
        coinpack_1,
        coinpack_2,
        coinpack_3,
        coinpack_4,
        coinpack_5,
        coinpack_6,
    }

    #endregion

    #region ENEMY
    public enum ENEMY
    {
        enemy_bad,
        enemy_bandit_with_axe,
        enemy_bandit_with_knife,
        enemy_bandit_with_sword,
        enemy_bandit_with_woodden_mace,
        enemy_bandit_with_woodden_sword,
        enemy_bandit_with_mace,
        enemy_bone,
        enemy_bug,
        enemy_executioner,
        enemy_fat_man,
        enemy_mercenary_green,
        enemy_mercenary_red_hair,
        enemy_red_bull,
        enemy_snail,
        enemy_spide_blue,
        enemy_spide_gray,
        enemy_spide_red,
        enemy_spide_yellow,
        enemy_the_dead,
        enemy_wofl_blue,
        enemy_wofl_gray,
        enemy_wofl_green,
        enemy_wofl_horn_blue,
        enemy_wofl_horn_gray,
        enemy_wofl_horn_green,
        enemy_wofl_soldier_blue,
        enemy_wofl_soldier_gray,
        enemy_wofl_soldier_green,
        enemy_soldier_axe,
        enemy_soldier_sword,
        enemy_flying_wood,
        enemy_airship,

        //BOSS
        boss_bad,
        boss_bear,
        boss_bone,
        boss_kingkong,
        boss_sung_bo,
        boss_toc_vang,
        boss_ariship,
        boss_bad_black,
        boss_bandit_with_sword,
        boss_bandit_with_woodden_mace,
        boss_bug,
        boss_flying_wood,
        boss_spide_blue,
        boss_spide_red,
        boss_the_dead,
        boss_bandit_with_mace,
        boss_spide_yellow,


    }
    public enum ENEMY_KIND
    {
        All,
        Airforce,
        Infantry,
    }
    #endregion



    #region BOARD INFO
    public enum BOARD_INFO
    {
        GemBoard,
        CoinBoard,
        WaveBoard,

        HeartBoard,
        StarToUpgradeBoard_Normal,
        StarToUpgradeBoard_Hard,
        StarToUpgradeBoard_Nightmate,

        TotalStarBoard_Normal,
        TotalStarBoard_Hard,
        TotalStarBoard_Nightmate,
    }
    #endregion



    #region KIND OF STAR 
    public enum STAR
    {
        white,
        blue,
        yellow,
    }
    #endregion


    #region  UGRADE CONTENT
    public enum UPGRADE
    {
        //SKILL ------------------------
        Skill_MoreRangeForFireFromSky,
        Skill_MoreRangeForMineOnRoad,
        Skill_MoreTimeForFreeze,

        //REINFORCEMENT
        Reinforcement_2To3Man,
        Reinforcement_MoreTimelife30Percent,
        Reinforcement_MoreTimelife50Percent,
        Reinforcement_MoreHp,

        //ENEMY
        Enemy_ReduceMovementSpeedAll,
        Enemy_ReduceMovementSpeed_Airforce,
        Enemy_ReduceMovementSpeed_Infantry,
        Enemy_ReduceDamage,


        //BOSS
        Boss_ReduceMovementSpeed,


        //TOWER
        TowerArcher_MoreDamage,
        TowerArcher_MoreRange,
        TowerArcher_ReducePriceToBuild,

        TowerThunder_MoreDamage,
        TowerThunder_MoreRange,
        TowerThunder_ReducePriceToBuild,

        TowerGunmen_MoreDamage,
        TowerGunmen_MoreRange,
        TowerGunmen_ReducePriceToBuild,

        TowerMagic_MoreDamage,
        TowerMagic_MoreRange,
        TowerMagic_ReducePriceToBuild,

        TowerPoison_MoreDamage,
        TowerPoison_MoreRange,
        TowerPoison_ReducePriceToBuild,

        TowerRocketLaucher_MoreDamage,
        TowerRocketLaucher_MoreRange,
        TowerRocketLaucher_ReducePriceToBuild,

        TowerCannonner_MoreDamage,
        TowerCannonner_MoreRange,
        TowerCannonner_ReducePriceToBuild,


        //OTHER
        Other_ReducePriceTowerToBuildForAllTower,
        Other_MoreAttackSpeedForAllTower,


    }
    #endregion

    #region FACTOR_TYPE
    public enum FACTOR_TYPE
    {
        up,
        down,
    }

    #endregion

    #region STAR TYPE
    public enum STAR_TYPE
    {
        white,
        blue,
        yellow,
    }
    #endregion

    #region SKILLS IN GAME
    public enum POWER_UP
    {
        guardian, //Ve than
        freeze,
        fire_of_lord, // tha bom vao vi tri
        mine_on_road, // dat bom tren duong
        poison,// Add blood for hero
        Null,
    }
    public static TheEnumManager.POWER_UP ConverStringToEnum_Skill(string _id)
    {
        int _total = System.Enum.GetNames(typeof(POWER_UP)).Length;
        for (int i = 0; i < _total; i++)
        {
            if (_id == ((POWER_UP)i).ToString())
            {
                return ((POWER_UP)i);
            }
        }
        return POWER_UP.Null;
    }
    #endregion

}
