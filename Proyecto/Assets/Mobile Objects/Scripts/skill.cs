using System.Collections.Generic;
using UnityEngine;

public enum properties
{
    none,fire, water, earth, wind, electric, light, dark, magnetic, radioactivity,
    physic, psychic, metal, rock, energy, plasma, explosion, piercing
};

public enum skillType { pasive, active, target };

public enum skillRecipient { onlyBlax, currentTransformation, allTransformation };

public enum targetType { None, AreaOfEffect, SingleTarget, AOEANDST, PhysicalContact};

public delegate void skillEnergy();
public delegate int getValue(int level);
public delegate bool restriction();
public delegate string getDescription();
public delegate string getLevelDescription(int level);

/// <summary>
/// Clase que representa una skill o habilidad. Estas habilidades son utilizados para representar 
/// ataques o mejoras a los distintos personajes.
/// </summary>
public class skill
{
    /// <summary>
    /// Indice de la skill
    /// </summary>
    public int Index = 0;
    /// <summary>
    /// who have the skill
    /// </summary>
    public BaseCharacter owner;
    /// <summary>
    /// Dice si la skill esta bloqueada.
    /// </summary>
    private bool locked = true;
    /// <summary>
    /// Dice si la skill esta bloqueada (Solo Lectura).
    /// </summary>
    public bool Locked { get { return locked; } }
    /// <summary>
    /// Skills que son requerimientos para desbloquear esta skill
    /// </summary>
    private List<skill> parents = null;
    /// <summary>
    /// Skills que pueden ser desbloqueadas a partir de esta skill.
    /// </summary>
    private List<skill> children = null;
    /// <summary>
    /// Nivel minimo para desbloquear las skills hijas de esta skill.
    /// </summary>
    private List<int> minLevels = null;

    /// <summary>
    /// Agrega una skill a la lista de las skilles que requieren de esta skill para desbloquearlas.
    /// </summary>
    /// <param name="child">Skill que se agregara.</param>
    /// <param name="minLevel">Nivel minimo para desbloquear la skill agregada.</param>
    public void addChild(skill child, int minLevel)
    {
        if (child == null) return;
        if (minLevel < 1) minLevel = 1;
        if (children == null) children = new List<skill>();
        if (minLevels == null) minLevels = new List<int>();

        if (child.parents == null) child.parents = new List<skill>();

        child.parents.Add(this);

        children.Add(child);
        minLevels.Add(minLevel);
    }
    /// <summary>
    /// Propiedad de esta skill.
    /// </summary>
    public properties property;
    /// <summary>
    /// Describe el uso de la skill.
    /// Pasive : Es una skill que solo afecta al poseedor de la skill y el efecto es permanente.
    /// Active : Es una skill que solo ejerce su efecto cuando es utilizada
    /// Target : Es una skill que debe ser utilizada sobre un enemigo (o grupo de enemigos).
    /// o posicion especifica (o grupo de posiciones).
    /// </summary>
    public skillType type;
    public targetType TargetType;
    /// <summary>
    /// Nivel máximo de esta skill.
    /// </summary>
    public int maxLevel;
    /// <summary>
    /// Nivel actual de esta skill.
    /// </summary>
    private int currentLevel = 0;
    /// <summary>
    /// Nivel actual de esta skill (solo lectura).
    /// </summary>
    public int CurrentLevel
    {
        get { return currentLevel; }
    }
    /// <summary>
    /// Dice si la skill afecta solo a blax, a todas las transformaciones o solo a la transformacion que posee la skill.
    /// </summary>
    public skillRecipient recipient;

    public string Name
    {
        get
        {
            return textTranslate.SkillNames[Index, textTranslate.SelectedLanguage];
        }
    }

    public string Description
    {
        get
        {
            if (description == null) description = normalDescription;
            return description();
        }
    }

    public getDescription DescriptionFunction
    {
        set
        {
            description = value;
        }
    }

    private getDescription description;

    public string normalDescription()
    {
        return textTranslate.SkillDescriptions[Index, textTranslate.SelectedLanguage];
    }

    public string DescriptionLevel(int level)
    {
        if (descriptionLevel == null) descriptionLevel = normalDescriptionLevel;
        return descriptionLevel(level);
    }

    public getLevelDescription DescriptionLevelFunction
    {
        set
        {
            descriptionLevel = value;
        }
    }

    private getLevelDescription descriptionLevel;

    private string normalDescriptionLevel(int level)
    {
        return normalDescriptionLevel(level);
    }

    public string normalDescriptionLevel(params int[] args)
    {
        string st = textTranslate.SkillLevelDescriptions[Index, textTranslate.SelectedLanguage];

        if (args == null || args.Length <= 0)
            return st;
        else
        {
            st = st.Replace("#LVL", "" + args[0]);

            for (int i = 1; i < args.Length; i++)
            {
                st = st.Replace("#VAL " + (i - 1), "" + args[i]);
            }
        }
        return st;
    }
    /// <summary>
    /// Es una funcion que permite saber cual es el grado de critico de la skill dependiendo del 
    /// nivel de la skill.
    /// </summary>
    private int[] gCritical;
    /// <summary>
    /// Es una funcion que permite saber cual es el grado de critico de la skill dependiendo del 
    /// nivel de la skill. (Solo Escritura).
    /// </summary>
    public int[] getCritical
    {
        get
        {
            if(gCritical==null)
            {
                gCritical = new int[maxLevel];
            }
            return gCritical;
        }
        set
        {
            gCritical = value;
        }
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber cual es el grado de critico de la skill.
    /// </summary>
    /// <returns>Grado de critico de la skill.</returns>
    public int GetCritical()
    {
        if (currentLevel == 0) return 0;
        if (gCritical != null) return gCritical[currentLevel - 1];
        return 0;
    }
    public int GetCritical(int level)
    {
        if (level <= 0) return 0;
        if (gCritical != null) return gCritical[level];
        return 0;
    }
    /// <summary>
    /// Es una funcion que permite saber cual es el costo en energia de la skill dependiendo del 
    /// nivel de la skill.
    /// </summary>
    private int[] gEnergy;
    /// <summary>
    /// Es una funcion que permite saber cual es el costo en energia de la skill dependiendo del 
    /// nivel de la skill (Solo escritura).
    /// </summary>
    public int[] getEnergy
    {
        get
        {
            if (gEnergy == null)
            {
                gEnergy = new int[maxLevel];
            }
            return gEnergy;
        }
        set
        {
            gEnergy = value;
        }
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber cual es el costo en energia de la skill.
    /// </summary>
    /// <returns>Costo en energia de la skill.</returns>
    public int GetEnergy()
    {
        if (currentLevel == 0) return 0;
        if (gEnergy != null) return gEnergy[currentLevel - 1];
        return 0;
    }
    public int GetEnergy(int level)
    {
        if (level <= 0) return 0;
        if (gEnergy != null) return gEnergy[level];
        return 0;
    }
    /// <summary>
    /// Es una funcion que permite saber cual es el daño base de la skill dependiendo del 
    /// nivel de la skill.
    /// </summary>
    private int[] gBaseDamage;
    /// <summary>
    /// Es una funcion que permite saber cual es el daño base de la skill dependiendo del 
    /// nivel de la skill (Solo escritura).
    /// </summary>
    public int[] getBaseDamage
    {
        get
        {
            if (gBaseDamage == null)
            {
                gBaseDamage = new int[maxLevel];
            }
            return gBaseDamage;
        }
        set
        {
            gBaseDamage = value;
        }
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber cual es el daño base de la skill.
    /// </summary>
    /// <returns>Daño base de la skill.</returns>
    public int GetBaseDamage()
    {
        if (currentLevel == 0) return 0;
        if (gBaseDamage != null)return gBaseDamage[currentLevel - 1];
        return 0;
    }
    public int GetBaseDamage(int level)
    {
        if (level <= 0) return 0;
        if (gBaseDamage != null) return gBaseDamage[level];
        return 0;
    }
    /// <summary>
    /// Es una funcion que permite saber cual es la duracion de la skill dependiendo del 
    /// nivel de la skill.
    /// </summary>
    private int[] gTime;
    /// <summary>
    /// Es una funcion que permite saber cual es la duracion de la skill dependiendo del 
    /// nivel de la skill (Solo escritura).
    /// </summary>
    public int[] getTime
    {
        get
        {
            if (gTime == null)
            {
                gTime = new int[maxLevel];
            }
            return gTime;
        }
        set
        {
            gTime = value;
        }
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber cual es la duracion de la skill.
    /// </summary>
    /// <returns>Duracion de la skill.</returns>
    public int GetTime()
    {
        if (currentLevel == 0) return 0;
        if (gTime != null) return gTime[currentLevel - 1];
        return -1;
    }
    public int GetTime(int level)
    {
        if (level <= 0) return 0;
        if (gTime != null) return gTime[level];
        return -1;
    }
    /// <summary>
    /// Es una funcion que permite saber cuanto HP recupera la skill dependiendo del 
    /// nivel de la skill.
    /// </summary>
    private int[] gRecover;
    /// <summary>
    /// Es una funcion que permite saber cuanto HP recupera la skill dependiendo del 
    /// nivel de la skill (Solo escritura).
    /// </summary>
    public int[] getRecover
    {
        get
        {
            if (gRecover == null)
            {
                gRecover = new int[maxLevel];
            }
            return gRecover;
        }
        set
        {
            gRecover = value;
        }
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber cuanto HP recupera la skill.
    /// </summary>
    /// <returns>HP que recupera la skill.</returns>
    public int GetRecover()
    {
        if (currentLevel == 0) return 0;
        if (gRecover != null) return gRecover[currentLevel - 1];
        return -1;
    }
    public int GetRecover(int level)
    {
        if (level <= 0) return 0;
        if (gRecover != null) return gRecover[level];
        return -1;
    }
    private int[] gExperience;
    public int[] getExperience
    {
        get
        {
            if (gExperience == null)
            {
                gExperience = new int[maxLevel];
            }
            return gExperience;
        }
        set
        {
            gExperience = value;
        }
    }

    public int GetExperience()
    {
        if (currentLevel == 0) return 0;
        if (gExperience != null) return gExperience[currentLevel - 1];
        return -1;
    }

    public int GetExperience(int level)
    {
        if (level <= 0) return 0;
        if (gExperience != null) return gExperience[level];
        return -1;
    }
    /// <summary>
    /// Es una lista de funciones que permite saber ciertos valores que varian segun el 
    /// nivel de la skill.
    /// </summary>
    private List<int[]> gOthers;
    /// <summary>
    /// Permite agregar una funcion para saber cierto valor que varia segun el
    /// nivel de la skill.
    /// </summary>
    /// <param name="func">Funcion que dice cuanto varia el valor.</param>
    /// <returns>Indice en que quedo almacenada la funcion en la lista. Si es -1 es por que hubo un error.</returns>
    public int addGetOthers(int[] func)
    {
        if (func == null) return -1;
        if (gOthers == null) gOthers = new List<int[]>();
        gOthers.Add(func);
        return gOthers.Count - 1;
    }
    /// <summary>
    /// Ejecuta una funcion que permite saber como cambia cierto valor dependiendo del nivel de la skill.
    /// </summary>
    /// <param name="index">Indice de la funcion.</param>
    /// <returns>Valor que se quiere saber.</returns>
    public int GetOthers(int index)
    {
        if (gOthers == null) return -1;
        if (index >= gOthers.Count) return -1;
        if (index < 0) return -1;
        if (currentLevel == 0) return 0;
        if (gOthers[index] != null) return gOthers[index][currentLevel - 1];
        return -1;
    }
    public int GetOthers(int index, int level)
    {
        if (gOthers == null) return -1;
        if (index >= gOthers.Count) return -1;
        if (index < 0) return -1;
        if (level <= 0) return 0;
        if (gOthers[index] != null) return gOthers[index][level];
        return -1;
    }
    /// <summary>
    /// Tiempo que lleva siendo utilizada la skill, solo funciona con skilles que
    /// funcionan durante un tiempo.
    /// </summary>
    private float time = 0f;
    /// <summary>
    /// Funcion que es llamada cuando la skill recien inicia.
    /// </summary>
    private skillEnergy startEffect;
    /// <summary>
    /// Funcion que es llamada cuando la skill termina.
    /// </summary>
    private skillEnergy endEffect;
    /// <summary>
    /// Funcion que es llamada mientras la skill esta funcionando.
    /// </summary>
    private skillEnergy timeEffect;
    /// <summary>
    /// Funcion que es llamada cuando la skill aumenta de nivel.
    /// </summary>
    private skillEnergy changeLevelEffect;
    /// <summary>
    /// Funcion que es llamada cuando la skill recien inicia (Solo escritura).
    /// </summary>
    public skillEnergy StartEffect { set { startEffect = value; } }
    /// <summary>
    /// Funcion que es llamada cuando la skill termina (Solo escritura).
    /// </summary>
    public skillEnergy EndEffect { set { endEffect = value; } }
    /// <summary>
    /// Funcion que es llamada cuando la skill aumenta de nivel (Solo escritura).
    /// </summary>
    public skillEnergy TimeEffect { set { timeEffect = value; } }
    /// <summary>
    /// Funcion que es llamada cuando la skill aumenta de nivel (Solo escritura).
    /// </summary>
    public skillEnergy ChangeLevelEffect { set { changeLevelEffect = value; } }
    /// <summary>
    /// Ejecuta una funcion que es llamada cuando la skill recien inicia.
    /// </summary>
    public void ExcecuteStartEffect()
    {
        if (GetEnergy() > owner.CurrentEnergy) return;
        time = 0;
        if (startEffect != null) startEffect();
    }
    /// <summary>
    /// Ejecuta una funcion que es llamada cuando la skill termina.
    /// </summary>
    public void ExcecuteEndEffect()
    {
        if (endEffect != null) endEffect();
    }
    /// <summary>
    /// Ejecuta una funcion que es llamada en todo momento mientras la skill funciona.
    /// Tambien aumenta el tiempo de duracion de la skill.
    /// </summary>
    public void ExcecuteTimeEffect()
    {
        if (timeEffect != null && (GetTime() > time))
        {
            timeEffect();
            time += Time.deltaTime;
        }
    }
    /// <summary>
    /// Aumenta el nivel de la skill. 
    /// Tambien ejecuta una funcion que sucede cuando aumenta el nivel de la skill.
    /// </summary>
    /// <returns>Si el nivel fue subido con exito retorna verdadero.</returns>
    public bool increaseLevel()
    {
        if (currentLevel >= maxLevel)
        {
            currentLevel = maxLevel;
            return false;
        }
        currentLevel++;
        if (changeLevelEffect != null) changeLevelEffect();
        return true;
    }

    public bool setLevel(int level)
    {
        if (level >= maxLevel)
        {
            currentLevel = maxLevel;
            return false;
        }
        currentLevel = level;
        if (changeLevelEffect != null) changeLevelEffect();
        return true;
    }
    /// <summary>
    /// Permite llevar un registro de todos los bonus que han sido utilizados.
    /// </summary>
    List<bonusRegister> bonusRegisters = new List<bonusRegister>();
    /// <summary>
    /// Permite llevar un registro de todos los bonus que han sido utilizados (Solo lectura).
    /// </summary>
    public List<bonusRegister> BonusRegisters { get { return bonusRegisters; } }
    /// <summary>
    /// Funcion que dice las restricciones adicionales que puede tener una skill
    /// para ser desbloqueada.
    /// </summary>
    private restriction restrictions;
    /// <summary>
    /// Funcion que dice las restricciones adicionales que puede tener una skill
    /// para ser desbloqueada (Solo escritura).
    /// </summary>
    public restriction Restrictions { set { restrictions = value; } }
    /// <summary>
    /// Funcion que dice si la skill puede ser desbloqueada.
    /// </summary>
    /// <returns>Retorna verdadero si puede ser desbloqueada.</returns>
    public bool canBeUnlocked()
    {
        if (restrictions!=null && !restrictions()) return false;

        if (parents != null && parents.Count > 0)
        {
            int i = 0;
            foreach (skill s in parents)
            {
                i = s.children.IndexOf(this);
                if (s.currentLevel < s.minLevels[i]) return false;
            }
            return true;
        }

        return true;
    }

    public bool Unlock()
    {
        if (canBeUnlocked())
        {
            increaseLevel();
            locked = false;
        }
        return locked;
    }
}
