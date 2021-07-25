using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MapHandler : MonoBehaviour
{
    GameObject backgroundPrefab;
    GameObject backgroundObject;

    public Transform playerTransform;
    public Transform cameraTransform;

    public MapSegment oldMapSegment;
    public MapSegment curMapSegment;
    public MapSegment nextMapSegment;

    private float lowerBoundary;

    //----------------------------------------------------------------------------------------------
    void Awake()
    {
        // Background
        backgroundPrefab = Resources.Load<GameObject>("Prefabs/background");
        Assert.IsNotNull(backgroundPrefab);
        backgroundObject = Instantiate(backgroundPrefab, new Vector3(0f, 0f, 10f), Quaternion.identity);

        // Init levels
        oldMapSegment = new MapSegment(0f);
        curMapSegment = new MapSegment(-MapSegment.height);
        nextMapSegment = new MapSegment(-2f * MapSegment.height);
        lowerBoundary = MapSegment.height * -2f;
    }

    //----------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // Background is aligned with camera
        backgroundObject.transform.position = 
            new Vector3(cameraTransform.position.x,
                        cameraTransform.position.y,
                        10f);

        // Check if player is out of boundary
        if (playerTransform.position.y < lowerBoundary)
        {
            oldMapSegment.DeleteSegment();

            oldMapSegment = curMapSegment;
            curMapSegment = nextMapSegment;
            nextMapSegment = new MapSegment(lowerBoundary - MapSegment.height);

            // Update lowerBoundary
            lowerBoundary -= MapSegment.height;
        }
    }
}
