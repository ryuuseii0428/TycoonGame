using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject unitPrefab;  // 생성할 유닛의 프리팹
    public float timeRemaining = 5; // 초기 시간 설정

    public float num = 0;
    public Text timeText; // UI Text
    private bool isTimerRunning = false; // 타이머 작동 여부

    private float elapsedTime = 0f;

    void Start()
    {
        // PlayerPrefs에서 저장된 시간 정보를 불러옴
         float previousTime = PlayerPrefs.GetFloat("previousTime");
         float playerNum = PlayerPrefs.GetFloat("playerNum");
        if(playerNum > 0)
        {
            num = playerNum;

            for (int i = 0; i < num; i++)
                {
                    Instantiate(unitPrefab, transform.position, Quaternion.identity);    
                }
        }
        if (previousTime > 0)
        {
            timeRemaining = previousTime;
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                PlayerPrefs.SetFloat("previousTime", timeRemaining);
                DisplayTime(timeRemaining);
            }
            else if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                Debug.Log("Time has run out!");
                Instantiate(unitPrefab, transform.position, Quaternion.identity);
                num += 1;
                isTimerRunning = false;
                timeRemaining = 5;
                
            }
        }

        if(num > 0)
        {
            PlayerPrefs.SetFloat("playerNum",num);
        }
    }

    // 시간 정보를 UI Text에 표시
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 타이머 시작 버튼에 연결될 함수
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // 타이머 중지 버튼에 연결될 함수
    public void StopTimer()
    {
        isTimerRunning = false;

        // PlayerPrefs에 현재 시간을 저장
        PlayerPrefs.SetFloat("SavedTime", timeRemaining);
        PlayerPrefs.Save();
    }
}