using System.Collections.Generic;
using UnityEngine;

public enum DamageVariation { MinDamage, MaxDamage, VariableDamage};
public enum CriticalKind { canCritical, notCritical, AlwaysCritical};

[System.Serializable]
public class BaseCharacter : MobileObject
{
    public int minMass;
    public int maxMass;
    public charComponent components;
    [HideInInspector]
    public CharacterController controller;
    protected int aditiveBonusHP, aditiveBonusEnergy,
        aditiveBonusSTR, aditiveBonusAGI,
        aditiveBonusDEX, aditiveBonusDEF;
    protected int multiplicativeBonusHP, multiplicativeBonusEnergy, multiplicativeBonusSTR,
        multiplicativeBonusAGI, multiplicativeBonusDEX, multiplicativeBonusDEF;
    public int currentHP, currentEnergy;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
    }
    public int CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }
    }
    protected float mBonusHP = 1, mBonusEnergy = 1;

    [HideInInspector]
    public int state = 0;

    public void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.character = this;
        controller.AwakeController();
    }

    public void Update()
    {
        controller.UpdateController();
    }

    #region physics

    #endregion

    #region Stats&Bonus
    public float getAnimationReduction()
    {
        return Mathf.Max(0.5f, 1f - (getAGI() / 300f));
    }

    public int getMaxHP()
    {

        int adder = aditiveBonusHP;

        int multiplier = multiplicativeBonusHP;

        int hpfactor = getDEF() / components.hpIncresing;

        return (int)((((components.baseMaxHP * mBonusHP * hpfactor) + adder) * (100 + multiplier)) / 100f);
    }

    public int getMaxEnergy()
    {
        int adder = aditiveBonusEnergy;
        
        int multiplier = multiplicativeBonusEnergy;

        int energyfactor = getDEF() / components.energyIncresing;

        return (int)((((components.baseEnergy * mBonusEnergy * energyfactor) + adder) * (100 + multiplier)) / 100f);
    }

    public int getSTR()
    {
        int adder = aditiveBonusSTR;

        int multiplier = multiplicativeBonusSTR;

        return (int)(((components.baseSTR + adder) * (100 + multiplier)) / 100f);
    }

    public int getAGI()
    {
        int adder = aditiveBonusAGI;

        int multiplier = multiplicativeBonusAGI;

        return (int)(((components.baseAGI + adder) * (100 + multiplier)) / 100f);
    }

    public int getDEX()
    {
        int adder = aditiveBonusDEX;

        int multiplier = multiplicativeBonusDEX;

        return (int)(((components.baseDEX + adder) * (100 + multiplier)) / 100f);
    }

    public int getDEF()
    {
        int adder = aditiveBonusDEF;

        int multiplier = multiplicativeBonusDEF;

        return (int)(((components.baseDEF + adder) * (100 + multiplier)) / 100f);
    }

    public int getDexCriticalRatio()
    {
        int criticalRatio = getDEX() / 10;

        return criticalRatio;
    }

    #endregion

    #region damage
    public int getRawDamage()
    {
        int totalSTR = getSTR();
        int totalDEX = getDEX();

        int STRFactor = totalSTR / 10;
        int DEXFactor = totalDEX / 3;

        int damage = (STRFactor * STRFactor) + totalSTR + DEXFactor;

        return damage;
    }

    public void DealDamage(int damage, int baseDamage, int dexCriticalRatio, int criticalRatio, BaseCharacter affectedCharacter)
    {
        int totalDEF = affectedCharacter.getDEF();
        int DEFFactor = totalDEF / 20;
        int realDamage = damage - (DEFFactor * DEFFactor);
        if (realDamage <= 0) realDamage = 0;
        realDamage = (int)((realDamage * Random.Range(0.85f, 1) * baseDamage) / totalDEF);

        float criticalChance = dexCriticalRatio / (float)affectedCharacter.getAGI();

        criticalChance += (criticalRatio / 100f);

        if (Random.Range(0, 1) <= criticalChance)
        {
            realDamage *= 2;
        }

        affectedCharacter.hurt(realDamage);
    }

    public void hurt(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            die();
        }
    }

    public virtual void die()
    {
        Destroy(gameObject);
    }

    #endregion
}

