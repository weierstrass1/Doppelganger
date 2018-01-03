using UnityEngine;

public class defaultButtons
{
    public static string action1 = "z";
    public static string action2 = "x";
    public static string action3 = "c";
    public static string action4 = "v";
    public static string up = "up";
    public static string down = "down";
    public static string left = "left";
    public static string right = "right";
    public static string start = "enter";
    public static string select = KeyCode.Backspace.ToString();
    public static string L = "l";
    public static string R = "r";
}

public abstract class CharacterController : MonoBehaviour
{
    public int Mode = 0;
    public bool UseMode = true;

    public bool[] Versions;

    //0 = Blax
    public int currentVersion = 0;

    public ButtonGetter[] controls;

    public int State
    {
        get
        {
            if (character != null) return character.state;
            return -1;
        }
        set
        {
            if (character != null)
            {
                int lastVal = character.state;
                if (lastVal != value) StartState(value);
            }
        }
    }

    public charComponent component
    {
        get
        {
            if (character != null) return character.components;
            return null;
        }
        set
        {
            character.components = value;
        }
    }

    public animation[] animations
    {
        get
        {
            if (component != null) return component.Animations;
            return null;
        }
    }

    public graphicComponent[] bodyZones
    {
        get
        {
            if (component != null) return component.graphicComponents;
            return null;
        }
    }

    public command[] commands
    {
        get
        {
            if (component != null) return component.cmds;
            return null;
        }
        set
        {
            component.cmds = value;
        }
    }

    public skill[] skills
    {
        get
        {
            if (component == null)
            {
                return null;
            }
            return component.skills;
        }
        set
        {
            component.skills = value;
        }
    }

    [HideInInspector]
    public BaseCharacter character;

    public void AwakeController()
    {
        character.components.owner = character;
        character.components.startComponent = componentStart;
        character.components.updateComponent = componentUpdate;
        character.components.AwakeComponent();
        characterStart();
    }

    public void UpdateController()
    {
        character.components.UpdateComponent();
        characterUpdate();
    }

    public abstract void characterStart();

    public abstract void characterUpdate();

    public abstract void componentStart();

    public abstract void componentUpdate();

    public abstract void StartState(int state);

    public virtual bool shoot()
    {
        if (component.projectileLaunchers == null || component.projectileLaunchers.Length <= 0) return false;
        if (character.Mass - component.projectileLaunchers[0].projectil.GetComponent<BaseCharacter>().Mass < character.minMass)
        {
            return false;
        }
        character.Mass -= component.projectileLaunchers[0].projectil.GetComponent<BaseCharacter>().Mass;
        component.projectileLaunchers[0].owner = character;
        component.projectileLaunchers[0].launchProjectil(component.FlipX, component.FlipY);
        return true;
    }

    public virtual bool shoot(int Index)
    {
        if (component.projectileLaunchers == null || component.projectileLaunchers.Length <= Index) return false;
        if (character.Mass - component.projectileLaunchers[Index].projectil.GetComponent<BaseCharacter>().Mass < character.minMass)
        {
            return false;
        }
        character.Mass -= component.projectileLaunchers[Index].projectil.GetComponent<BaseCharacter>().Mass;
        component.projectileLaunchers[Index].owner = character;
        component.projectileLaunchers[Index].launchProjectil(component.FlipX, component.FlipY);
        return true;
    }
}
