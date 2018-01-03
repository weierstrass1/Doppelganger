using System.Collections.Generic;
using UnityEngine;

public enum buttonState { Down, Up, Press};

public class ButtonGetter
{
    string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    List<buttonState> states;
    List<string> buttons;

    public ButtonGetter(string name)
    {
        _name = name;
        buttons = new List<string>();
        states = new List<buttonState>();
    }

    public void addButton(string button, buttonState state)
    {
        buttons.Add(button);
        states.Add(state);
    }

    public bool isUsing
    {
        get
        {
            if (buttons.Count <= 0) return true;

            IEnumerator<string> b = buttons.GetEnumerator();
            IEnumerator<buttonState> s = states.GetEnumerator();

            b.Reset();
            s.Reset();

            while(b.MoveNext())
            {
                s.MoveNext();

                switch(s.Current)
                {
                    case buttonState.Down:
                        if (!Input.GetKeyDown(b.Current)) return false;
                        break;
                    case buttonState.Up:
                        if (!Input.GetKeyUp(b.Current)) return false;
                        break;
                    default:
                        if (!Input.GetKey(b.Current)) return false;
                        break;
                }
            }
            return true;
        }
    }
}
