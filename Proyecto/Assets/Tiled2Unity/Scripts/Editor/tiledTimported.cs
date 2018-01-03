using UnityEngine;
using System.Collections.Generic;
using Tiled2Unity;

[CustomTiledImporter]
public class tiledTimported : ICustomTiledImporter
{
    layer l;
    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties)
    {
        gameObject.AddComponent<SpriteRenderer>();
        //int x = (int)gameObject.transform.position.x;
        //int y = (int)gameObject.transform.position.y;

        //foreach (string s in customProperties.Keys) Debug.Log(s);
    }

    public void CustomizePrefab(GameObject prefab)
    {
        prefab.AddComponent<layer>();

        l = prefab.GetComponent<layer>();

        TiledMap tm = prefab.GetComponent<TiledMap>();

        l.blockSize = tm.TileHeight;

        l.width = tm.NumTilesWide;
        l.height = tm.NumTilesHigh;

        l.map = new int[l.width * l.height];

        for(int i = 0;i<l.width;i++)
        {
            for (int j = 0; j < l.height; j++)
            {
                l.map[i + (l.width * j)] = -1;
            }
        }
    }
}
