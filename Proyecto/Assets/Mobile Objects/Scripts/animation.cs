using UnityEngine;
using System;

[Serializable]
public delegate void action();

[Serializable]
public class animation
{
    public string name;
    public frameList[] frames;
    public int[] times;
    public int[] nexts;
    public bool[] xFlips;
    public bool[] yFlips;
    public bool canChangeFlipX = false, canChangeFlipY = false;
    public charComponentAction[] startAction = null, endAction = null;
    public charComponentAction[] StartAction
    {
        get
        {
            if (startAction == null)
            {
                startAction = new charComponentAction[frames.Length];
            }
            return startAction;
        }
        set
        {
            startAction = value;
        }
    }
    public charComponentAction[] EndAction
    {
        get
        {
            if (endAction == null)
            {
                endAction = new charComponentAction[frames.Length];
            }
            return endAction;
        }
        set
        {
            endAction = value;
        }
    }
    public int index;
    float currentTime = 0;
    public int currentIndex = 0;
    public charComponent owner;
    public bool affectedByReduction = true;
    public bool fold;
    public bool[] foldFrames;
    bool starting = false;

    public animation(charComponent Owner,int Index)
    {
        owner = Owner;
        index = Index;
    }

    public void startAnimation(int index)
    {
        starting = true;
        UseFrame(index);
    }

    public void UseFrame(int index)
    {
        if (!starting && EndAction != null && EndAction[currentIndex] != default(charComponentAction))
        {
            try
            {
                EndAction[currentIndex]();
            }
            catch
            {
                EndAction[currentIndex] = default(charComponentAction);
            }
        }
        starting = false;
        currentIndex = index;
        currentTime = 0;

        if (xFlips != null && canChangeFlipX)
            owner.FlipX = owner.FlipX ^ xFlips[currentIndex];
        if (yFlips != null && canChangeFlipY)
            owner.FlipY = owner.FlipY ^ yFlips[currentIndex];

        if(frames!=null)
        {
            if (currentIndex >= frames.Length || currentIndex < 0)
            {
                Debug.Log(name + ": " + currentIndex);
            }
            foreach (frame f in frames[currentIndex].frames)
            {
                if (f != null)
                {
                    f.Use();
                }
            }  
        }


        if (StartAction != null && StartAction[currentIndex] != default(charComponentAction))
        {
            try
            {
                StartAction[currentIndex]();
            }
            catch
            {
                StartAction[currentIndex] = default(charComponentAction);
            }
        }
    }

    public bool FrameIsFinished(int frame)
    {
        if (currentIndex != frame) return false;
        return isFinished();
    }

    public bool FrameIsFinished(int frame,int FPS)
    {
        if (currentIndex != frame) return false;
        return isFinished(FPS);
    }

    // Update is called once per frame
    public void excecuteAnimation()
    {
        if(affectedByReduction)
        {
            if (currentIndex >= frames.Length || currentIndex < 0)
            {
                Debug.Log(name + ": " + currentIndex);
            }
            float maxTime = globalVars.FPS / (float)times[currentIndex];
            owner.owner = owner.transform.parent.gameObject.GetComponent<BaseCharacter>();
            float checkTime = 1 / maxTime;
            if (maxTime == 0) checkTime = float.MaxValue;
            if (affectedByReduction && owner.owner!=null)
            {
                checkTime *= owner.owner.getAnimationReduction();
            }

            currentTime += Time.deltaTime;
            if (currentTime >= checkTime)
            {
                UseFrame(nexts[currentIndex]);
            }

        }
    }

    public void excecuteAnimation(int FPS)
    {
        if (affectedByReduction)
        {
            float maxTime = FPS / (float)times[currentIndex];
            owner.owner = owner.transform.parent.gameObject.GetComponent<BaseCharacter>();
            float checkTime = 1 / maxTime;
            if (maxTime == 0) checkTime = float.MaxValue;
            if (affectedByReduction && owner.owner != null)
            {
                checkTime *= owner.owner.getAnimationReduction();
            }

            currentTime += Time.deltaTime;
            if (currentTime >= checkTime)
            {
                UseFrame(nexts[currentIndex]);
            }

        }
    }

    public void excecuteAnimationWithoutAnimReduction(int FPS)
    {
        float maxTime = FPS / (float)times[currentIndex];
        owner.owner = owner.transform.parent.gameObject.GetComponent<BaseCharacter>();
        float checkTime = 1 / maxTime;
        if (maxTime == 0) checkTime = float.MaxValue;

        currentTime += Time.deltaTime;
        if (currentTime >= checkTime)
        {
            UseFrame(nexts[currentIndex]);
        }
    }

    public bool isFinished()
    {
        if (affectedByReduction)
        {
            float maxTime = globalVars.FPS / (float)times[currentIndex];
            owner.owner = owner.transform.parent.gameObject.GetComponent<BaseCharacter>();
            float checkTime = 1 / maxTime;
            if (maxTime == 0) checkTime = float.MaxValue;
            if (affectedByReduction && owner.owner != null)
            {
                checkTime *= owner.owner.getAnimationReduction();
            }

            if (currentTime + Time.deltaTime >= checkTime)
            {
                return true;
            }

        }
        return false;
    }

    public bool isFinished(int FPS)
    {
        if (affectedByReduction)
        {
            float maxTime = FPS / (float)times[currentIndex];
            owner.owner = owner.transform.parent.gameObject.GetComponent<BaseCharacter>();
            float checkTime = 1 / maxTime;
            if (maxTime == 0) checkTime = float.MaxValue;
            if (affectedByReduction && owner.owner != null)
            {
                checkTime *= owner.owner.getAnimationReduction();
            }

            if (currentTime + Time.deltaTime >= checkTime)
            {
                return true;
            }

        }
        return false;
    }
}
