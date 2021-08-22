using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A MapSegment is a set of levels. Each MapSegment has the same local patern.

public class MapSegment
{
    private const float offsetX = 2f;
    private const float distBtwLevels = 1.2f;
    private const int numLevels = 6;
    public const float height = numLevels * distBtwLevels;

    private List<GameObject> levelList = new List<GameObject>();

    public MapSegment(float startY)
    {
        GameObject levelPrefab = Resources.Load<GameObject>("Prefabs/level");
        Assert.IsNotNull(levelPrefab);
        
        float isPositive = 1;
        for (int i = 0; i < numLevels; ++i)
        {
            GameObject levelObject = UnityEngine.Object.Instantiate(
                levelPrefab,
                new Vector3(offsetX * isPositive, startY - i * distBtwLevels, 1f),
                Quaternion.identity);
            isPositive *= -1f;
            levelList.Add(levelObject);
        }
    }

    public void DeleteSegment()
    {
        foreach(GameObject level in levelList)
        {
            UnityEngine.Object.Destroy(level);
        }
    }
}
