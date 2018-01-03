using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textChanger : MonoBehaviour
{
    public CharacterController charac;
    public Text[] t;
    string[] initialTexts;

    public Text detail;

    public void Start()
    {
        if (t != null && charac != null)
        {
            initialTexts = new string[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != null)
                {
                    initialTexts[i] = t[i].text;
                    textChange(i);
                }
            }
        }
    }

    public void textChange(int text)
    {
        int newLvl = charac.skills[text].CurrentLevel;
        string lastText = initialTexts[text];
        string newText = lastText.Replace("#LVL", "" + newLvl);
        t[text].text = newText;

        if (detail != null)
        {
            if(charac.skills[text].maxLevel>1)
            {
                detail.text = charac.skills[text].DescriptionLevel(charac.skills[text].CurrentLevel);
            }
            else
            {
                detail.text = charac.skills[text].Description;
            }
        }
    }
}
