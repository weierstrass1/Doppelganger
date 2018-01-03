using UnityEngine;
using UnityEditor;

public class layerEditor : EditorWindow
{
    public static layer obj = null;

    public int selectedTileX = 0, selectedTileY = 0;

    public int lastBlock = -1;

    [MenuItem("Window/Layer")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(layerEditor));
        window.position = new Rect(0, 0, 250, 80);
        window.Show();
    }

    void OnGUI()
    {
        obj = (layer)EditorGUI.ObjectField(new Rect(3, 6, position.width - 6, 20), "Layer", obj, typeof(layer), true);
        
        if (obj!=null)
        {

            selectedTileX = EditorGUI.IntField(new Rect(3, 26, position.width - 6, 20), "Selected Tile X", obj.selectedX);
            selectedTileY = EditorGUI.IntField(new Rect(3, 46, position.width - 6, 20), "Selected Tile Y", obj.selectedY);

            EditorGUI.LabelField(new Rect(3, 66, position.width - 6, 20), "Initial X: " + obj.initX);
            EditorGUI.LabelField(new Rect(3, 86, position.width - 6, 20), "Initial Y: " + obj.initY);
            EditorGUI.LabelField(new Rect(3, 106, position.width - 6, 20), "Final X: " + obj.endX);
            EditorGUI.LabelField(new Rect(3, 126, position.width - 6, 20), "Final Y: " + obj.endY);

            if (selectedTileX >= 0 && selectedTileX < obj.width && selectedTileY >= 0 && selectedTileY < obj.height)
            {
                if(obj.map == null)
                {
                    obj.map = new int[obj.width * obj.height];

                    for (int i = 0; i < obj.width; i++)
                    {
                        for (int j = 0; j < obj.height; j++)
                        {
                            obj.map[i + (obj.width * j)] = -1;
                        }
                    }
                }

                if(obj.state == 2)
                {
                    int maxX = Mathf.Max(obj.initX, obj.endX);
                    int maxY = Mathf.Max(obj.initY, obj.endY);

                    string[] blocksTypes = new string[layer.AllBlocks.Length + 1];

                    blocksTypes[0] = "Empty Block";

                    for (int i = 1; i < blocksTypes.Length; i++)
                    {
                        blocksTypes[i] = layer.AllBlocks[i - 1].BlockName;
                    }

                    lastBlock = EditorGUI.Popup(new Rect(3, 186, 300, 20), lastBlock, blocksTypes);

                    for (int i = Mathf.Min(obj.initX, obj.endX); i <= maxX; i++)
                    {
                        for (int j = Mathf.Min(obj.initY, obj.endY); j <= maxY; j++)
                        {
                            obj.map[i + (obj.width * j)] = lastBlock - 1;
                        }
                    }
                    
                }
                
                    
            }
            
        }

        
    }
}
