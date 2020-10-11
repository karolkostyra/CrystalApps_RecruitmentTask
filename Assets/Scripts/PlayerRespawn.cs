using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = this.transform.position;
    }

    private void OnEnable()
    {
        DeathDetector.OnDeathZoneContact += Respawn;
    }

    private void OnDisable()
    {
        DeathDetector.OnDeathZoneContact -= Respawn;
    }

    private void Respawn()
    {
        this.transform.position = startPosition;
    }
}
