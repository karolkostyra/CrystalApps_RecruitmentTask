using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Color color;

    public float startTime;
    private string currentTime;

    private void Start()
    {
        timerText.color = color;
    }

    void Update()
    {
        startTime += Time.deltaTime;
        timerText.text = "Time: " + string.Format("{0:0.0}", startTime);
    }
}