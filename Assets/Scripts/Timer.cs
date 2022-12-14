using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private const float time = 10f;
    private float timeLeft;
    private float interval = 1;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = time;
        InvokeRepeating("Countdown", interval, interval);
    }

    void Countdown()
    {
        if (timeLeft > 0)
        {
            timeLeft -= 1;
        }

        if (timeLeft == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(scene.buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            timeLeft = time;            
        }
    }
}
