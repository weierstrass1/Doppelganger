public enum BonusKind : int
{
    baMHP = -4, baEnergy = -3,
    bmMHP = -2, bmEnergy = -1,
    aMHP=0, aEnergy=1, aSTR=2, aAGI=3, aDEX=4, aDEF=5,
    mMHP=6, mEnergy=7, mSTR=8, mAGI=9, mDEX=10, mDEF=11
};
public class bonusRegister
{
    BonusKind kind;
    public BonusKind Kind { get { return kind; } }
    BaseCharacter target;
    public BaseCharacter Target { get { return target; } }
    int index;
    public int Index { get { return index; } }
    int bonus;
    public int Bonus { get { return bonus; } }

    public bonusRegister(BonusKind bKind, int bIndex, BaseCharacter bTarget,int bBonus)
    {
        index = bIndex;
        kind = bKind;
        target = bTarget;
        bonus = bBonus;
    }
}
