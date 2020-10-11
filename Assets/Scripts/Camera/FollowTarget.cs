using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    private float startYPosition;

    private void Awake()
    {
        startYPosition = transform.position.y;
    }

    private void Start()
    {
        transform.position = new Vector3(targetToFollow.position.x,
                                         startYPosition,
                                         targetToFollow.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(targetToFollow.position.x,
                                         targetToFollow.position.y + startYPosition,
                                         targetToFollow.position.z);
    }
}
