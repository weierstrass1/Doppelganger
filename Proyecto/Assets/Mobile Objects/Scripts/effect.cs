using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : CharacterController
{
    bool haveInteractors;
    public int damage;
    public int baseDamage;
    public int criticalRatio;
    public int dexCriticalRatio;
    public ownerType OwnerType;
    public bool canHurt = false;

    public override void characterStart()
    {
    }

    public override void characterUpdate()
    {
    }

    public override void componentStart()
    {
        haveInteractors = component.interactors != null && component.interactors.Length > 0;
        animations[0].startAnimation(0);
        if(haveInteractors)
        {
            foreach (interactor inter in component.interactors)
            {
                if (OwnerType == ownerType.Ally)
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
        if (haveInteractors && component.interactors.Length > animations[0].currentIndex) 
        {
            component.interactors[animations[0].currentIndex].gameObject.SetActive(false);
        }

        animations[0].excecuteAnimationWithoutAnimReduction(60);

        if (haveInteractors && component.interactors.Length > animations[0].currentIndex)
        {
            component.interactors[animations[0].currentIndex].gameObject.SetActive(true);
        }

        if (component.graphicComponents[0].GetComponent<SpriteRenderer>().sprite == null)
        {
            Destroy(gameObject);
        }
    }

    void TriggerEnter(Collider2D box)
    {
        BaseCharacter bc = box.gameObject.GetComponent<BaseCharacter>();
        if (canHurt && bc != null)
        {
            bc.DealDamage(damage, baseDamage, dexCriticalRatio, criticalRatio, bc);
        }
    }

    public override void StartState(int state)
    {
    }
}
