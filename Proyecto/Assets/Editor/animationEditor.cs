using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(animationComponent))]
public class animationEditor : Editor
{
    bool foldAnimations;
    public graphicComponent selectedGC;
    public int selectedFrame;
    int startNum, endNum, startingIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorUtility.SetDirty(target);
        animationComponent ac = (animationComponent)target;

        int numAnims = 0;
        if (ac.animations != null) numAnims = ac.animations.Length;
        numAnims = EditorGUILayout.IntField("Animation count", numAnims);

        if (numAnims < 0) numAnims = 0;
        if (ac.animations == null || (ac.animations != null && numAnims != ac.animations.Length))
        {
            animation[] newA = new animation[numAnims];
            for (int i = 0; i < newA.Length; i++)
            {
                if (i < ac.animations.Length)
                {
                    newA[i] = ac.animations[i];
                }
                else
                {
                    newA[i] = new animation(ac.transform.parent.gameObject.GetComponent<charComponent>(), i);
                }
            }

            ac.animations = newA;
        }

        if (GUILayout.Button("Add a Animation"))
        {
            animation[] newA = new animation[ac.animations.Length + 1];
            for (int i = 0; i < ac.animations.Length; i++)
            {
                newA[i] = ac.animations[i];
            }

            newA[ac.animations.Length] = new animation(ac.transform.parent.gameObject.GetComponent<charComponent>(), ac.animations.Length);

            ac.animations = newA;
        }

        if (ac.animations != null)
        {
            foldAnimations = EditorGUILayout.Foldout(foldAnimations, "Show Animations");
            EditorGUI.indentLevel++;
            for (int i = 0; i < ac.animations.Length; i++)
            {
                ac.animations[i].fold = EditorGUILayout.Foldout(ac.animations[i].fold, ac.animations[i].name);

                if (ac.animations[i].fold)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField("Index " + ac.animations[i].index);
                    ac.animations[i].name = EditorGUILayout.TextField("Name", ac.animations[i].name);

                    int numFrameLists = 0;
                    if (ac.animations[i].frames != null) numFrameLists = ac.animations[i].frames.Length;
                    EditorGUILayout.LabelField("Frame Count " + numFrameLists);

                    if (GUILayout.Button("Add a Frame"))
                    {
                        frameList[] newf = new frameList[numFrameLists + 1];
                        int[] times = new int[numFrameLists + 1];
                        int[] nexts = new int[numFrameLists + 1];
                        bool[] xFlips = new bool[numFrameLists + 1];
                        bool[] yFlips = new bool[numFrameLists + 1];
                        bool[] foldFrames = new bool[numFrameLists + 1];
                        charComponentAction[] StartAction = new charComponentAction[numFrameLists + 1];
                        charComponentAction[] EndAction = new charComponentAction[numFrameLists + 1];
                        for (int j = 0; j < numFrameLists; j++)
                        {
                            newf[j] = ac.animations[i].frames[j];
                            times[j] = ac.animations[i].times[j];
                            nexts[j] = ac.animations[i].nexts[j];
                            xFlips[j] = ac.animations[i].xFlips[j];
                            yFlips[j] = ac.animations[i].yFlips[j];
                            foldFrames[j] = ac.animations[i].foldFrames[j];
                            StartAction[j] = ac.animations[i].StartAction[j];
                            EndAction[j] = ac.animations[i].EndAction[j];
                        }

                        newf[numFrameLists] = new frameList();
                        times[numFrameLists] = 1;
                        nexts[numFrameLists] = numFrameLists + 1;
                        xFlips[numFrameLists] = false;
                        yFlips[numFrameLists] = false;
                        foldFrames[numFrameLists] = false;
                        StartAction[numFrameLists] = default(charComponentAction);
                        EndAction[numFrameLists] = default(charComponentAction);

                        ac.animations[i].frames = newf;
                        ac.animations[i].times = times;
                        ac.animations[i].nexts = nexts;
                        ac.animations[i].xFlips = xFlips;
                        ac.animations[i].yFlips = yFlips;
                        ac.animations[i].foldFrames = foldFrames;
                        ac.animations[i].StartAction = StartAction;
                        ac.animations[i].EndAction = EndAction;
                    }

                    if (ac.animations[i].frames != null)
                    {
                        selectedGC = (graphicComponent)EditorGUILayout.ObjectField(selectedGC, typeof(graphicComponent), true);

                        startNum = EditorGUILayout.IntField("Start Index", startNum);
                        endNum = EditorGUILayout.IntField("End Index", endNum);
                        startingIndex = EditorGUILayout.IntField("Starting Index", startingIndex);

                        if (GUILayout.Button("Add Graphics to Frames") && selectedGC != null)
                        {
                            if (selectedGC.allFrames != null
                                && ac.animations[i].frames.Length > startingIndex
                                && selectedGC.allFrames.Length > endNum
                                && selectedGC.allFrames.Length > startNum) 
                            {
                                for (int j = startingIndex, k = startNum; j < ac.animations[i].frames.Length && k <= endNum; j++, k++)
                                {
                                    ac.animations[i].frames[j].frames.Add(selectedGC.allFrames[k]);
                                }
                            }
                        }

                        if (selectedGC != null && selectedGC.allFrames != null && selectedGC.allFrames.Length > 0)
                        {
                            string[] names = new string[selectedGC.allFrames.Length];
                            for (int k = 0; k < selectedGC.allFrames.Length; k++)
                            {
                                names[k] = selectedGC.allFrames[k].name;
                            }
                            selectedFrame = EditorGUILayout.Popup("Frames", selectedFrame, names);
                            EditorGUILayout.ObjectField(selectedGC.allFrames[selectedFrame].mainSprite, typeof(Sprite), true, GUILayout.Width(112), GUILayout.Height(64));
                            EditorGUILayout.LabelField("X: " + selectedGC.allFrames[selectedFrame].mainPosX + " Y: " + selectedGC.allFrames[selectedFrame].mainPosY);
                            EditorGUILayout.LabelField("Flip X: " + selectedGC.allFrames[selectedFrame].GlobalFlipX + " Flip Y: " + selectedGC.allFrames[selectedFrame].GlobalFlipX);
                        }
                        for (int j = 0; j < ac.animations[i].frames.Length; j++)
                        {
                            ac.animations[i].foldFrames[j] = EditorGUILayout.Foldout(ac.animations[i].foldFrames[j], "Frame " + (j + 1));
                            if (ac.animations[i].foldFrames[j])
                            {
                                EditorGUI.indentLevel++;
                                if (GUILayout.Button("Add"))
                                {
                                    ac.animations[i].frames[j].frames.Add(selectedGC.allFrames[selectedFrame]);
                                }

                                foreach (frame f in ac.animations[i].frames[j].frames)
                                {
                                    f.fold = EditorGUILayout.Foldout(f.fold, f.name);
                                    if (f.fold)
                                    {
                                        EditorGUI.indentLevel++;

                                        EditorGUILayout.ObjectField(f.mainSprite, typeof(Sprite), true, GUILayout.Width(128), GUILayout.Height(64));
                                        EditorGUILayout.LabelField("X: " + f.mainPosX + " Y: " + f.mainPosY);
                                        EditorGUILayout.LabelField("Flip X: " + f.GlobalFlipX + " Flip Y: " + f.GlobalFlipX);
                                        int ind = ac.animations[i].frames[j].frames.IndexOf(f);
                                        if (GUILayout.Button("Change"))
                                        {
                                            ac.animations[i].frames[j].frames[ind] = selectedGC.allFrames[selectedFrame];
                                        }
                                        if (GUILayout.Button("Refresh"))
                                        {
                                            ac.animations[i].frames[j].frames[ind] = selectedGC.allFrames[ac.animations[i].frames[j].frames[ind].index];
                                        }
                                        if (GUILayout.Button("Delete"))
                                        {
                                            ac.animations[i].frames[j].frames.RemoveAt(ind);
                                            break;
                                        }
                                        if (ind != 0 && GUILayout.Button("Up"))
                                        {
                                            frame faux = ac.animations[i].frames[j].frames[ind - 1];
                                            ac.animations[i].frames[j].frames[ind - 1] = f;
                                            ac.animations[i].frames[j].frames[ind] = faux;
                                        }
                                        if (ind != ac.animations[i].frames[j].frames.Count - 1 && GUILayout.Button("Down"))
                                        {
                                            frame faux = ac.animations[i].frames[j].frames[ind + 1];
                                            ac.animations[i].frames[j].frames[ind + 1] = f;
                                            ac.animations[i].frames[j].frames[ind] = faux;
                                        }
                                        if (GUILayout.Button("Use"))
                                        {
                                            f.Use();
                                        }

                                        EditorGUI.indentLevel--;
                                    }
                                }

                                EditorGUI.indentLevel--;
                                ac.animations[i].nexts[j] = EditorGUILayout.IntField("Next", ac.animations[i].nexts[j]);
                                ac.animations[i].times[j] = EditorGUILayout.IntField("Time", ac.animations[i].times[j]);
                                ac.animations[i].xFlips[j] = EditorGUILayout.Toggle("X Flip", ac.animations[i].xFlips[j]);
                                ac.animations[i].yFlips[j] = EditorGUILayout.Toggle("Y Flip", ac.animations[i].yFlips[j]);
                            }
                        }

                    }
                       

                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }
        EditorUtility.SetDirty(target);
    }
}
