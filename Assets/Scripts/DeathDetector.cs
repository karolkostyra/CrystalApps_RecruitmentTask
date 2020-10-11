using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    public delegate void DeathZoneContact();
    public static event DeathZoneContact OnDeathZoneContact;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            OnDeathZoneContact?.Invoke();
        }
    }
}
