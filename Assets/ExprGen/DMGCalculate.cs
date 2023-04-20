using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DMGCalculate
{

    /// <summary>
    /// [攻]攻击力
    /// </summary>
    public float BaseATK = 0;

    /// <summary>
    /// [攻]暴击率
    /// </summary>
    public float BaseCritRate = 0;

    /// <summary>
    /// [攻]暴击伤害
    /// </summary>
    public float BaseCritDMG = 0;

    /// <summary>
    /// [攻]武器攻击力
    /// </summary>
    public float WeaponATK = 0;

    /// <summary>
    /// [攻]武器暴击率
    /// </summary>
    public float WeaponCritRate = 0;

    /// <summary>
    /// [攻]武器暴击伤害
    /// </summary>
    public float WeaponCritDMG = 0;

    /// <summary>
    /// [攻]武器特殊词条攻击力乘区
    /// </summary>
    public float WeaponATKMulFactor = 0;

    /// <summary>
    /// [攻]武器特殊词条攻击力加区
    /// </summary>
    public float WeaponATKAddFactor = 0;

    /// <summary>
    /// [攻]装备特殊词条攻击力乘区
    /// </summary>
    public float EquipATKMulFactor = 0;

    /// <summary>
    /// [攻]装备特殊词条攻击力加区
    /// </summary>
    public float EquipATKAddFactor = 0;

    /// <summary>
    /// [守]防御力
    /// </summary>
    public float BaseDEF = 0;

    /// <summary>
    /// [守]装备总防御力
    /// </summary>
    public float EquipDef = 0;

    /// <summary>
    /// [守]武器特殊词条防御力乘区
    /// </summary>
    public float WeaponDEFMulFactor = 0;

    /// <summary>
    /// [守]武器特殊词条防御力加区
    /// </summary>
    public float WeaponDEFAddFactor = 0;

    /// <summary>
    /// [守]装备特殊词条防御力乘区
    /// </summary>
    public float EquipDEFMulFactor = 0;

    /// <summary>
    /// [守]装备特殊词条防御力加区
    /// </summary>
    public float EquipDEFAddFactor = 0;


    ///<param name="BaseATK">[攻]攻击力</param>
    ///<param name="BaseCritRate">[攻]暴击率</param>
    ///<param name="BaseCritDMG">[攻]暴击伤害</param>
    ///<param name="WeaponATK">[攻]武器攻击力</param>
    ///<param name="WeaponCritRate">[攻]武器暴击率</param>
    ///<param name="WeaponCritDMG">[攻]武器暴击伤害</param>
    ///<param name="WeaponATKMulFactor">[攻]武器特殊词条攻击力乘区</param>
    ///<param name="WeaponATKAddFactor">[攻]武器特殊词条攻击力加区</param>
    ///<param name="EquipATKMulFactor">[攻]装备特殊词条攻击力乘区</param>
    ///<param name="EquipATKAddFactor">[攻]装备特殊词条攻击力加区</param>
    ///<param name="BaseDEF">[守]防御力</param>
    ///<param name="EquipDef">[守]装备总防御力</param>
    ///<param name="WeaponDEFMulFactor">[守]武器特殊词条防御力乘区</param>
    ///<param name="WeaponDEFAddFactor">[守]武器特殊词条防御力加区</param>
    ///<param name="EquipDEFMulFactor">[守]装备特殊词条防御力乘区</param>
    ///<param name="EquipDEFAddFactor">[守]装备特殊词条防御力加区</param>

    public DMGCalculate(float BaseATK, float BaseCritRate, float BaseCritDMG, float WeaponATK, float WeaponCritRate, float WeaponCritDMG, float WeaponATKMulFactor, float WeaponATKAddFactor, float EquipATKMulFactor, float EquipATKAddFactor, float BaseDEF, float EquipDef, float WeaponDEFMulFactor, float WeaponDEFAddFactor, float EquipDEFMulFactor, float EquipDEFAddFactor)
    {
        this.BaseATK = BaseATK;
        this.BaseCritRate = BaseCritRate;
        this.BaseCritDMG = BaseCritDMG;
        this.WeaponATK = WeaponATK;
        this.WeaponCritRate = WeaponCritRate;
        this.WeaponCritDMG = WeaponCritDMG;
        this.WeaponATKMulFactor = WeaponATKMulFactor;
        this.WeaponATKAddFactor = WeaponATKAddFactor;
        this.EquipATKMulFactor = EquipATKMulFactor;
        this.EquipATKAddFactor = EquipATKAddFactor;
        this.BaseDEF = BaseDEF;
        this.EquipDef = EquipDef;
        this.WeaponDEFMulFactor = WeaponDEFMulFactor;
        this.WeaponDEFAddFactor = WeaponDEFAddFactor;
        this.EquipDEFMulFactor = EquipDEFMulFactor;
        this.EquipDEFAddFactor = EquipDEFAddFactor;
    }




    /// <summary>
    /// 
    /// </summary>
    public float FinalDamage => GetFinalDamage();

    /// <summary>
    /// 
    /// </summary>
    public float FinalCritDamage => GetFinalCritDamage();

    /// <summary>
    /// 
    /// </summary>
    public float CritDamage => GetCritDamage();

    /// <summary>
    /// 
    /// </summary>
    public float CritDMGFactor => GetCritDMGFactor();

    /// <summary>
    /// 
    /// </summary>
    public float BaseDamage => GetBaseDamage();

    /// <summary>
    /// 最终面板攻击
    /// </summary>
    public float FinalCharaATK => GetFinalCharaATK();

    /// <summary>
    /// 最终面板防御
    /// </summary>
    public float FinalCharaDEF => GetFinalCharaDEF();

    /// <summary>
    /// 最终面板暴击率
    /// </summary>
    public float FinalCritRate => GetFinalCritRate();

    /// <summary>
    /// 最终武器面板攻击力乘区
    /// </summary>
    public float FinalWeaponATKMulFactor => GetFinalWeaponATKMulFactor();

    /// <summary>
    /// 最终武器面板攻击力加区
    /// </summary>
    public float FinalWeaponATKAddFactor => GetFinalWeaponATKAddFactor();

    /// <summary>
    /// 最终装备面板攻击力乘区
    /// </summary>
    public float FinalEquipATKMulFactor => GetFinalEquipATKMulFactor();

    /// <summary>
    /// 最终装备面板攻击力加区
    /// </summary>
    public float FinalEquipATKAddFactor => GetFinalEquipATKAddFactor();

    /// <summary>
    /// 最终武器面板防御力乘区
    /// </summary>
    public float FinalWeaponDEFMulFactor => GetFinalWeaponDEFMulFactor();

    /// <summary>
    /// 最终武器面板防御力加区
    /// </summary>
    public float FinalWeaponDEFAddFactor => GetFinalWeaponDEFAddFactor();

    /// <summary>
    /// 最终装备面板防御力乘区
    /// </summary>
    public float FinalEquipDEFMulFactor => GetFinalEquipDEFMulFactor();

    /// <summary>
    /// 最终装备面板防御力加区
    /// </summary>
    public float FinalEquipDEFAddFactor => GetFinalEquipDEFAddFactor();

    /// <summary>
    /// 
    /// </summary>
    public float DMG_K1 => GetDMG_K1();

    /// <summary>
    /// 
    /// </summary>
    public float DMG_K2 => GetDMG_K2();

    /// <summary>
    /// 
    /// </summary>
    public float DMG_K3 => GetDMG_K3();

    /// <summary>
    /// 
    /// </summary>
    public float DMG_K4 => GetDMG_K4();



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetFinalDamage()
    {
        return BaseDamage + FinalCritDamage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetFinalCritDamage()
    {
        return CritDamage * Random.Range(1, 100) < FinalCritRate ? 1 : 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetCritDamage()
    {
        return FinalCharaATK * (1 + CritDMGFactor);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetCritDMGFactor()
    {
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetBaseDamage()
    {
        return (FinalCharaATK * DMG_K1 + DMG_K2) / (FinalCharaATK + FinalCharaDEF * DMG_K3 + DMG_K4);
    }

    /// <summary>
    /// 最终面板攻击
    /// </summary>
    /// <returns></returns>
    private float GetFinalCharaATK()
    {
        return BaseATK + WeaponATK;
    }

    /// <summary>
    /// 最终面板防御
    /// </summary>
    /// <returns></returns>
    private float GetFinalCharaDEF()
    {
        return BaseDEF + EquipDef;
    }

    /// <summary>
    /// 最终面板暴击率
    /// </summary>
    /// <returns></returns>
    private float GetFinalCritRate()
    {
        return BaseCritRate + WeaponCritRate;
    }

    /// <summary>
    /// 最终武器面板攻击力乘区
    /// </summary>
    /// <returns></returns>
    private float GetFinalWeaponATKMulFactor()
    {
        return WeaponATKMulFactor;
    }

    /// <summary>
    /// 最终武器面板攻击力加区
    /// </summary>
    /// <returns></returns>
    private float GetFinalWeaponATKAddFactor()
    {
        return WeaponATKAddFactor;
    }

    /// <summary>
    /// 最终装备面板攻击力乘区
    /// </summary>
    /// <returns></returns>
    private float GetFinalEquipATKMulFactor()
    {
        return EquipATKMulFactor;
    }

    /// <summary>
    /// 最终装备面板攻击力加区
    /// </summary>
    /// <returns></returns>
    private float GetFinalEquipATKAddFactor()
    {
        return EquipATKAddFactor;
    }

    /// <summary>
    /// 最终武器面板防御力乘区
    /// </summary>
    /// <returns></returns>
    private float GetFinalWeaponDEFMulFactor()
    {
        return WeaponDEFMulFactor;
    }

    /// <summary>
    /// 最终武器面板防御力加区
    /// </summary>
    /// <returns></returns>
    private float GetFinalWeaponDEFAddFactor()
    {
        return WeaponDEFAddFactor;
    }

    /// <summary>
    /// 最终装备面板防御力乘区
    /// </summary>
    /// <returns></returns>
    private float GetFinalEquipDEFMulFactor()
    {
        return EquipDEFMulFactor;
    }

    /// <summary>
    /// 最终装备面板防御力加区
    /// </summary>
    /// <returns></returns>
    private float GetFinalEquipDEFAddFactor()
    {
        return EquipDEFAddFactor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetDMG_K1()
    {
        return FinalWeaponATKMulFactor + FinalEquipATKMulFactor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetDMG_K2()
    {
        return FinalWeaponATKAddFactor + FinalEquipATKAddFactor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetDMG_K3()
    {
        return FinalWeaponDEFMulFactor + FinalEquipDEFMulFactor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetDMG_K4()
    {
        return FinalWeaponDEFAddFactor + FinalEquipDEFAddFactor;
    }


}
