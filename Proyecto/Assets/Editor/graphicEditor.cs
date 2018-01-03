using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(graphicComponent))]
public class graphicEditor : Editor
{
    public Texture2D txt;
    Sprite[] sprites;
    bool foldFrames;
    public enum GraphicImport { Main,XFlipped,YFlipped,XYFlipped,
                                BlaxMain, BlaxXFlipped, BlaxYFlipped, BlaxXYFlipped};
    GraphicImport gi;
    int startNum, endNum,startingIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        graphicComponent gc = (graphicComponent)target;
        GUILayout.Label("Frames");
        EditorGUI.indentLevel++;

        int numFrames = 0;
        if (gc.allFrames != null) numFrames = gc.allFrames.Length;
        numFrames = EditorGUILayout.IntField("Frame count", numFrames);

        if (numFrames < 0) numFrames = 0;
        if (gc.allFrames != null && numFrames != gc.allFrames.Length)
        {
            frame[] newf = new frame[numFrames];
            for (int i = 0; i < newf.Length; i++)
            {
                if(i < gc.allFrames.Length)
                {
                    newf[i] = gc.allFrames[i];
                }
                else
                {
                    newf[i] = new frame(gc, i);
                }
            }
            
            gc.allFrames = newf;
        }

        if(GUILayout.Button("Add a Frame"))
        {
            frame[] newf;
            if (gc.allFrames!=null)
            {
                newf = new frame[gc.allFrames.Length + 1];

                for (int i = 0; i < gc.allFrames.Length; i++)
                {
                    newf[i] = gc.allFrames[i];
                }
            }
            else
            {
                newf = new frame[1];
            }

            newf[newf.Length - 1] = new frame(gc, newf.Length - 1);

            gc.allFrames = newf;
        }

        foldFrames = EditorGUILayout.Foldout(foldFrames, "Show Frames");
        if(foldFrames)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < gc.allFrames.Length; i++)
            {
                gc.allFrames[i].fold = EditorGUILayout.Foldout(gc.allFrames[i].fold, gc.allFrames[i].name);
                if(gc.allFrames[i].fold)
                {
                    EditorGUILayout.LabelField("Index "+ gc.allFrames[i].index);
                    gc.allFrames[i].name = EditorGUILayout.TextField("Name", gc.allFrames[i].name);
                    gc.allFrames[i].canChangeSprite = EditorGUILayout.Toggle("Change Sprite?", gc.allFrames[i].canChangeSprite);
                    gc.allFrames[i].haveBlaxVersion = EditorGUILayout.Toggle("Blax Version?", gc.allFrames[i].haveBlaxVersion);
                    if (gc.allFrames[i].canChangeSprite)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.LabelField("Main Sprite");
                        EditorGUI.indentLevel++;
                        EditorGUILayout.LabelField("Not Flipped");
                        gc.allFrames[i].mainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].mainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                        gc.allFrames[i].specialXFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].specialXFlipSprite, "X Flipped Sprite");
                        if(gc.allFrames[i].specialXFlipSprite)
                            gc.allFrames[i].xfMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].xfMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                        gc.allFrames[i].specialYFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].specialYFlipSprite, "Y Flipped Sprite");
                        if (gc.allFrames[i].specialYFlipSprite)
                            gc.allFrames[i].yfMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].yfMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                        gc.allFrames[i].specialXYFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].specialXYFlipSprite, "XY Flipped Sprite");
                        if (gc.allFrames[i].specialXYFlipSprite)
                            gc.allFrames[i].xyfMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].xyfMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));
                        EditorGUI.indentLevel--;
                        if (gc.allFrames[i].haveBlaxVersion)
                        {
                            EditorGUILayout.LabelField("Blax Version Sprite");
                            gc.allFrames[i].blaxVersion = EditorGUILayout.Foldout(gc.allFrames[i].blaxVersion, "Blax Version");
                            if(gc.allFrames[i].blaxVersion)
                            {
                                EditorGUI.indentLevel++;
                                EditorGUILayout.LabelField("Not Flipped");
                                gc.allFrames[i].blaxMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].blaxMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                                gc.allFrames[i].blaxSpecialXFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].blaxSpecialXFlipSprite, "X Flipped Sprite");
                                if (gc.allFrames[i].blaxSpecialXFlipSprite)
                                    gc.allFrames[i].blaxXFMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].blaxXFMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                                gc.allFrames[i].blaxSpecialYFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].blaxSpecialYFlipSprite, "Y Flipped Sprite");
                                if (gc.allFrames[i].blaxSpecialYFlipSprite)
                                    gc.allFrames[i].blaxYFMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].blaxYFMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));

                                gc.allFrames[i].blaxSpecialXYFlipSprite = EditorGUILayout.Foldout(gc.allFrames[i].blaxSpecialXYFlipSprite, "XY Flipped Sprite");
                                if (gc.allFrames[i].blaxSpecialXYFlipSprite)
                                    gc.allFrames[i].blaxXYFMainSprite = (Sprite)EditorGUILayout.ObjectField(gc.allFrames[i].blaxXYFMainSprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(64));
                                EditorGUI.indentLevel--;
                            }
                        }
                        
                        EditorGUI.indentLevel--;
                    }
                    gc.allFrames[i].GlobalFlipX = EditorGUILayout.Toggle("Flip X", gc.allFrames[i].GlobalFlipX);
                    gc.allFrames[i].GlobalFlipY = EditorGUILayout.Toggle("Flip Y", gc.allFrames[i].GlobalFlipY);
                    gc.allFrames[i].canChangeX = EditorGUILayout.Toggle("Change X?", gc.allFrames[i].canChangeX);
                    if(gc.allFrames[i].canChangeX)gc.allFrames[i].mainPosX = EditorGUILayout.FloatField("X", gc.allFrames[i].mainPosX);
                    gc.allFrames[i].canChangeY = EditorGUILayout.Toggle("Change Y?", gc.allFrames[i].canChangeY);
                    if (gc.allFrames[i].canChangeY) gc.allFrames[i].mainPosY = EditorGUILayout.FloatField("Y", gc.allFrames[i].mainPosY);
                    if (GUILayout.Button("Use"))
                    {
                        gc.allFrames[i].Use();
                    }
                }
            }
        }
        EditorGUI.indentLevel = 0;
        if (txt != null) txt = (Texture2D)EditorGUILayout.ObjectField(txt, typeof(Texture2D), false, GUILayout.Width(304), GUILayout.Height((304f / txt.width) * txt.height));
        else txt = (Texture2D)EditorGUILayout.ObjectField(txt, typeof(Texture2D), false, GUILayout.Width(288), GUILayout.Height(96));
        sprites = null;
        if (txt != null)
        {
            string path = AssetDatabase.GetAssetPath(txt);
            sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
        }
        gi = (GraphicImport)EditorGUILayout.EnumPopup("Graphic Import", gi);
        startNum = EditorGUILayout.IntField("Start Index", startNum);
        endNum = EditorGUILayout.IntField("End Index", endNum);
        startingIndex = EditorGUILayout.IntField("Starting Index", startingIndex);

        if (txt != null && GUILayout.Button("Get All Sprites"))
        {
            if (startNum < 0) startNum = 0;
            if (startNum >= sprites.Length) startNum = sprites.Length - 1;
            if (endNum < startNum) endNum = startNum;
            if (endNum >= sprites.Length) endNum = sprites.Length - 1;
            if (startingIndex < 0) startingIndex = 0;

            frame[] allf = new frame[Mathf.Max(startingIndex + endNum + 1 - startNum, gc.allFrames.Length)];
            frame faux;
            for (int i = startNum, j = 0, k = 0; j < allf.Length; j++, k++)
            {
                if (j < startingIndex || j > startingIndex + endNum - startNum)
                {
                    allf[j] = gc.allFrames[j];
                    k++;
                }
                else
                {
                    faux = new frame(gc, j);
                    faux.name = txt.name + j;
                    faux.canChangeSprite = true;

                    allf[j] = faux;

                    switch (gi)
                    {
                        case GraphicImport.Main:
                            allf[j].mainSprite = sprites[i];
                            break;
                        case GraphicImport.XFlipped:
                            allf[j].xfMainSprite = sprites[i];
                            break;
                        case GraphicImport.YFlipped:
                            allf[j].yfMainSprite = sprites[i];
                            break;
                        case GraphicImport.XYFlipped:
                            allf[j].xyfMainSprite = sprites[i];
                            break;
                        case GraphicImport.BlaxMain:
                            allf[j].blaxMainSprite = sprites[i];
                            break;
                        case GraphicImport.BlaxXFlipped:
                            allf[j].blaxXFMainSprite = sprites[i];
                            break;
                        case GraphicImport.BlaxYFlipped:
                            allf[j].blaxYFMainSprite = sprites[i];
                            break;
                        case GraphicImport.BlaxXYFlipped:
                            allf[j].blaxXYFMainSprite = sprites[i];
                            break;
                    }
                    i++;
                }
               
            }
            gc.allFrames = allf;
        }
        EditorUtility.SetDirty(target);
    }
}
