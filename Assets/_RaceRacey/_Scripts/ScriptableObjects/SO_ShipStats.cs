using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ShipStats", menuName="ShipStats")]
public class SO_ShipStats : ScriptableObject
{
    [Header("Vehicle Settings")]
    [SerializeField] internal float turnSpeed;
    [SerializeField] internal float drift, maxSpeed, enginePower;
    [SerializeField, Tooltip("This moves the Ship to fasters speeds sooner")] internal float torque = 12f;
    [SerializeField, Tooltip("This helps increase the Torque faster from 0"), Range(0.1f, 1f)] internal float torqueRampSpeed = 0.5f;


}
