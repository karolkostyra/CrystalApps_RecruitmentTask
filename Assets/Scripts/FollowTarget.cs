using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    
    void Update()
    {
        transform.position = new Vector3(targetToFollow.position.x,
                                         transform.position.y,
                                         targetToFollow.position.z);
    }
}
