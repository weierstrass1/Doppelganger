using UnityEngine;

public class projectileLauncher : MonoBehaviour
{
    public GameObject projectil;
    public float XSpeed, YSpeed;
    public float XAccel, YAccel;
    public float XFriction, YFriction;
    public float XGravity, YGravity;
    public int size = 1, initialEffectSize = 1, endEffectSize = 1;
    public BaseCharacter owner;
    public bool CanFlipProjectile = false;
    public bool CanFlipInitialEffect = false;
    public bool CanFlipEndEffect = false;
    public int damage = 0;
    public int baseDamage = 0;
    public int criticalRatio = 0;
    public int dexCriticalRatio = 0;

    public void launchProjectil(bool flipX, bool flipY)
    {
        BaseCharacter bc = projectil.GetComponent<BaseCharacter>();
        bc.XSpeed = XSpeed;
        bc.YSpeed = YSpeed;
        bc.XAccel = XAccel;
        bc.YAccel = YAccel;
        bc.XFriction = XFriction;
        bc.YFriction = YFriction;
        bc.XGravity = XGravity;
        bc.YGravity = YGravity;

        if(flipX)
        {
            bc.XSpeed = -bc.XSpeed;
            bc.XAccel = -bc.XAccel;
            if (CanFlipProjectile)
            {
                projectil.GetComponent<charComponent>().FlipX = flipX;
            }
        }
        if(flipY)
        {
            bc.YSpeed = -bc.YSpeed;
            bc.YAccel = -bc.YAccel;
        }

        bc.XSpeed += owner.XSpeed;
        bc.YSpeed += owner.YSpeed;

        projectil.transform.position = transform.position;
        projectil.GetComponent<projectile>().damage = damage;
        projectil.GetComponent<projectile>().baseDamage = baseDamage;
        projectil.GetComponent<projectile>().criticalRatio = criticalRatio;
        projectil.GetComponent<projectile>().dexCriticalRatio = dexCriticalRatio;
        projectil.GetComponent<projectile>().initialEffectSize = initialEffectSize;
        projectil.GetComponent<projectile>().endEffectSize = endEffectSize;
        projectil.transform.localScale = new Vector3(size, size, 1);
        projectil.GetComponent<projectile>().CanFlipInitialEffect = CanFlipInitialEffect && flipX;
        projectil.GetComponent<projectile>().CanFlipEndEffect = CanFlipEndEffect && flipX;

        Instantiate(projectil);
    }
}
