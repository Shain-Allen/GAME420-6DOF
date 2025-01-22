using System;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    private Dictionary<string, float> gateCrossTimes = new Dictionary<string, float>();
    public List<GameObject> gates;
    float totalTime;
    
    public static Action<GameObject> OnGateCrossed;


    private void Awake()
    {
        OnGateCrossed += GateCrossed;
    }

    private void GateCrossed(GameObject gate)
    {
        
    }
}
