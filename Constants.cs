using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const float cameraSize = 5f;     // screen vertical height == 5 units
    
    private const float maxFallingSpeedNormalized = 1.2f;
    public static float maxFallingSpeed { get => maxFallingSpeedNormalized * cameraSize; }

    private const float gravityAccelerationNormalized = 0.8f;  
    public static float gravityAcceleration { get => gravityAccelerationNormalized * cameraSize; }

    private const float angleToSpeedNormalized = 0.006f;
    public static float angleToSpeed { get => angleToSpeedNormalized * cameraSize; }

    // Jump
    private const float jumpSpeedNormalized = 0.5f;
    public static float jumpSpeed { get => jumpSpeedNormalized * cameraSize; }

    private const float highestDistanceNormalized = 0.4f;
    public static float highestDistance { get => highestDistanceNormalized * cameraSize; }

    // Dump
    private const float dumpSpeedNormalized = 0.2f;
    public static float dumpSpeed { get => dumpSpeedNormalized * cameraSize; }

    private const float oneLevelHeightNormalized = 0.2f;
    public static float oneLevelHeight { get => oneLevelHeightNormalized * cameraSize; }

    // === Boss ===
    private const float verticalJumpDistanceNormalized = 0.3f;
    public static float verticalJumpDistance { get => verticalJumpDistanceNormalized * cameraSize; }

    private const float horizontalJumpDistanceNormalized = 0.14f;
    public static float horizontalJumpDistance { get => horizontalJumpDistanceNormalized * cameraSize; }

    private const float bossAttackRangeNormalized = 0.26f;
    public static float bossAttackRange { get => bossAttackRangeNormalized * cameraSize; }

    // === Map ===
    
}
