using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public enum TEAM { BLACK, WHITE, RED };
    public static int[] orderNum = new int[]{0,1,2};
    
    [SerializeField] private Button[] btnComponents;
    private TextMeshProUGUI[] numberTexts = new TextMeshProUGUI[3];

    private void Awake()
    {
        for (int i = 0; i < btnComponents.Length; i++)
            numberTexts[i] = btnComponents[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private int iterNum = 0;
    public void SetOrder(int _team)
    {
        iterNum++;
        btnComponents[_team].interactable = false;
        numberTexts[_team].gameObject.SetActive(true);
        numberTexts[_team].text = iterNum.ToString();
        orderNum[iterNum-1] = _team;
        Debug.Log("1번: "+orderNum[0]+" 2번: "+orderNum[1]+" 3번: "+orderNum[2]);

        if (iterNum == 3)
            SceneManager.LoadScene(1);
    }
}
