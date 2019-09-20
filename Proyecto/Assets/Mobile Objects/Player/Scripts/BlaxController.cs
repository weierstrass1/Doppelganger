using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlaxAnimations
{
    public const int Idle = 0;
}
public class BlaxController : CharacterController
{
    #region Starts
    public override void characterStart()
    {
    }

    public override void componentStart()
    {
        animations[BlaxAnimations.Idle].startAnimation(0);
    }
    #endregion

    #region Common
    public override void characterUpdate()
    {
    }

    public override void componentUpdate()
    {
        animations[BlaxAnimations.Idle].excecuteAnimation();
    }

    public override void StartState(int state)
    {
    }
    #endregion

}
