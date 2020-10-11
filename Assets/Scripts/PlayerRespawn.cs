using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 startPosition;
    private Quaternion startRotation;

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
        player.transform.position = startPosition;
        player.transform.rotation = startRotation;
    }
}
