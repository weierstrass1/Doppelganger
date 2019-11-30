using System;
using UnityEngine;

public class metballaLegStates
{
    public const int Idle = 0;
    public const int StandUp = 1;
    public const int LegsOut = 2;
    public const int NoLegs = 3;
}

public class metballStates
{
    public const int Idle = 0;
    public const int Move = 1;
    public const int Break = 2;
    public const int BreakExit = 3;
    public const int Flip = 4;
    public const int StartCanon = 5;
    public const int Canon = 6;
    public const int CloseCanon = 7;
    public const int StartLaser = 8;
    public const int Laser = 9;
    public const int CloseLaser = 10;
}

public class metballAnimations
{
    public const int Idle = 0;
    public const int SummonLaser = 1;
    public const int ShootLaser = 2;
    public const int SaveLaser = 3;
    public const int Rotation = 4;
    public const int InverseRotation = 5;
    public const int Floor = 6;
    public const int StandUp = 7;
    public const int OpenCanon = 8;
    public const int ShootCanon = 9;
    public const int CloseCanon = 10;
    public const int Flip = 11;
    public const int BreakStart = 12;
    public const int BreakSpark = 13;
    public const int BreakEnd = 14;
}

public class metballBodyZones
{
    public const int Body = 0;
    public const int FrontLeg = 1;
    public const int Canon = 2;
    public const int Turbine = 3;
    public const int Laser = 4;
    public const int BackLeg = 5;
    public const int Spark = 6;
}

public class metballSkills
{
    public const int Movement = 0;
    public const int BreakUpgrade = 1;
    public const int BuffaloManauver = 2;
    public const int Jumping = 3;
    public const int IncreaseAgi = 4;
    public const int PowerImpact = 5;
    public const int LaserX9RA = 6;
    public const int EnergyEfficiency = 7;
    public const int X9RA20 = 8;
    public const int Canon = 9;
    public const int GreatExplosion = 10;
    public const int Movementra = 11;
    public const int AutoAimLaser = 12;
    public const int AutoAimCanon = 13;
}

public class metballControls
{
    public const int MovesLeft = 0;
    public const int MovesRight = 1;
    public const int Breaks = 2;
    public const int LegsIn = 3;
    public const int Jump = 4;
    public const int ModeChange1 = 5;
    public const int ModeChange2 = 6;
    public const int Canon = 7;
    public const int CanonAutoAim = 8;
    public const int Laser = 9;
    public const int LaserAutoAim = 10;
    public const int LegsOut1 = 11;
    public const int LegsOut2 = 12;
    public const int BuffaloManauverRight = 13;
    public const int BuffaloManauverLeft = 14;
    public const int OnLegsMoveInAirLeft = 15;
    public const int OnLegsMoveInAirRight = 16;
    public const int OnLegsCanon = 17;
    public const int OnLegsCanonAutoAim = 18;
    public const int OnLegsLaser = 19;
    public const int OnLegsLaserAutoAim = 20;
    public const int JumpDown = 21;
    public const int JumpUp = 22;
}

public class metballController : CharacterController
{
    public float angle = 0;
    public float rotationSpeed = 0;
    public float rotationAccel = 0;
    public float rotationMaxSpeed = 0;
    public float sparkSpeed = 0;
    public float breakForce = 0;
    public float rotationMinXSpeed = 0;
    public float realRotationMinXSpeed = 0;
    public float rotationAccelSet = 0;
    public float jumpForce = 2;
    public float MaxNoControlTimer = 0;
    float noControlTimer = 0;
    public bool jumping = false;
    protected bool canExitJumping = true;
    public float FlyXSpeed = 0;
    public float breakingFlyForce = 0;
    public bool breakFlag = false;
    public float setGravity = 0;
    public float minBounceSpeed = 0;
    public float rotationLimitSpeed = 0;
    public float defaultCanonForce = 0;
    public float restoreAngleSpeed = 600;
    public float maxCanonTimer = 0;
    public float canonTimer = 0;
    public float maxLaserTimer = 0;
    public float LaserTimer = 0;
    public bool standUp = false;
    public int legState = 0;
    public int LegState
    {
        get
        {
            return legState;
        }
        set
        {
            StartLegState(value);
        }
    }
    public float shootLastUse = 0;
    public float MaxAirMoveUse = 0.5f;

    #region Starts

    public override void characterStart()
    {
        Versions[0] = true;
        Versions[1] = true;
        Versions[2] = true;
        Versions[3] = true;
        controls = new ButtonGetter[23];

        controls[metballControls.MovesLeft] = new ButtonGetter("Move Left");
        controls[metballControls.MovesLeft].addButton(defaultButtons.left, buttonState.Press);

        controls[metballControls.MovesRight] = new ButtonGetter("Move Right");
        controls[metballControls.MovesRight].addButton(defaultButtons.right, buttonState.Press);

        controls[metballControls.Breaks] = new ButtonGetter("Break");
        controls[metballControls.Breaks].addButton(defaultButtons.action2, buttonState.Press);

        controls[metballControls.LegsIn] = new ButtonGetter("Legs In");
        controls[metballControls.LegsIn].addButton(defaultButtons.action3, buttonState.Press);

        controls[metballControls.Jump] = new ButtonGetter("Jump");
        controls[metballControls.Jump].addButton(defaultButtons.action1, buttonState.Press);

        controls[metballControls.ModeChange1] = new ButtonGetter("Mode Change 1");
        controls[metballControls.ModeChange1].addButton(defaultButtons.up, buttonState.Down);

        controls[metballControls.ModeChange2] = new ButtonGetter("Mode Change 2");
        controls[metballControls.ModeChange2].addButton(defaultButtons.down, buttonState.Down);

        controls[metballControls.Canon] = new ButtonGetter("Canon");
        controls[metballControls.Canon].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.CanonAutoAim] = new ButtonGetter("Canon Auto Aim");
        controls[metballControls.CanonAutoAim].addButton(defaultButtons.action2, buttonState.Down);
        controls[metballControls.CanonAutoAim].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.Laser] = new ButtonGetter("Laser");
        controls[metballControls.Laser].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.LaserAutoAim] = new ButtonGetter("Laser Auto Aim");
        controls[metballControls.LaserAutoAim].addButton(defaultButtons.action2, buttonState.Down);
        controls[metballControls.LaserAutoAim].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.LegsOut1] = new ButtonGetter("Legs Out 1");
        controls[metballControls.LegsOut1].addButton(defaultButtons.left, buttonState.Press);

        controls[metballControls.LegsOut2] = new ButtonGetter("Legs Out 2");
        controls[metballControls.LegsOut2].addButton(defaultButtons.right, buttonState.Press);

        controls[metballControls.BuffaloManauverLeft] = new ButtonGetter("Buffalo Manauver Left");
        controls[metballControls.BuffaloManauverLeft].addButton(defaultButtons.left, buttonState.Press);
        controls[metballControls.BuffaloManauverLeft].addButton(defaultButtons.action3, buttonState.Press);

        controls[metballControls.BuffaloManauverRight] = new ButtonGetter("Buffalo Manauver Right");
        controls[metballControls.BuffaloManauverRight].addButton(defaultButtons.right, buttonState.Press);
        controls[metballControls.BuffaloManauverRight].addButton(defaultButtons.action3, buttonState.Press);

        controls[metballControls.OnLegsMoveInAirLeft] = new ButtonGetter("On Legs Move In Air Left");
        controls[metballControls.OnLegsMoveInAirLeft].addButton(defaultButtons.left, buttonState.Press);
        controls[metballControls.OnLegsMoveInAirLeft].addButton(defaultButtons.action1, buttonState.Press);

        controls[metballControls.OnLegsMoveInAirRight] = new ButtonGetter("On Legs Move In Air Right");
        controls[metballControls.OnLegsMoveInAirRight].addButton(defaultButtons.right, buttonState.Press);
        controls[metballControls.OnLegsMoveInAirRight].addButton(defaultButtons.action1, buttonState.Press);


        controls[metballControls.OnLegsCanon] = new ButtonGetter("On Legs Canon");
        controls[metballControls.OnLegsCanon].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.OnLegsCanonAutoAim] = new ButtonGetter("On Legs Canon Auto Aim");
        controls[metballControls.OnLegsCanonAutoAim].addButton(defaultButtons.action2, buttonState.Down);

        controls[metballControls.OnLegsLaser] = new ButtonGetter("On Legs Laser");
        controls[metballControls.OnLegsLaser].addButton(defaultButtons.action4, buttonState.Down);

        controls[metballControls.OnLegsLaserAutoAim] = new ButtonGetter("On Legs Laser Auto Aim");
        controls[metballControls.OnLegsLaserAutoAim].addButton(defaultButtons.action2, buttonState.Down);

        controls[metballControls.JumpDown] = new ButtonGetter("Jump Down");
        controls[metballControls.JumpDown].addButton(defaultButtons.action1, buttonState.Down);

        controls[metballControls.JumpUp] = new ButtonGetter("Jump Down");
        controls[metballControls.JumpUp].addButton(defaultButtons.action1, buttonState.Up);
    }

    public override void componentStart()
    {
        Versions = new bool[] { false, false, false, false };
        character.YGravity = setGravity;
        character.BlockedFromBelow = true;

        animations[metballAnimations.Idle].StartAction[0] = move7Up;
        animations[metballAnimations.Idle].EndAction[0] = move7Down;
        animations[metballAnimations.Idle].StartAction[1] = move8Up;
        animations[metballAnimations.Idle].EndAction[1] = move8Down;
        animations[metballAnimations.Idle].StartAction[2] = move7Up;
        animations[metballAnimations.Idle].EndAction[2] = move7Down;
        animations[metballAnimations.Idle].StartAction[3] = move6Up;
        animations[metballAnimations.Idle].EndAction[3] = move6Down;

        animations[metballAnimations.Floor].StartAction[0] = move8Up;
        animations[metballAnimations.Floor].EndAction[0] = move8Down;
        animations[metballAnimations.Floor].StartAction[1] = move7Up;
        animations[metballAnimations.Floor].EndAction[1] = move7Down;
        animations[metballAnimations.Floor].StartAction[2] = move6Up;
        animations[metballAnimations.Floor].EndAction[2] = move6Down;
        animations[metballAnimations.Floor].StartAction[3] = move3Up;
        animations[metballAnimations.Floor].EndAction[3] = move3Down;

        animations[metballAnimations.StandUp].StartAction[7] = move3Up;
        animations[metballAnimations.StandUp].EndAction[7] = move3Down;
        animations[metballAnimations.StandUp].StartAction[8] = move6Up;
        animations[metballAnimations.StandUp].EndAction[8] = move6Down;

        animations[metballAnimations.Flip].StartAction[4] = flipX;

        //Movement
        skills = new skill[14];

        skills[0] = new skill();

        skill saux = skills[0];

        saux.Index = SkillTexts.metballSkill0;

        saux.owner = character;

        saux.DescriptionLevelFunction = movementLevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 5;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 0, 5, 10, 15, 25 });
        saux.getExperience = new int[]{ 0, 5, 5, 8, 8 };

        saux.Unlock();

        //Break Upgrade
        skills[1] = new skill();

        saux = skills[1];

        saux.Index = SkillTexts.metballSkill1;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill1LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 2;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 10, 20 });
        saux.getExperience = new int[] { 5, 10 };

        skills[0].addChild(saux, 3);

        //Buffalo Manauver
        skills[2] = new skill();

        saux = skills[2];

        saux.Index = SkillTexts.metballSkill2;

        saux.Restrictions = bronzeRestriction;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill2LevelDescription;

        saux.property = properties.physic;

        saux.type = skillType.active;

        saux.TargetType = targetType.PhysicalContact;

        saux.maxLevel = 5;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getEnergy = new int[] { 1, 1, 1, 1, 1 };
        saux.getBaseDamage = new int[] { 20, 40, 60, 100, 150 };
        saux.getExperience = new int[] { 10, 10, 20, 20, 30 };

        skills[0].addChild(saux, 5);

        //Jumping
        skills[3] = new skill();

        saux = skills[3];

        saux.Index = SkillTexts.metballSkill3;

        saux.Restrictions = bronzeRestriction;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill3LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.active;

        saux.TargetType = targetType.None;

        saux.maxLevel = 4;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getEnergy = new int[] { 4, 4, 4, 4 };
        saux.addGetOthers(new int[] { 10, 20, 30, 50 });
        saux.getExperience = new int[] { 5, 5, 5, 5 };

        skills[1].addChild(saux, 2);

        //Increase Agi
        skills[4] = new skill();

        saux = skills[4];

        saux.Index = SkillTexts.metballSkill4;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill4LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 3;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 10, 20, 30 });
        saux.getExperience = new int[] { 10, 10, 10 };

        skills[3].addChild(saux, 2);

        //Power Impact
        skills[5] = new skill();

        saux = skills[5];

        saux.Index = SkillTexts.metballSkill5;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill5LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 3;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 2, 4, 6 });
        saux.getExperience = new int[] { 10, 15, 20 };

        skills[2].addChild(saux, 5);

        //Laser X9RA
        skills[6] = new skill();

        saux = skills[6];

        saux.Index = SkillTexts.metballSkill6;

        saux.Restrictions = silverRestriction;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill6LevelDescription;

        saux.property = properties.plasma;

        saux.type = skillType.active;

        saux.TargetType = targetType.SingleTarget;

        saux.maxLevel = 5;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getEnergy = new int[] { 2, 2, 2, 2, 2 };
        saux.getBaseDamage = new int[] { 5, 15, 25, 35, 50 };
        saux.getExperience = new int[] { 4, 5, 6, 7, 8 };

        //Energy Efficiency	
        skills[7] = new skill();

        saux = skills[7];

        saux.Index = SkillTexts.metballSkill7;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill7LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 5;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 5, 10, 15, 20, 25 });
        saux.addGetOthers(new int[] { 10, 20, 30, 40, 50 });
        saux.getExperience = new int[] { 5, 6, 7, 8, 10 };

        skills[6].addChild(saux, 2);

        //X9RA 2.0
        skills[8] = new skill();

        saux = skills[8];

        saux.Index = SkillTexts.metballSkill8;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill8LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 10;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 120, 160 });
        saux.getExperience = new int[] { 3, 3, 8, 8, 10, 10, 15, 15, 30, 30 };

        skills[6].addChild(saux, 5);

        //Canon
        skills[9] = new skill();

        saux = skills[9];

        saux.Index = SkillTexts.metballSkill9;

        saux.Restrictions = silverRestriction;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill9LevelDescription;

        saux.property = properties.explosion;

        saux.type = skillType.active;

        saux.TargetType = targetType.AOEANDST;

        saux.maxLevel = 10;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getBaseDamage = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        saux.getExperience = new int[] { 5, 5, 6, 6, 7, 7, 8, 8, 10, 10 };

        //Great Explosion
        skills[10] = new skill();

        saux = skills[10];

        saux.Index = SkillTexts.metballSkill10;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill10LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 5;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 20, 40, 80, 120, 160 });
        saux.getExperience = new int[] { 6, 8, 8, 10, 10 };

        skills[9].addChild(saux, 3);

        //Movementra
        skills[11] = new skill();

        saux = skills[11];

        saux.Index = SkillTexts.metballSkill11;

        saux.Restrictions = AllRestriction;

        saux.owner = character;

        saux.DescriptionLevelFunction = skill11LevelDescription;

        saux.property = properties.none;

        saux.type = skillType.pasive;

        saux.TargetType = targetType.None;

        saux.maxLevel = 1;

        saux.recipient = skillRecipient.currentTransformation;

        saux.addGetOthers(new int[] { 40 });
        saux.getExperience = new int[] { 50 };

        skills[5].addChild(saux, 3);

        //Auto-Aim: Laser
        skills[12] = new skill();

        saux = skills[12];

        saux.Index = SkillTexts.metballSkill12;

        saux.Restrictions = AllRestriction;

        saux.owner = character;

        saux.property = properties.none;

        saux.type = skillType.active;

        saux.TargetType = targetType.SingleTarget;

        saux.maxLevel = 1;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getExperience = new int[] { 50 };

        skills[7].addChild(saux, 5);
        skills[8].addChild(saux, 10);

        //Auto-Aim: Canon
        skills[13] = new skill();

        saux = skills[13];

        saux.Index = SkillTexts.metballSkill13;

        saux.Restrictions = AllRestriction;

        saux.owner = character;

        saux.property = properties.none;

        saux.type = skillType.active;

        saux.TargetType = targetType.SingleTarget;

        saux.maxLevel = 1;

        saux.recipient = skillRecipient.currentTransformation;

        saux.getExperience = new int[] { 50 };

        skills[9].addChild(saux, 10);
        skills[10].addChild(saux, 5);
    }

    #endregion

    #region Common

    public override void characterUpdate()
    {
        bool dontCanon = skills[metballSkills.Canon].Locked;
        bool dontLaser = skills[metballSkills.LaserX9RA].Locked;
        bool dontAim = skills[metballSkills.AutoAimCanon].Locked && skills[metballSkills.AutoAimLaser].Locked;
        bool useMode = UseMode && !dontCanon && !dontLaser;

        if (useMode)
        {
            if (controls[metballControls.ModeChange1].isUsing || controls[metballControls.ModeChange2].isUsing)
            {
                if (Mode == 0) Mode = 1;
                else Mode = 0;
            }
        }

        if (bodyZones[metballBodyZones.FrontLeg].currentFrame > 5 && bodyZones[metballBodyZones.FrontLeg].currentFrame < 12)
        {
            bool dontBuffalo = skills[metballSkills.BuffaloManauver].Locked;
            bool dontJumping = skills[metballSkills.Jumping].Locked;
            bool dontLegsOut = false;
            bool dontWeapon = false;

            if(!dontBuffalo && controls[metballControls.BuffaloManauverLeft].isUsing && !controls[metballControls.BuffaloManauverRight].isUsing)
            {
                dontLegsOut = true;
                dontWeapon = true;
                if (State == metballStates.StartCanon || State == metballStates.Canon) closeCanon();
                if (State == metballStates.StartLaser || State == metballStates.Laser) closeLaser();
            }
            else if(!dontBuffalo && controls[metballControls.BuffaloManauverRight].isUsing && !controls[metballControls.BuffaloManauverLeft].isUsing)
            {
                dontLegsOut = true;
                dontWeapon = true;
                if (State == metballStates.StartCanon || State == metballStates.Canon) closeCanon();
                if (State == metballStates.StartLaser || State == metballStates.Laser) closeLaser();
            }

            if(!character.BlockedFromBelow && controls[metballControls.OnLegsMoveInAirLeft].isUsing && !controls[metballControls.OnLegsMoveInAirRight].isUsing)
            {
                dontLegsOut = true;
            }
            else if(!character.BlockedFromBelow && controls[metballControls.OnLegsMoveInAirRight].isUsing && !controls[metballControls.OnLegsMoveInAirLeft].isUsing)
            {
                dontLegsOut = true;
            }

            if(!dontJumping && character.BlockedFromBelow && controls[metballControls.Jump].isUsing)
            {
                dontLegsOut = true;
            }

            if (!dontLegsOut && controls[metballControls.LegsOut1].isUsing)
            {
                MoveLeft();
                noControlTimer = MaxNoControlTimer;
            }
            else if(!dontLegsOut && controls[metballControls.LegsOut2].isUsing)
            {
                MoveRight();
                noControlTimer = MaxNoControlTimer;
            }
            else if(dontLegsOut)
            {
                if (rotationAccel != 0)
                {
                    noControlTimer -= Time.deltaTime;
                    if (noControlTimer < 0)
                    {
                        noControlTimer = 0;
                        rotationAccel = 0;
                    }
                }
            }

            if(!dontAim && (controls[metballControls.OnLegsCanonAutoAim].isUsing || controls[metballControls.OnLegsLaserAutoAim].isUsing))
            {

            }

            if(!dontWeapon)
            {
                if(useMode)
                {
                    if(!dontCanon && Mode == 0 && controls[metballControls.OnLegsCanon].isUsing)
                    {
                        if (State == metballStates.StartLaser || 
                            State == metballStates.Laser ||
                            State == metballStates.CloseLaser) closeLaser();
                        else shootCanon();
                    }
                    else if(!dontLaser && Mode == 1 && controls[metballControls.OnLegsLaser].isUsing)
                    {
                        if (State == metballStates.StartCanon ||
                        State == metballStates.Canon ||
                        State == metballStates.CloseCanon) closeCanon();
                        else shootLaser();
                    }
                }
                else
                {
                    if (!dontLaser && controls[metballControls.OnLegsLaser].isUsing)
                    {
                        if (State == metballStates.StartCanon ||
                        State == metballStates.Canon ||
                        State == metballStates.CloseCanon) closeCanon();
                        else shootLaser();
                    }
                    else if (!dontCanon && controls[metballControls.OnLegsCanon].isUsing)
                    {
                        if (State == metballStates.StartLaser ||
                            State == metballStates.Laser ||
                            State == metballStates.CloseLaser) closeLaser();
                        else shootCanon();
                    }
                }
            }
        }
        else
        {
            shootLastUse += Time.deltaTime;
            if (controls[metballControls.LegsIn].isUsing && bodyZones[metballBodyZones.Body].currentFrame < 15 &&
                legState != metballaLegStates.StandUp)
            {
                LegState = metballaLegStates.StandUp;
            }
            else if(character.BlockedFromBelow && controls[metballControls.Breaks].isUsing)
            {
                if (character.XSpeed != 0)
                {
                    Breaking();
                }
                else if (controls[metballControls.MovesRight].isUsing && !controls[metballControls.MovesLeft].isUsing)
                {
                    BreakingRight();
                }
                else if (controls[metballControls.MovesLeft].isUsing && !controls[metballControls.MovesRight].isUsing)
                {
                    BreakingLeft();
                }
            }
            else if(controls[metballControls.MovesLeft].isUsing && !controls[metballControls.MovesRight].isUsing)
            {
                MoveLeft();
                noControlTimer = MaxNoControlTimer;
                if ((shootLastUse >= MaxAirMoveUse || character.BlockedFromBelow) && (State == metballStates.StartCanon || State == metballStates.Canon)) closeCanon();
                if ((shootLastUse >= MaxAirMoveUse || character.BlockedFromBelow) && (State == metballStates.StartLaser || State == metballStates.Laser)) closeLaser();
            }
            else if(controls[metballControls.MovesRight].isUsing && !controls[metballControls.MovesLeft].isUsing)
            {
                MoveRight();
                noControlTimer = MaxNoControlTimer;
                if ((shootLastUse >= MaxAirMoveUse || character.BlockedFromBelow) && (State == metballStates.StartCanon || State == metballStates.Canon)) closeCanon();
                if ((shootLastUse >= MaxAirMoveUse || character.BlockedFromBelow) && (State == metballStates.StartLaser || State == metballStates.Laser)) closeLaser();
            }
            else
            {
                if (rotationAccel != 0)
                {
                    noControlTimer -= Time.deltaTime;
                    if (noControlTimer < 0)
                    {
                        noControlTimer = 0;
                        rotationAccel = 0;
                    }
                }
            }

            if (controls[metballControls.JumpDown].isUsing)
            {
                JumpStart();
            }
            else if (controls[metballControls.JumpUp].isUsing)
            {
                canExitJumping = true;
            }
            else if(controls[metballControls.Jump].isUsing)
            {
                Jump();
            }

            if (!dontAim && (controls[metballControls.LaserAutoAim].isUsing || controls[metballControls.CanonAutoAim].isUsing))
            {

            }

            if (useMode)
            {
                if (!dontCanon && Mode == 0 && controls[metballControls.Canon].isUsing)
                {
                    shootLastUse = 0;
                    if (State == metballStates.StartLaser ||
                            State == metballStates.Laser ||
                            State == metballStates.CloseLaser) closeLaser();
                    else shootCanon();
                }
                else if (!dontLaser && Mode == 1 && controls[metballControls.Laser].isUsing)
                {
                    shootLastUse = 0;
                    if (State == metballStates.StartCanon ||
                        State == metballStates.Canon ||
                        State == metballStates.CloseCanon) closeCanon();
                    else shootLaser();
                }
            }
            else
            {
                if (!dontLaser && controls[metballControls.Laser].isUsing)
                {
                    shootLastUse = 0;
                    if (State == metballStates.StartCanon || 
                        State == metballStates.Canon || 
                        State == metballStates.CloseCanon) closeCanon();
                    else shootLaser();
                }
                else if (!dontCanon && controls[metballControls.Canon].isUsing)
                {
                    shootLastUse = 0;
                    if (State == metballStates.StartLaser ||
                            State == metballStates.Laser ||
                            State == metballStates.CloseLaser) closeLaser();
                    else shootCanon();
                }
            }
        }     
    }

    public override void componentUpdate()
    {
        realRotationMinXSpeed = rotationMinXSpeed;
        //Movement Effect
        if(!skills[metballSkills.Movement].Locked)
        {
            realRotationMinXSpeed *= (100f + skills[metballSkills.Movement].GetOthers(0));
            realRotationMinXSpeed /= 100f;
        }

        //Movementra Effect
        if(!skills[metballSkills.Movementra].Locked)
        {
            realRotationMinXSpeed *= (100f + skills[metballSkills.Movementra].GetOthers(0));
            realRotationMinXSpeed /= 100f;
        }

        switch(legState)
        {
            case metballaLegStates.Idle:
                animations[metballAnimations.Idle].excecuteAnimation();
                break;
            case metballaLegStates.StandUp:
                if (animations[metballAnimations.StandUp].FrameIsFinished(8))
                {
                    legState = metballStates.Idle;
                }
                else
                {
                    animations[metballAnimations.StandUp].excecuteAnimation();
                }
                break;
            case metballaLegStates.LegsOut:
                animations[metballAnimations.Floor].excecuteAnimation();
                if (bodyZones[metballBodyZones.FrontLeg].currentFrame == 22)
                {
                    legState = metballaLegStates.NoLegs;
                }
                break;
        }

        bool breaking = false;
        character.XFriction = 0;
        bool onFloor = false;

        if(character.BlockedFromBelow)
        {
            if (bodyZones[metballBodyZones.FrontLeg].currentFrame > 5 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame < 12)
            {
                breaksLess(out breaking);
            }
            else
            {
                onFloor = true;
            }
        }
        

        switch (State)
        {
            case metballStates.Idle:
                break;
            case metballStates.Move:
                if(legState==metballaLegStates.Idle)
                {
                    LegState = metballaLegStates.LegsOut;
                }
                break;
            case metballStates.Break:
                if (bodyZones[metballBodyZones.Body].currentFrame >= 12)
                {
                    animations[metballAnimations.BreakStart].excecuteAnimation();
                    rotationSpeed = 0;
                }
                else if (bodyZones[metballBodyZones.Body].currentFrame == 0)
                {
                    animations[metballAnimations.BreakStart].startAnimation(0);
                }
                else
                {
                    rotationAccel = 0;
                    if (rotationSpeed >= 0 && rotationSpeed < realRotationMinXSpeed) rotationSpeed = realRotationMinXSpeed;
                    if (rotationSpeed < 0 && rotationSpeed > -realRotationMinXSpeed) rotationSpeed = -realRotationMinXSpeed;
                }
                if (character.BlockedFromBelow)
                {
                    breaks(out breaking);
                }
                break;
            case metballStates.BreakExit:
                animations[metballAnimations.BreakEnd].excecuteAnimation();
                if(bodyZones[metballBodyZones.Body].currentFrame==0 && animations[metballAnimations.BreakEnd].isFinished())
                {
                    State = metballStates.Move;
                }
                if (character.BlockedFromBelow)
                {
                    breaks(out breaking);
                }
                break;
            case metballStates.Flip:
                animations[metballAnimations.Flip].excecuteAnimation();
                if(animations[metballAnimations.Flip].FrameIsFinished(8))
                {
                    State = 1;
                }
                break;
            case metballStates.StartCanon:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Body].currentFrame != 0)
                {
                    rotateToFrame0();
                    if(bodyZones[metballBodyZones.Body].currentFrame == 0)
                    {
                        animations[metballAnimations.OpenCanon].startAnimation(0);
                    }
                }
                else if(animations[metballAnimations.OpenCanon].FrameIsFinished(6))
                {
                    State = metballStates.Canon;
                    bodyZones[metballBodyZones.Canon].currentFrame = 8;
                }
                else
                {
                    animations[metballAnimations.OpenCanon].excecuteAnimation();
                }
                break;
            case metballStates.Canon:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Canon].currentFrame != 8)
                {
                    animations[metballAnimations.ShootCanon].excecuteAnimation();
                }
                else
                {
                    canonTimer -= Time.deltaTime;
                }
                break;
            case metballStates.CloseCanon:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Canon].currentFrame==15)
                {
                    State = 0;
                    speed2RotationSpeed();
                }
                else
                {
                    animations[metballAnimations.CloseCanon].excecuteAnimation();
                }
                break;
            case metballStates.StartLaser:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Body].currentFrame != 9)
                {
                    rotateToFrame9();
                    if (bodyZones[metballBodyZones.Body].currentFrame == 9)
                    {
                        animations[metballAnimations.SummonLaser].startAnimation(0);
                    }
                }
                else if (animations[metballAnimations.SummonLaser].FrameIsFinished(9))
                {
                    State = metballStates.Laser;
                }
                else
                {
                    animations[metballAnimations.SummonLaser].excecuteAnimation();
                }
                break;
            case metballStates.Laser:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Laser].currentFrame != 9)
                {
                    animations[metballAnimations.ShootLaser].excecuteAnimation();
                }
                else
                {
                    LaserTimer -= Time.deltaTime;
                }
                break;
            case metballStates.CloseLaser:
                if (onFloor)
                {
                    breaks(out breaking);
                }

                if (bodyZones[metballBodyZones.Laser].currentFrame == 21)
                {
                    State = 0;
                    speed2RotationSpeed();
                }
                else
                {
                    animations[metballAnimations.SaveLaser].excecuteAnimation();
                }
                break;
        }

        applyRotation();

        character.applySpeed(true, breaking, false, false, false);

        float lastXSpeed = character.XSpeed;
        float lastYSpeed = character.YSpeed;
        bool lastBlockedAngle = character.BlockedAngleDetector;


        bool lastBlockedFromSide = character.BlockedFromRight;
        if (lastXSpeed < 0) lastBlockedFromSide = character.BlockedFromLeft;

        character.checkContactPoints(component);

        bool newBlockedFromSide = character.BlockedFromRight;
        if (lastXSpeed < 0)
            newBlockedFromSide = character.BlockedFromLeft;

        if (!lastBlockedAngle && character.BlockedAngleDetector
            && character.TerrainAngle == 0 && !character.IgnoreBelowInteraction)
        {
            if (character.YSpeed < -minBounceSpeed &&
                !(bodyZones[metballBodyZones.FrontLeg].currentFrame > 5 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame < 12))
            {
                if ((!(bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame > 5)) &&
                    character.BlockedFromBelow)
                {
                    float spd = (float)(-(rotationSpeed * 2.0 * Math.PI * 16.0) / (360.0 * MobileObject.pixelPerMeter));
                    if (component.FlipX)
                        spd = -spd;

                    character.XSpeed = spd;
                }
                character.YSpeed = -lastYSpeed / 3;
                character.IgnoreBelowInteraction = true;
                character.BlockedFromBelow = false;
                character.BlockedAngleDetector = false;
            }
        }

        if(newBlockedFromSide && !lastBlockedFromSide)
        {
            rotationSpeed = -rotationSpeed;
            if (!character.BlockedFromBelow)
                character.XSpeed = -lastXSpeed;
        }

        if (canExitJumping && jumping && character.BlockedFromBelow) jumping = false;

        if (character.IgnoreBelowInteraction && (character.YSpeed <= 0 || !character.tryContactBelow(component))) character.IgnoreBelowInteraction = false;

        character.YGravity = setGravity;
    }

    public void BreakingLeft()
    {
        if (character.XSpeed != 0)
        {
            State = metballStates.Break;
        }
        else
        {
            if (component.FlipX && State != metballStates.BreakExit)
            {
                State = metballStates.BreakExit;
            }
            else if (!component.FlipX && State != metballStates.Flip)
            {
                State = metballStates.Flip;
            }
        }
        if (State == metballStates.Idle)
        {
            rotationSpeed = 0;
            rotationAccel = 0;
        }
    }

    public void BreakingRight()
    {
        if (character.XSpeed != 0)
        {
            State = metballStates.Break;
        }
        else
        {
            if (!component.FlipX && State != metballStates.BreakExit)
            {
                State = metballStates.BreakExit;
            }
            else if (component.FlipX && State != metballStates.Flip)
            {
                State = metballStates.Flip;
            }
        }
        if(State == metballStates.Idle)
        {
            rotationSpeed = 0;
            rotationAccel = 0;
        }
    }

    public void Breaking()
    {
        switch (State)
        {
            case metballStates.Idle:
                rotationSpeed = 0;
                rotationAccel = 0;
                break;
            case metballStates.Move:
                State = metballStates.Break;
                rotationAccel = 0;
                break;         
        }
    }

    public void MoveLeft()
    {
        if(character.BlockedFromBelow)
        {
            if(legState==metballaLegStates.Idle || legState == metballaLegStates.StandUp)
            {
                LegState = metballaLegStates.LegsOut;
            }
            switch (State)
            {
                case metballStates.Idle:
                    State = metballStates.Move;
                    break;
                case metballStates.Move:

                    if (component.FlipX)
                    {
                        if (rotationSpeed > -realRotationMinXSpeed && rotationSpeed <= 0)
                        {
                            rotationSpeed = -realRotationMinXSpeed;
                            rotationAccel = -rotationAccelSet;
                        }
                        else if (rotationSpeed > 0)
                        {
                            if (rotationSpeed < realRotationMinXSpeed)
                            {
                                rotationSpeed = -realRotationMinXSpeed;
                            }
                            else
                            {
                                State = metballStates.Break;
                            }
                        }

                    }
                    else
                    {
                        if (rotationSpeed < 0)
                        {
                            if (rotationSpeed > -realRotationMinXSpeed)
                            {
                                rotationSpeed = -realRotationMinXSpeed;
                            }
                            State = metballStates.Break;
                        }
                        else if (bodyZones[metballBodyZones.Body].currentFrame == 0 || bodyZones[metballBodyZones.Body].currentFrame == 14) 
                        {
                            State = 4;
                        }
                        else
                        {
                            if (rotationSpeed < realRotationMinXSpeed)
                            {
                                rotationSpeed = realRotationMinXSpeed;
                                rotationAccel = rotationAccelSet;
                            }
                        }
                    }
                    break;
                case metballStates.Break:
                    if (bodyZones[metballBodyZones.Body].currentFrame == 18)
                    {
                        if (animations[metballAnimations.BreakStart].isFinished())
                        {
                            if (!component.FlipX) component.FlipX = true;
                            State = metballStates.BreakExit;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            rotationAccel = -rotationAccelSet * 1.5f;
            if (!component.FlipX) rotationAccel = -rotationAccel;
            if (Mathf.Abs(character.XSpeed) < FlyXSpeed)
            {
                character.XSpeed = -FlyXSpeed;
            }
            else if (character.XSpeed > FlyXSpeed)
            {
                character.XSpeed -= breakingFlyForce * Time.deltaTime;
            }
        }
    }

    public void MoveRight()
    {
        if (character.BlockedFromBelow)
        {
            if (legState == metballaLegStates.Idle || legState == metballaLegStates.StandUp)
            {
                LegState = metballaLegStates.LegsOut;
            }
            switch (State)
            {
                case metballStates.Idle:
                    State = metballStates.Move;
                    break;
                case metballStates.Move:
                    if (!component.FlipX)
                    {
                        if (rotationSpeed > -realRotationMinXSpeed && rotationSpeed <= 0)
                        {
                            rotationSpeed = -realRotationMinXSpeed;
                            rotationAccel = -rotationAccelSet;
                        }
                        else if (rotationSpeed > 0)
                        {
                            if (rotationSpeed < realRotationMinXSpeed)
                            {
                                rotationSpeed = -realRotationMinXSpeed;
                            }
                            else
                            {
                                State = metballStates.Break;
                            }
                        }
                    }
                    else
                    {
                        if (rotationSpeed < 0)
                        {
                            if (rotationSpeed > -realRotationMinXSpeed)
                            {
                                rotationSpeed = -realRotationMinXSpeed;
                            }
                            State = metballStates.Break;
                        }
                        else if (bodyZones[metballBodyZones.Body].currentFrame == 0 || bodyZones[metballBodyZones.Body].currentFrame == 14)
                        {
                            State = 4;
                        }
                        else
                        {
                            if (rotationSpeed < realRotationMinXSpeed)
                            {
                                rotationSpeed = realRotationMinXSpeed;
                                rotationAccel = rotationAccelSet;
                            }
                        }
                    }
                    break;
                case metballStates.Break:
                    if (bodyZones[metballBodyZones.Body].currentFrame == 18)
                    {
                        if (animations[metballAnimations.BreakStart].isFinished())
                        {
                            if (component.FlipX) component.FlipX = false;
                            State = metballStates.BreakExit;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            rotationAccel = -rotationAccelSet * 1.5f;
            if (component.FlipX) rotationAccel = -rotationAccel;
            if (Mathf.Abs(character.XSpeed) < FlyXSpeed)
            {
                character.XSpeed = FlyXSpeed;
            }
            else if (character.XSpeed < -FlyXSpeed)
            {
                character.XSpeed += breakingFlyForce * Time.deltaTime;
            }
        }
    }

    public void JumpStart()
    {
        if (bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
            bodyZones[metballBodyZones.FrontLeg].currentFrame > 5) return;

        if (!jumping && character.BlockedFromBelow && !character.IgnoreBelowInteraction)
        {
            jumping = true;
            canExitJumping = false;
            Vector2 v = MobileObject.Rotate(character.TerrainAngle, 0, jumpForce);
            Vector2 v2 = MobileObject.Rotate(character.TerrainAngle, character.XSpeed, character.YSpeed);
            character.XSpeed = v2.x + v.x;
            character.YSpeed += v2.y + v.y;
            character.IgnoreBelowInteraction = true;
            character.BlockedFromBelow = false;
            character.BlockedAngleDetector = false;
        }
    }

    public void Jump()
    {
        if (character.YSpeed > 0 && jumping && character.BlockedFromBelow) return;
        if (bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame > 5) return;

        character.YGravity = setGravity * 0.6f;
    }

    public void shootCanon()
    {
        if (skills[metballSkills.Canon].Locked) return;
        if (bodyZones[metballBodyZones.Body].currentFrame > 14) return;

        if (State != metballStates.Canon)
        {
            State = metballStates.StartCanon;
        }
        else if (bodyZones[metballBodyZones.Canon].currentFrame == 8 && canonTimer <= 0)
        {
            animations[metballAnimations.ShootCanon].startAnimation(1);
            canonTimer = maxCanonTimer * character.getAnimationReduction();

            component.projectileLaunchers[0].damage = character.getRawDamage();
            component.projectileLaunchers[0].baseDamage = skills[metballSkills.Canon].GetBaseDamage();
            component.projectileLaunchers[0].dexCriticalRatio = character.getDexCriticalRatio();
            component.projectileLaunchers[0].criticalRatio = 0;

            if (!skills[metballSkills.GreatExplosion].Locked)
            {
                component.projectileLaunchers[0].endEffectSize = 2;
                component.projectileLaunchers[0].baseDamage *= (100 + skills[metballSkills.GreatExplosion].GetOthers(0));
                component.projectileLaunchers[0].baseDamage /= 100;
            }
            shoot();
        }
    }

    public void shootLaser()
    {
        if (skills[metballSkills.LaserX9RA].Locked) return;
        if (bodyZones[metballBodyZones.Body].currentFrame > 14) return;

        if (State != metballStates.Laser)
        {
            State = metballStates.StartLaser;
        }
        else if (bodyZones[metballBodyZones.Laser].currentFrame == 9 && LaserTimer <= 0)
        {
            animations[metballAnimations.ShootLaser].startAnimation(0);
            LaserTimer = maxLaserTimer * character.getAnimationReduction();
            if(!skills[metballSkills.EnergyEfficiency].Locked)
            {
                LaserTimer *= (100 - skills[metballSkills.EnergyEfficiency].GetOthers(1));
                LaserTimer /= 100;
            }
            
            component.projectileLaunchers[1].damage = character.getRawDamage();
            component.projectileLaunchers[1].baseDamage = skills[metballSkills.LaserX9RA].GetBaseDamage();
            component.projectileLaunchers[1].dexCriticalRatio = character.getDexCriticalRatio();
            component.projectileLaunchers[1].criticalRatio = 0;

            if (!skills[metballSkills.X9RA20].Locked)
            {
                component.projectileLaunchers[0].baseDamage *= (100 + skills[metballSkills.X9RA20].GetOthers(0));
                component.projectileLaunchers[0].baseDamage /= 100;
            }
            shoot(1);
        }
    }

    public void closeCanon()
    {
        State = metballStates.CloseCanon;
    }

    public void closeLaser()
    {
        State = metballStates.CloseLaser;
    }

    public void StartLegState(int state)
    {
        restoreYDisp();

        int prevState = legState;
        legState = state;

        switch(state)
        {
            case metballaLegStates.StandUp:
                if (bodyZones[metballBodyZones.Body].currentFrame > 11 && bodyZones[metballBodyZones.Body].currentFrame != 14)
                {
                    switch (bodyZones[metballBodyZones.Body].currentFrame)
                    {
                        case 0:
                            animations[metballAnimations.BreakEnd].startAnimation(4);
                            break;
                        case 14:
                            animations[metballAnimations.BreakEnd].startAnimation(4);
                            break;
                        case 15:
                            animations[metballAnimations.BreakEnd].startAnimation(3);
                            break;
                        case 16:
                            animations[metballAnimations.BreakEnd].startAnimation(2);
                            break;
                        case 17:
                            animations[metballAnimations.BreakEnd].startAnimation(1);
                            break;
                        case 18:
                            animations[metballAnimations.BreakEnd].startAnimation(0);
                            break;
                        default:
                            animations[metballAnimations.BreakEnd].startAnimation(0);
                            break;
                    }
                    State = metballStates.BreakExit;
                }
                else if (bodyZones[metballBodyZones.FrontLeg].currentFrame <= 7)
                {
                    animations[metballAnimations.StandUp].startAnimation(bodyZones[metballBodyZones.FrontLeg].currentFrame);
                    if (State == metballStates.Move) State = metballStates.Idle;
                }
                else if (bodyZones[metballBodyZones.FrontLeg].currentFrame == 22)
                {
                    animations[metballAnimations.StandUp].startAnimation(0);
                    if (State == metballStates.Move) State = metballStates.Idle;
                }
                else
                {
                    legState = prevState;
                }
                break;
            case metballaLegStates.LegsOut:
                floorStart();
                break;
        }
    }

    public void restoreYDisp()
    {
        if (component.contactDown[2].localPosition.y < 0)
        {
            if (character.BlockedFromBelow)
            {
                character.y += component.contactDown[2].localPosition.y - 1;
            }

            component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
                0,
                component.contactDown[2].localPosition.z);
            component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
                0,
                component.contactDown[3].localPosition.z);
            component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
                0,
                component.angleDetector[1].localPosition.z);

            component.contactDown[2].gameObject.SetActive(false);
            component.contactDown[3].gameObject.SetActive(false);
            component.angleDetector[1].gameObject.SetActive(false);
            component.angleDetector[0].gameObject.SetActive(true);
        }
    }

    public override void StartState(int state)
    {
        int prevState = State;
        character.state = state;

        switch(State)
        {
            case metballStates.Move:
                if (bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame > 5)
                {
                    LegState = metballaLegStates.LegsOut;
                    state = prevState;
                }
                break;
            case metballStates.Break:
                if (bodyZones[metballBodyZones.FrontLeg].currentFrame != 22) 
                {
                    character.state = prevState;
                }
                else if(bodyZones[metballBodyZones.Body].currentFrame == 0)
                {
                    animations[metballAnimations.BreakStart].startAnimation(0);
                    character.XFriction = breakForce;
                    rotationSpeed = 0;
                }
                else
                {
                    rotationAccel = 0;
                    if (rotationSpeed >= 0 && rotationSpeed < realRotationMinXSpeed) rotationSpeed = realRotationMinXSpeed;
                    if (rotationSpeed < 0 && rotationSpeed > -realRotationMinXSpeed) rotationSpeed = -realRotationMinXSpeed;
                    character.XFriction = breakForce;
                }
                break;
            case metballStates.BreakExit:
                if (bodyZones[metballBodyZones.FrontLeg].currentFrame != 22)
                {
                    character.state = prevState;
                }
                else
                {
                    switch (bodyZones[metballBodyZones.Body].currentFrame)
                    {
                        case 0:
                            animations[metballAnimations.BreakEnd].startAnimation(4);
                            break;
                        case 14:
                            animations[metballAnimations.BreakEnd].startAnimation(4);
                            break;
                        case 15:
                            animations[metballAnimations.BreakEnd].startAnimation(3);
                            break;
                        case 16:
                            animations[metballAnimations.BreakEnd].startAnimation(2);
                            break;
                        case 17:
                            animations[metballAnimations.BreakEnd].startAnimation(1);
                            break;
                        case 18:
                            animations[metballAnimations.BreakEnd].startAnimation(0);
                            break;
                        default:
                            animations[metballAnimations.BreakEnd].startAnimation(0);
                            break;
                    }
                } 
                break;
            case metballStates.Flip:
                if (bodyZones[metballBodyZones.FrontLeg].currentFrame != 22)
                {
                    character.state = prevState;
                }
                else
                {
                    switch (bodyZones[metballBodyZones.Body].currentFrame)
                    {
                        case 0:
                            animations[metballAnimations.Flip].startAnimation(0);
                            break;
                        case 14:
                            animations[metballAnimations.Flip].startAnimation(0);
                            break;
                        case 15:
                            animations[metballAnimations.Flip].startAnimation(1);
                            break;
                        case 16:
                            animations[metballAnimations.Flip].startAnimation(2);
                            break;
                        case 17:
                            animations[metballAnimations.Flip].startAnimation(3);
                            break;
                        case 18:
                            animations[metballAnimations.Flip].startAnimation(4);
                            break;
                        default:
                            character.state = prevState;
                            break;
                    }
                }
                break;
            case metballStates.StartCanon:
                switch (bodyZones[metballBodyZones.Canon].currentFrame)
                {
                    case 0:
                        animations[metballAnimations.OpenCanon].startAnimation(0);
                        break;
                    case 1:
                        animations[metballAnimations.OpenCanon].startAnimation(1);
                        break;
                    case 2:
                        animations[metballAnimations.OpenCanon].startAnimation(2);
                        break;
                    case 3:
                        animations[metballAnimations.OpenCanon].startAnimation(3);
                        break;
                    case 4:
                        animations[metballAnimations.OpenCanon].startAnimation(4);
                        break;
                    case 5:
                        animations[metballAnimations.OpenCanon].startAnimation(5);
                        break;
                    case 6:
                        animations[metballAnimations.OpenCanon].startAnimation(6);
                        break;
                    case 7:
                        animations[metballAnimations.OpenCanon].startAnimation(4);
                        break;
                    case 8:
                        animations[metballAnimations.OpenCanon].startAnimation(6);
                        break;
                    case 9:
                        animations[metballAnimations.OpenCanon].startAnimation(5);
                        break;
                    case 10:
                        animations[metballAnimations.OpenCanon].startAnimation(4);
                        break;
                    case 11:
                        animations[metballAnimations.OpenCanon].startAnimation(3);
                        break;
                    case 12:
                        animations[metballAnimations.OpenCanon].startAnimation(2);
                        break;
                    case 13:
                        animations[metballAnimations.OpenCanon].startAnimation(1);
                        break;
                    case 14:
                        animations[metballAnimations.OpenCanon].startAnimation(0);
                        break;
                    case 15:
                        if (bodyZones[metballBodyZones.Body].currentFrame == 0 ||
                            bodyZones[metballBodyZones.Body].currentFrame == 14)
                            animations[metballAnimations.OpenCanon].startAnimation(0);
                        break;
                }
                break;
            case metballStates.Canon:
                rotationSpeed = 0;
                rotationAccel = 0;
                break;
            case metballStates.CloseCanon:
                switch(bodyZones[metballBodyZones.Canon].currentFrame)
                {
                    case 0:
                        animations[metballAnimations.CloseCanon].startAnimation(6);
                        break;
                    case 1:
                        animations[metballAnimations.CloseCanon].startAnimation(5);
                        break;
                    case 2:
                        animations[metballAnimations.CloseCanon].startAnimation(4);
                        break;
                    case 3:
                        animations[metballAnimations.CloseCanon].startAnimation(3);
                        break;
                    case 4:
                        animations[metballAnimations.CloseCanon].startAnimation(2);
                        break;
                    case 5:
                        animations[metballAnimations.CloseCanon].startAnimation(1);
                        break;
                    case 6:
                        animations[metballAnimations.CloseCanon].startAnimation(0);
                        break;
                    case 7:
                        animations[metballAnimations.CloseCanon].startAnimation(2);
                        break;
                    case 8:
                        animations[metballAnimations.CloseCanon].startAnimation(0);
                        break;
                    case 9:
                        animations[metballAnimations.CloseCanon].startAnimation(1);
                        break;
                    case 10:
                        animations[metballAnimations.CloseCanon].startAnimation(2);
                        break;
                    case 11:
                        animations[metballAnimations.CloseCanon].startAnimation(3);
                        break;
                    case 12:
                        animations[metballAnimations.CloseCanon].startAnimation(4);
                        break;
                    case 13:
                        animations[metballAnimations.CloseCanon].startAnimation(5);
                        break;
                    case 14:
                        animations[metballAnimations.CloseCanon].startAnimation(6);
                        break;
                    case 15:
                        State = metballStates.Idle;
                        break;
                }
                break;
            case metballStates.StartLaser:
                if (bodyZones[metballBodyZones.Laser].currentFrame < 10)
                {
                    animations[metballAnimations.SummonLaser].startAnimation(bodyZones[metballBodyZones.Laser].currentFrame);
                }
                else
                {
                    switch(bodyZones[metballBodyZones.Laser].currentFrame)
                    {
                        case 21:
                            if (bodyZones[metballBodyZones.Body].currentFrame == 9)
                            {
                                animations[metballAnimations.SummonLaser].startAnimation(0);
                            }
                            break;
                        case 20:
                            animations[metballAnimations.SummonLaser].startAnimation(1);
                            break;
                        case 19:
                            animations[metballAnimations.SummonLaser].startAnimation(2);
                            break;
                        case 18:
                            animations[metballAnimations.SummonLaser].startAnimation(3);
                            break;
                        case 17:
                            animations[metballAnimations.SummonLaser].startAnimation(4);
                            break;
                        case 16:
                            animations[metballAnimations.SummonLaser].startAnimation(5);
                            break;
                        case 15:
                            animations[metballAnimations.SummonLaser].startAnimation(6);
                            break;
                        case 14:
                            animations[metballAnimations.SummonLaser].startAnimation(7);
                            break;
                        case 13:
                            animations[metballAnimations.SummonLaser].startAnimation(8);
                            break;
                        default:
                            animations[metballAnimations.SummonLaser].startAnimation(9);
                            break;
                    }
                }
                break;
            case metballStates.Laser:
                rotationSpeed = 0;
                rotationAccel = 0;
                break;
            case metballStates.CloseLaser:
                if (bodyZones[metballBodyZones.Laser].currentFrame < 10 || bodyZones[metballBodyZones.Laser].currentFrame > 11)
                {
                    animations[metballAnimations.SaveLaser].startAnimation(CloseLaserStartFrame[bodyZones[metballBodyZones.Laser].currentFrame]);
                }
                else
                {
                    state = prevState;
                }
                break;
        }
    }

    int[] CloseLaserStartFrame = { 8, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 8 };

    int[] FloorStartFrame = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 0, 1, 2 };
    void floorStart()
    {
        int stFrame = 11;
        if (bodyZones[metballBodyZones.FrontLeg].currentFrame < FloorStartFrame.Length)
        {
            stFrame = FloorStartFrame[bodyZones[metballBodyZones.FrontLeg].currentFrame];
        }
       
        animations[metballAnimations.Floor].startAnimation(stFrame);
    }

    #endregion

    #region physics
    void speed2RotationSpeed()
    {
        rotationSpeed = Mathf.Abs((character.XSpeed / (16f / MobileObject.pixelPerMeter)) * Mathf.Rad2Deg);
        if ((character.XSpeed > 0 && !component.FlipX) ||
            (character.XSpeed < 0 && component.FlipX))
            rotationSpeed -= rotationSpeed;
    }

    void breaks(out bool breaking)
    {
        breaking = true;
        character.XFriction = breakForce;
        if (!skills[1].Locked)
        {
            character.XFriction *= (100 + skills[1].GetOthers(0));
            character.XFriction /= 100f;
        }
    }

    void breaksLess(out bool breaking)
    {
        breaking = true;
        character.XFriction = breakForce;
        if (!skills[1].Locked)
        {
            character.XFriction *= (100 + skills[1].GetOthers(0));
            character.XFriction /= 100f;
        }
        character.XFriction /= 1.4f;
    }

    void AngleToFrame()
    {
        int angleInt = (int)(angle / 30);
        switch (angleInt)
        {
            case 0:
                bodyZones[metballBodyZones.Body].allFrames[0].Use();
                break;
            case 1:
                bodyZones[metballBodyZones.Body].allFrames[11].Use();
                break;
            case 2:
                bodyZones[metballBodyZones.Body].allFrames[10].Use();
                break;
            case 3:
                bodyZones[metballBodyZones.Body].allFrames[9].Use();
                break;
            case 4:
                bodyZones[metballBodyZones.Body].allFrames[8].Use();
                break;
            case 5:
                bodyZones[metballBodyZones.Body].allFrames[7].Use();
                break;
            case 6:
                bodyZones[metballBodyZones.Body].allFrames[6].Use();
                break;
            case 7:
                bodyZones[metballBodyZones.Body].allFrames[5].Use();
                break;
            case 8:
                bodyZones[metballBodyZones.Body].allFrames[4].Use();
                break;
            case 9:
                bodyZones[metballBodyZones.Body].allFrames[3].Use();
                break;
            case 10:
                bodyZones[metballBodyZones.Body].allFrames[2].Use();
                break;
            default:
                bodyZones[metballBodyZones.Body].allFrames[1].Use();
                break;
        }
    }

    void rotateToFrame9()
    {
        if (bodyZones[metballBodyZones.Body].currentFrame > 14)
        {

        }
        else
        {
            if (angle <= 270 && angle > 90)
            {
                angle -= restoreAngleSpeed * Time.deltaTime;
                if (angle < 90) angle = 90;
            }
            else
            {
                angle += restoreAngleSpeed * Time.deltaTime;
                if (angle >= 360) angle -= 360;
                if (angle > 90 && angle < 270) angle = 90;
            }

            AngleToFrame();
        }
    }

    void rotateToFrame0()
    {
        if(bodyZones[metballBodyZones.Body].currentFrame > 14)
        {
            
        }
        else
        {
            if(angle <= 180)
            {
                angle -= restoreAngleSpeed * Time.deltaTime;
                if (angle < 0) angle = 0;
            }
            else
            {
                angle += restoreAngleSpeed * Time.deltaTime;
                if (angle >= 360) angle = 0;
            }

            AngleToFrame();
        }
    }

    void applyRotation()
    {
        if (bodyZones[metballBodyZones.Canon].currentFrame != 15) return;
        if (State > metballStates.Break && legState != metballaLegStates.StandUp) return;
        if (bodyZones[metballBodyZones.Body].currentFrame > 11 && State == metballStates.Break) return;
        if (bodyZones[metballBodyZones.Body].currentFrame > 11 && legState == metballaLegStates.StandUp) return;

        angle += rotationSpeed * Time.deltaTime;

        float realRotationMaxSpeed = rotationMaxSpeed * (1 / character.getAnimationReduction());

        //Movement Skill Effect
        if(!skills[0].Locked)
        {
            realRotationMaxSpeed *= (skills[0].GetOthers(0) + 100);
            realRotationMaxSpeed /= 100f;
        }

        //Movementra Skill Effect
        if (!skills[11].Locked)
        {
            realRotationMaxSpeed *= (skills[11].GetOthers(0) + 100);
            realRotationMaxSpeed /= 100f;
        }

        if (!character.BlockedFromBelow) realRotationMaxSpeed *= 1.2f;

        float gx = (character.gravityResistance().x * MobileObject.pixelPerMeter) / 16;

        gx *= Mathf.Rad2Deg;

        if (component.FlipX) gx = -gx;

        if (bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame > 5)
        {
            gx = 0;
        }

        float desaccFactor = 50;

        if (gx != 0)
        {
            float deltaAcc = rotationAccel;
            //Movementra Skill Effect
            if (!skills[11].Locked)
            {
                deltaAcc *= (skills[11].GetOthers(0) + 100);
                deltaAcc /= 100f;
            }

            deltaAcc -= gx;

            if ((rotationAccel > 0 && gx > 0) || (rotationAccel < 0 && gx < 0))
            {
                if (rotationSpeed > realRotationMaxSpeed)
                {
                    if (deltaAcc < 0) desaccFactor -= deltaAcc;
                    rotationSpeed -= desaccFactor * Time.deltaTime;
                    if (rotationSpeed < realRotationMaxSpeed)
                        rotationSpeed = realRotationMaxSpeed;
                }
                else if (rotationSpeed < -realRotationMaxSpeed)
                {
                    if (deltaAcc > 0) desaccFactor += deltaAcc;
                    rotationSpeed += desaccFactor * Time.deltaTime;
                    if (rotationSpeed > -realRotationMaxSpeed)
                        rotationSpeed = -realRotationMaxSpeed;
                }
                else if((rotationSpeed != realRotationMaxSpeed && rotationSpeed != -realRotationMaxSpeed) ||
                    (rotationSpeed > 0 && deltaAcc < 0)||
                    (rotationSpeed < 0 && deltaAcc > 0))
                {
                    rotationSpeed += deltaAcc * Time.deltaTime;
                }
            }
            else
            {
                rotationSpeed += deltaAcc * Time.deltaTime;
            }
        }
        else
        {
            float realRotationAccel = rotationAccel;

            //Movementra Skill Effect
            if (!skills[11].Locked)
            {
                realRotationAccel *= (skills[11].GetOthers(0) + 100);
                realRotationAccel /= 100f;
            }

                if (rotationSpeed > realRotationMaxSpeed)
            {
                if (rotationAccel < 0) desaccFactor -= realRotationAccel;
                rotationSpeed -= desaccFactor * Time.deltaTime;
                if (rotationSpeed < realRotationMaxSpeed)
                    rotationSpeed = realRotationMaxSpeed;
            }
            else if(rotationSpeed < -realRotationMaxSpeed)
            {
                if (rotationAccel > 0) desaccFactor += realRotationAccel;
                rotationSpeed += desaccFactor * Time.deltaTime;
                if (rotationSpeed > -realRotationMaxSpeed)
                    rotationSpeed = -realRotationMaxSpeed;
            }
            else
            {
                rotationSpeed += realRotationAccel * Time.deltaTime;
                if(rotationSpeed > realRotationMaxSpeed)
                    rotationSpeed = realRotationMaxSpeed;
                if (rotationSpeed < -realRotationMaxSpeed)
                    rotationSpeed = -realRotationMaxSpeed;
            }
        }

        float realRotationlimit = rotationLimitSpeed;

        //Movementra Skill Effect
        if (!skills[11].Locked)
        {
            realRotationlimit *= (skills[11].GetOthers(0) + 100);
            realRotationlimit /= 100;
        }

            if (rotationSpeed > realRotationlimit) rotationSpeed = realRotationlimit;
        if (rotationSpeed < -realRotationlimit) rotationSpeed = -realRotationlimit;

        if (angle >= 360) angle -= 360;
        if (angle < 0) angle += 360;

        AngleToFrame();

        if ((!(bodyZones[metballBodyZones.FrontLeg].currentFrame < 12 &&
                    bodyZones[metballBodyZones.FrontLeg].currentFrame > 5)) &&
                    character.BlockedFromBelow && State != metballStates.Break)
        {
            float spd = (float)(-(rotationSpeed * 2.0 * Math.PI * 16.0) / (360.0 * MobileObject.pixelPerMeter));
            if (component.FlipX)
                spd = -spd;

            character.XSpeed = spd;
        }
    }
    #endregion

    #region skills

    bool bronzeRestriction()
    {
        return Versions[1] || Versions[3];
    }

    bool silverRestriction()
    {
        return Versions[2] || Versions[3];
    }

    bool AllRestriction()
    {
        return Versions[1] && Versions[2] && Versions[3];
    }

    //Skill 0: Movement
    string movementLevelDescription(int level)
    {
        return skills[0].normalDescriptionLevel(level, skills[0].GetOthers(0, level - 1));
    }

    public void skillLevelUp(int skill)
    {
        if (skill < 0) skill = 0;
        if (skill > skills.Length) skill = skills.Length - 1;
        if(skills[skill].CurrentLevel==0)
        {
            skills[skill].Unlock();
        }
        else
        {
            skills[skill].increaseLevel();
        }
    }

    //Skill 1: Break Upgrade
    string skill1LevelDescription(int level)
    {
        return skills[1].normalDescriptionLevel(level, skills[1].GetOthers(0, level - 1));
    }

    //Skill 2: Buffalo Manauver
    string skill2LevelDescription(int level)
    {
        return skills[2].normalDescriptionLevel(level, skills[2].GetBaseDamage(level - 1));
    }

    //Skill 3: Jumping
    string skill3LevelDescription(int level)
    {
        return skills[3].normalDescriptionLevel(level, skills[3].GetOthers(0, level - 1));
    }

    //Skill 4: Increase Agi
    string skill4LevelDescription(int level)
    {
        return skills[4].normalDescriptionLevel(level, skills[4].GetOthers(0, level - 1));
    }


    //Skill 5: Power Impact
    string skill5LevelDescription(int level)
    {
        return skills[5].normalDescriptionLevel(level, skills[5].GetOthers(0, level - 1));
    }

    //Skill 6: Laser X9RA
    string skill6LevelDescription(int level)
    {
        return skills[6].normalDescriptionLevel(level, skills[6].GetBaseDamage(level - 1));
    }

    //Skill 7: Energy Efficiency	
    string skill7LevelDescription(int level)
    {
        return skills[7].normalDescriptionLevel(level, skills[7].GetOthers(0, level - 1), skills[7].GetOthers(1, level - 1));
    }

    //Skill 8: X9RA 2.0	
    string skill8LevelDescription(int level)
    {
        return skills[8].normalDescriptionLevel(level, skills[8].GetOthers(0, level - 1));
    }

    //Skill 9: Canon	
    string skill9LevelDescription(int level)
    {
        return skills[9].normalDescriptionLevel(level, skills[9].GetBaseDamage(level - 1));
    }

    //Skill 10: Great explosion
    string skill10LevelDescription(int level)
    {
        return skills[10].normalDescriptionLevel(level, skills[10].GetOthers(0, level - 1));
    }

    //Skill 11: Movementra
    string skill11LevelDescription(int level)
    {
        return skills[11].normalDescriptionLevel(level, skills[11].GetOthers(0, level - 1));
    }
    #endregion

    #region animationsFixers

    void move3Up()
    {
        component.contactDown[2].gameObject.SetActive(true);
        component.contactDown[3].gameObject.SetActive(true);
        component.angleDetector[1].gameObject.SetActive(true);
        component.angleDetector[0].gameObject.SetActive(false);

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            -3,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            -3,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            -3,
            component.angleDetector[1].localPosition.z);

        if (character.BlockedFromBelow)
        {
            character.y += 3;
        }
    }

    void move3Down()
    {
        if (character.BlockedFromBelow)
        {
            character.y -= 3;
        }
        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            0,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            0,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            0,
            component.angleDetector[1].localPosition.z);

        component.contactDown[2].gameObject.SetActive(false);
        component.contactDown[3].gameObject.SetActive(false);
        component.angleDetector[1].gameObject.SetActive(false);
        component.angleDetector[0].gameObject.SetActive(true);
    }

    void move6Up()
    {
        component.contactDown[2].gameObject.SetActive(true);
        component.contactDown[3].gameObject.SetActive(true);
        component.angleDetector[1].gameObject.SetActive(true);
        component.angleDetector[0].gameObject.SetActive(false);

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            -5, 
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            -5,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            -5,
            component.angleDetector[1].localPosition.z);

        if (character.BlockedFromBelow)
        {
            character.y += 5;
        }
    }

    void move6Down()
    {
        if (character.BlockedFromBelow)
        {
            character.y -= 5;
        }
        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            0,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            0,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            0,
            component.angleDetector[1].localPosition.z);

        component.contactDown[2].gameObject.SetActive(false);
        component.contactDown[3].gameObject.SetActive(false);
        component.angleDetector[1].gameObject.SetActive(false);
        component.angleDetector[0].gameObject.SetActive(true);
    }

    void move7Up()
    {
        component.contactDown[2].gameObject.SetActive(true);
        component.contactDown[3].gameObject.SetActive(true);
        component.angleDetector[1].gameObject.SetActive(true);
        component.angleDetector[0].gameObject.SetActive(false);

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            -6,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            -6,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            -6,
            component.angleDetector[1].localPosition.z);
        if (character.BlockedFromBelow)
        {
            character.y += 6;
        }  
    }

    void move7Down()
    {
        if (character.BlockedFromBelow)
        {
            character.y -= 6;
        }

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            0,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            0,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            0,
            component.angleDetector[1].localPosition.z);

        component.contactDown[2].gameObject.SetActive(false);
        component.contactDown[3].gameObject.SetActive(false);
        component.angleDetector[1].gameObject.SetActive(false);
        component.angleDetector[0].gameObject.SetActive(true);
    }

    void move8Up()
    {
        component.contactDown[2].gameObject.SetActive(true);
        component.contactDown[3].gameObject.SetActive(true);
        component.angleDetector[1].gameObject.SetActive(true);
        component.angleDetector[0].gameObject.SetActive(false);

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            -7,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            -7,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            -7,
            component.angleDetector[1].localPosition.z);

        if (character.BlockedFromBelow)
        {
            character.y += 7;
        }
    }

    void move8Down()
    {
        if (character.BlockedFromBelow)
        {
            character.y -= 7;
        }

        component.contactDown[2].localPosition = new Vector3(component.contactDown[2].localPosition.x,
            0,
            component.contactDown[2].localPosition.z);
        component.contactDown[3].localPosition = new Vector3(component.contactDown[3].localPosition.x,
            0,
            component.contactDown[3].localPosition.z);
        component.angleDetector[1].localPosition = new Vector3(component.angleDetector[1].localPosition.x,
            0,
            component.angleDetector[1].localPosition.z);

        component.contactDown[2].gameObject.SetActive(false);
        component.contactDown[3].gameObject.SetActive(false);
        component.angleDetector[1].gameObject.SetActive(false);
        component.angleDetector[0].gameObject.SetActive(true);
    }

    public void flipX()
    {
        component.FlipX = !component.FlipX;
        rotationSpeed = -rotationSpeed;
        rotationAccel = -rotationAccel;
    }

    #endregion
}
