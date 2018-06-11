using UnityEngine;

public class TimeController : MonoBehaviour {

    public float playedTime;
    public float elapsedTime;

	void Awake ()
    {
        playedTime = 0f;
        elapsedTime = Time.realtimeSinceStartup;
        Debug.Log("Elapsed Time (Start) " + elapsedTime);
	}

    private void Update()
    {
        playedTime += Time.deltaTime;
    }

    public void updateScore()
    {
        float minTime = PlayerPrefs.GetFloat("minTime");
        float maxTime = PlayerPrefs.GetFloat("maxTime");

        PlayerPrefs.SetFloat("lastTime", playedTime);

        if(minTime == 0f)
            PlayerPrefs.SetFloat("minTime", playedTime);
        else if (playedTime < minTime)
            PlayerPrefs.SetFloat("minTime", playedTime);

        if (playedTime > maxTime)
            PlayerPrefs.SetFloat("maxTime", playedTime);
    }

    public float getElapsedGenerationTime()
    {
        Debug.Log("El tiempo fue " + (Time.realtimeSinceStartup - elapsedTime));
        return Time.realtimeSinceStartup - elapsedTime;
    }
}
