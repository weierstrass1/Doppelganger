public class Language
{
    public const int Spanish = 0;
    public const int English = 1;
}

public class SkillTexts
{
    public const int metballSkill0 = 0;
    public const int metballSkill1 = 1;
    public const int metballSkill2 = 2;
    public const int metballSkill3 = 3;
    public const int metballSkill4 = 4;
    public const int metballSkill5 = 5;
    public const int metballSkill6 = 6;
    public const int metballSkill7 = 7;
    public const int metballSkill8 = 8;
    public const int metballSkill9 = 9;
    public const int metballSkill10 = 10;
    public const int metballSkill11 = 11;
    public const int metballSkill12 = 12;
    public const int metballSkill13 = 13;
}

public class textTranslate
{
    public static int SelectedLanguage = 0;
    public static string[,] SkillNames =
    {
        #region metballSkillsNames
        //Metball Skills
        /*0*/{"Movimiento", "Movement" },
        /*1*/{"Freno Mejorado", "Break Upgrade" },
        /*2*/{"Gran Salto", "Hi-Jump" },
        /*3*/{"Aumento de Agilidad", "Increase AGI" },
        /*4*/{"Maniobra Bufalo", "Buffalo Maneuver" },
        /*5*/{"Gran Impacto", "Power Impact" },
        /*6*/{"Laser X9RA", "Laser X9RA" },
        /*7*/{"Eficiencia Energetica", "Energy Efficiency" },
        /*8*/{"X9RA V.2.0", "X9RA V.2.0" },
        /*9*/{"Cañon", "Canon" },
        /*10*/{"Gran Explosión", "Great Explosion" },
        /*11*/{"Movimiento+", "Movementra" },
        /*12*/{"Auto-Apuntado: X9RA", "Auto-Aim: X9RA" },
        /*13*/{"Auto-Apuntado: Cañon", "Auto-Aim: Canon" }
        #endregion
    };
    public static string[,] SkillDescriptions =
    {
        #region metballSkillsDescriptions
        //Metball Skills
        /*0*/{"Permite moverse rodando por el piso.\nAumenta la velocidad de movimiento.",
            "Allows you to move by rolling on the floor.\nIncrease movement speed." },
        /*1*/{"Permite frenar con mayor facilidad.",
            "Allows you to break more easily" },
        /*2*/{"Permite saltar cuando esta parado en 2 piernas.\nAumenta la potencia de salto.",
            "Allows you to jump when it is standing on 2 legs.\nIncrease jump force." },
        /*3*/{"Aumenta la Agilidad cuando usas la transformación.",
            "Increase AGI when you are using the transformation." },
        /*4*/{"Permite acelerar al máximo mientras esta parado en 2 piernas.\nPermite dañar enemigos cuando chocas.\nAumenta el daño cuando se choca contra un enemigo.",
            "Allows you to accelerate to the maximum when it is standing on 2 legs.\nAllows you to damage enemies when it hit it.\nIncrease damage by hit." },
        /*5*/{"El daño por choque aumenta dependiendo de la velocidad.",
            "Increase damage by hit based on the movement speed." },
        /*6*/{"Permite hacer ataques con el laser.\nAumenta el daño producido por el laser.",
            "Allows you to use laser attacks.\nIncrease damage by laser." },
        /*7*/{"Disminuye el costo de usar el laser.\nDisminuye el retraso del disparo del laser.",
            "Decrease the cost to use laser attacks.\nDecrease laser delay." },
        /*8*/{"Aumenta el daño provocado por el laser.",
            "Increase damage by laser." },
        /*9*/{"Permite hacer ataques con el cañon.\nAumenta el daño del cañon.",
            "Allows you to use canon attacks.\nIncrease damage by canon." },
        /*10*/{"Aumenta el daño producido por el cañon.\nAumenta el area de explosion de las balas de cañon.",
            "Increase damage by canon. Increase area of damage when a canon ball explode." },
        /*11*/{"Duplica la efectividad de #SKN "+SkillTexts.metballSkill5+".\nAumenta la velocidad máxima y aceleracion en el suelo en 40%.",
            "Doubles the effect of #SKN "+SkillTexts.metballSkill5+".\nIncrease Maximum Speed and Acceleration when it is on floor in 40%." },
        /*12*/{"Permite elegir un objetivo y los ataques de laser apuntaran hacia ese objetivo.",
            "Allows you to select a target and the laser will aim to that target." },
        /*13*/{"Permite elegir un objetivo y los ataques de cañon apuntaran hacia ese objetivo.",
            "Allows you to select a target and the canon will aim to that target." }
        #endregion
    };
    public static string[,] SkillLevelDescriptions =
    {
        #region metballSkillsDescriptionsLevels
        //Metball Skills
        /*0*/{"Nivel #LVL: Aumenta la velocidad de movimiento en #VAL 0%.",
            "Level #LVL: Increase movement speed in #VAL 0%." },
        /*1*/{"Nivel #LVL: Frena #VAL 0% más rapido.",
            "Level #LVL: Break #VAL 0% faster." },
        /*2*/{"Nivel #LVL: Aumenta la potencia de salto en #VAL 0%.",
            "Level #LVL: Increase jump force #VAL 0%." },
        /*3*/{"Nivel #LVL: Aumenta la Agilidad en #VAL 0%.",
            "Level #LVL: Increase AGI #VAL 0%." },
        /*4*/{"Nivel #LVL: Daño base #VAL 0",
            "Level #LVL: Base damage #VAL 0" },
        /*5*/{"Nivel #LVL: Aumenta el daño por choque en #VAL 0*Velocidad%",
            "Level #LVL: Increase damage by hit in #VAL 0*Speed%" },
        /*6*/{"Nivel #LVL: Daño base #VAL 0",
            "Level #LVL: Base damage #VAL 0" },
        /*7*/{"Nivel #LVL: Disminuye el costo en #VAL 0%. Disminuye el retraso en #VAL 1%.",
            "Level #LVL: Decrease the cost in #VAL 0%. Decrease delay in #VAL 1%." },
        /*8*/{"Nivel #LVL: Aumenta el daño del laser en #VAL 0%.",
            "Level #LVL: Increase damage by laser in #VAL 0%." },
        /*9*/{"Nivel #LVL: Daño base #VAL 0",
            "Level #LVL: Base damage #VAL 0" },
        /*10*/{"Nivel #LVL: Aumenta el daño del cañon en #VAL 0%.",
            "Level #LVL: Increase damage by canon in #VAL 0%." },
        /*11*/{"","" },
        /*12*/{"","" },
        /*13*/{"","" },
        #endregion
    };
}
