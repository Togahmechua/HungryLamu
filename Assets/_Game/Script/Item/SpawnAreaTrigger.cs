using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaTrigger : MonoBehaviour
{
    [SerializeField] private LamuBehaviour lamu;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = Cache.GetPlayerControllerByCollider(other);
        if (player != null)
        {
            GameManager.Ins.roamArea = base.transform;

            if (UIManager.Ins.threeDObjectiveCanvas != null && UIManager.Ins.threeDObjectiveCanvas.lamuRoaming)
            {
                Invoke(nameof(SpawnInNewArea), 0.3f);
            }
        }
    }

    private void SpawnInNewArea()
    {
        lamu.SpawnRandomSpot();
    }
}
