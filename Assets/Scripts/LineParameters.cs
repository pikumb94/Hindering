using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Line Parameters", menuName = "Hindering/LineParameters")]
public class LineParameters : ScriptableObject
{
    public bool useWorldSpace;
    public Material material;
    public float increaseFactor;
    public float startWidth;
    public float endWidth;
    public Color startColor;
    public Color endMinColor;
    public Color endMaxColor;
    public int numCapVertices;
}
