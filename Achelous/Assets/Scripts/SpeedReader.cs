using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedReader : MonoBehaviour
{
    public TMP_Text speedText;
    public float speedScore;

    // Start is called before the first frame update
    void Start()
    {
        speedScore = 0f;
        speedText.text = speedScore.ToString() + " Speed";
    }

    // Update is called once per frame

    void Update()
    {
        //speedText.text = speedScore.ToString("F2") + " Speed";
        speedText.text = ("Speed: " + speedScore.ToString("F2"));
    }
}
