using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlaxAnimations
{
    public const int Idle = 0;
    public const int Walking = 1;
}
public class BlaxController : CharacterController
{
    #region Starts
    public override void characterStart()
    {
        character = GetComponent<BaseCharacter>();
    }

    public override void componentStart()
    {
        animations[BlaxAnimations.Walking].startAnimation(0);
    }
    #endregion

    #region Common
    public override void characterUpdate()
    {
    }

    public override void componentUpdate()
    {
        animations[BlaxAnimations.Walking].excecuteAnimation();
        character.checkContactPoints(character.components);
        character.applySpeed(true, false, true, true, true);
    }

    public override void StartState(int state)
    {
    }
    #endregion

}
