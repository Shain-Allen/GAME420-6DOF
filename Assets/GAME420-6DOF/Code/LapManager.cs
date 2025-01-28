using System;
using System.Collections.Generic;
using Singletons;
using UnityEngine;
using UnityEngine.Serialization;

public class LapManager : Singleton<LapManager>
{
    private Dictionary<string, float> _gateCrossTimes;
    public List<GameObject> _gates;
    private float _totalTime;
    private float _lastGateTime;
    private int _currentGateIndex;

    public Action<GameObject> OnGateCrossed;

    private new void Awake()
    {
        base.Awake();
        
        OnGateCrossed += GateCrossed;
        _lastGateTime = Time.time;
        
        _gateCrossTimes = new Dictionary<string, float>();
        _currentGateIndex = 0;
    }

    private void GateCrossed(GameObject gate)
    {
        if (_currentGateIndex >= _gates.Count || gate != _gates[_currentGateIndex])
        {
            Debug.LogWarning($"Incorrect gate order. Expected: {_gates[_currentGateIndex].name}, but crossed: {{gate.name}}");
            return;
        }
        
        float currentTime = Time.time;
        float gateTime = currentTime - _lastGateTime;
        _lastGateTime = currentTime;
        
        _gateCrossTimes.TryAdd(gate.name, gateTime);

        _totalTime += gateTime;
        Debug.Log($"Gate {gate.name} crossed in {gateTime} seconds. Total time: {_totalTime} seconds.");
        _currentGateIndex++;
    }

    public float GetTotalTime()
    {
        return _totalTime;
    }

    public float GetGateTime(string gateName)
    {
        return _gateCrossTimes.GetValueOrDefault(gateName, 0);
    }
}