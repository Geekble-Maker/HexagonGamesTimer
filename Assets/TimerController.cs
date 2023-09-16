using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private GameObject parentObj;
    [SerializeField] private GameObject prevParentObj;
    [SerializeField] private GameObject[] panelObjs;
    [SerializeField] private TextMeshProUGUI[] timerTexts;
    [SerializeField] private Vector3[] orderVecs;
    [SerializeField] private float totalTime;
    
    private GameObject[] currentPanelObjs = new GameObject[3];
    private TextMeshProUGUI[] currentTimerTexts = new TextMeshProUGUI[3];
    private float[] currentTime = new float[3];
    
    private int[] orderMapNum;
    private int orderNum = 0;
    private bool isStart = false;
    private bool isPause = false;

    void Start()
    {
        RemapObj(orderVecs[panelOrder]);

        for (int i = 0; i < currentPanelObjs.Length; i++)
        {
            currentTime[i] = totalTime;
            SetTimer(currentTimerTexts[i], currentTime[i]);
        }
    }

    private void RemapObj(Vector3 _orderVec)
    {
        orderMapNum = new int[] { (int)_orderVec.x, (int)_orderVec.y, (int)_orderVec.z };
        
        panelObjs[orderMapNum[0]].transform.SetParent(prevParentObj.transform);
        panelObjs[orderMapNum[1]].transform.SetParent(prevParentObj.transform);
        panelObjs[orderMapNum[2]].transform.SetParent(prevParentObj.transform);
        
        panelObjs[orderMapNum[0]].transform.SetParent(parentObj.transform);
        panelObjs[orderMapNum[1]].transform.SetParent(parentObj.transform);
        panelObjs[orderMapNum[2]].transform.SetParent(parentObj.transform);

        currentPanelObjs[0] = panelObjs[orderMapNum[0]];
        currentPanelObjs[1] = panelObjs[orderMapNum[1]];
        currentPanelObjs[2] = panelObjs[orderMapNum[2]];

        currentTimerTexts[0] = timerTexts[orderMapNum[0]];
        currentTimerTexts[1] = timerTexts[orderMapNum[1]];
        currentTimerTexts[2] = timerTexts[orderMapNum[2]];
    }

    // Update is called once per frame
    void Update()
    {
        SwitchPanelObj();
        SwitchTimer();
        PauseTime();
        
        ChangeTime();
    }

    private int panelOrder = 0;
    private void SwitchPanelObj()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            panelOrder++;
            if (panelOrder > 5) panelOrder = 0;
            RemapObj(orderVecs[panelOrder]);
        }
    }

    private void SwitchTimer()
    {
        if(isPause) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStart)
                isStart = true;
            else
            {
                orderNum++;
                if (orderNum > 2) orderNum = 0;
                Debug.Log(orderNum);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (!isStart)
                isStart = true;
            else
            {
                orderNum--;
                if (orderNum < 0) orderNum = 2;
                Debug.Log(orderNum);
            }
        }
    }

    private void PauseTime()
    {
        if (!isStart) return;
        if (Input.GetKeyDown(KeyCode.P))
            isPause = !isPause;

    }

    private void ChangeTime()
    {
        if (!isStart || isPause) return;
        
        currentTime[orderNum] -= Time.deltaTime;
        SetTimer(currentTimerTexts[orderNum], currentTime[orderNum]);
        if (currentTime[orderNum] < 0)
            currentTime[orderNum] = 0;
    }

    private void SetTimer(TextMeshProUGUI _timerText, float _time)
    {
        TimeSpan timespan = TimeSpan.FromSeconds(_time);
        string timeString = string.Format("{0:00}:{1:00}:{2:00}",
            timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        _timerText.text = timeString;
    }
}
