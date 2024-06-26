using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]TMP_Text infoText;
    [SerializeField]Button replayButton;

    private void Start(){
        HideInfo();
        HideReplayButton();
    }

    public void DisplayInfo(string message){
        infoText?.gameObject.SetActive(true);
        infoText.SetText(message);
    }

    public void HideInfo(){
        infoText?.gameObject.SetActive(false);
    }

    public void ShowReplayButton(){
        replayButton?.gameObject.SetActive(true);
    }

    public void HideReplayButton(){
        replayButton?.gameObject.SetActive(false);
    }
}
