using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(layer))]
public class layerEditorEscene : Editor
{

    void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        layer t = target as layer;

        if (t.show)
        {
            if (Event.current.type == EventType.MouseMove || Event.current.type == EventType.MouseDrag)
            {
                Vector3 v = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
                int X = t.getXPos(v.x);
                int Y = t.getXPos(v.y);

                if (X >= 0 && X < t.width && Y >= 0 && Y < t.height)
                {
                    t.selectedX = X;
                    t.selectedY = Y;
                }

            }

            if (Event.current.type == EventType.MouseDown && t.state == 0)
            {
                t.initX = t.selectedX;
                t.initY = t.selectedY;
                t.selectionStart = true;
                t.state = 1;
            }
            else if (Event.current.type == EventType.MouseUp && t.state == 1)
            {
                t.endX = t.selectedX;
                t.endY = t.selectedY;
                t.state = 2;
            }
            else if (Event.current.type == EventType.MouseUp && t.state == 2)
            {
                t.selectionStart = false;
                t.state = 0;
            }

            if (t.selectionStart)
            {
                int ex = t.selectedX;
                if (t.state == 2) ex = t.endX;
                int ey = t.selectedY;
                if (t.state == 2) ey = t.endY;

                int maxX = Mathf.Max(t.initX, ex);
                int maxY = Mathf.Max(t.initY, ey);
                int minX = Mathf.Min(t.initX, ex);
                int minY = Mathf.Min(t.initY, ey);

                Handles.DrawSolidRectangleWithOutline(new Rect(minX * t.blockSize, minY * t.blockSize,
                    ((maxX - minX) * t.blockSize) + t.blockSize, ((maxY - minY) * t.blockSize) + t.blockSize),
                    new Color32(0, 0, 128, 128), new Color32(0, 0, 224, 128));
            }

            List<Vector3> vs = new List<Vector3>();

            for (int i = 0; i < t.width; i++)
            {
                vs.Add(new Vector3(t.x + i * t.blockSize, t.y, -10));
                vs.Add(new Vector3(t.x + i * t.blockSize, t.y - (t.height * t.blockSize), -10));
            }

            for (int i = 0; i < t.height; i++)
            {
                vs.Add(new Vector3(t.x, t.y - t.height * t.blockSize + i * t.blockSize, -10));
                vs.Add(new Vector3(t.x + (t.width * t.blockSize), t.y - t.height * t.blockSize + i * t.blockSize, -10));
            }

            Handles.DrawLines(vs.ToArray());

            if (t.map != null)
            {
                if (t.map.Length == 0)
                {
                    t.map = new int[t.width * t.height];
                    for (int i = 0; i < t.width; i++)
                    {
                        for (int j = 0; j < t.height; j++)
                        {
                            t.map[i + (t.width * j)] = -1;
                        }
                    }
                }
                float x, y;
                Color c;
                Vector3[] vertices, newVert;
                int blockInd;
                Vector3 v;
                for (int i = 0; i < t.width; i++)
                {
                    for (int j = 0; j < t.height; j++)
                    {
                        if (t.map[i + (t.width * j)] >= 0 && layer.AllBlocks != null && layer.AllBlocks.Length > t.map[i + (t.width * j)])
                        {
                            x = t.x + (i * t.blockSize) + (t.blockSize / 2);
                            y = t.y - t.height * t.blockSize + ((j * t.blockSize) + (t.blockSize / 2));

                            v = new Vector3(x, y, 0);

                            blockInd = t.map[i + (t.width * j)];

                            if (blockInd >= 0)
                            {
                                c = layer.AllBlocks[blockInd].Color;
                                vertices = layer.AllBlocks[blockInd].Vertices;
                                newVert = new Vector3[vertices.Length];

                                for (int p = 0; p < vertices.Length; p++)
                                {
                                    newVert[p] = vertices[p] + v;
                                }

                                Handles.color = c;
                                Handles.DrawAAConvexPolygon(newVert);
                            }
                        }
                    }
                }
            }
            else
            {
                t.map = new int[t.width * t.height];

                for (int i = 0; i < t.width; i++)
                {
                    for (int j = 0; j < t.height; j++)
                    {
                        t.map[i + (t.width * j)] = -1;
                    }
                }
            }

            EditorUtility.SetDirty(target);
        }
    }
}