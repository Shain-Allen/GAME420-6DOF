using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out ShipController player))
            return;
            
        LapManager.Instance.OnGateCrossed?.Invoke(gameObject);
    }
}
