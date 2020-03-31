using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{

    public Text text;
    int framesCount = 0;
    float timer = 0f;

    private void Start()
    {
        //Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (timer >= 1f)
        {
            text.text = framesCount.ToString();
            framesCount = 0;
            timer = 0f;
        }
        framesCount++;
        timer += Time.deltaTime;
    }
}
