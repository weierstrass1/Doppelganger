using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ownerType { Ally, Enemy};
public class projectile : CharacterController
{
    public GameObject initialEffect, endEffect;
    public int damage;
    public int baseDamage;
    public int criticalRatio;
    public int dexCriticalRatio;
    public ownerType OwnerType;
    public bool gravity;
    public bool friction;
    public bool acceleration;
    public bool minSpeed;
    public bool maxSpeed;
    public int initialEffectSize = 1, endEffectSize = 1;
    public bool CanFlipInitialEffect = false;
    public bool CanFlipEndEffect = false;
    public bool canHurt = false;

    public override void characterStart()
    {
    }

    public override void characterUpdate()
    {
    }

    public override void componentStart()
    {
        if(initialEffect!=null)
        {
            initialEffect.transform.localScale = new Vector3(initialEffectSize, initialEffectSize, 1);
            bool ieFX = initialEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX;
            if (CanFlipInitialEffect) initialEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX = !ieFX;
            Instantiate(initialEffect, transform.position, Quaternion.identity);
            initialEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX = ieFX;
        }
        if (component.interactors != null && component.interactors.Length > 0)
        {
            foreach(interactor inter in component.interactors)
            {
                if(OwnerType == ownerType.Ally)
                {
                    inter.gameObject.layer = AvailablesLayers.Ally;
                }
                else
                {
                    inter.gameObject.layer = AvailablesLayers.Enemy;
                }
                inter.TriggerEnter = TriggerEnter;
            }
        }
    }

    public override void componentUpdate()
    {
        if (character.BlockedFromBelow || 
            character.BlockedFromAbove || 
            character.BlockedFromLeft || 
            character.BlockedFromRight)
        {
            Hit();
        }
        else
        {
            character.applySpeed(gravity, friction, acceleration, minSpeed, maxSpeed);
            character.checkContactPoints(component);
        }
    }

    void TriggerEnter(Collider2D box)
    {
        BaseCharacter bc =  box.gameObject.GetComponent<BaseCharacter>();
        if (canHurt && bc != null)
        {
            bc.DealDamage(damage, baseDamage, dexCriticalRatio, criticalRatio, bc);
        }
        Hit();
    }

    void Hit()
    {
        if (endEffect != null)
        {
            bool eeFX = endEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX;
            if (CanFlipEndEffect) endEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX = !eeFX;
            endEffect.transform.localScale = new Vector3(endEffectSize, endEffectSize, 1);
            endEffect.GetComponent<effect>().OwnerType = OwnerType;
            endEffect.transform.GetChild(0).GetComponent<charComponent>().FlipX = eeFX;
            endEffect.GetComponent<effect>().damage = damage;
            endEffect.GetComponent<effect>().baseDamage = baseDamage;
            endEffect.GetComponent<effect>().criticalRatio = criticalRatio;
            endEffect.GetComponent<effect>().dexCriticalRatio = dexCriticalRatio;
            Instantiate(endEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public override void StartState(int state)
    {
    }
}
