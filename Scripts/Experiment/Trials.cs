using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trials : MonoBehaviour
{
    public TMP_Text trialText;
    private int mapNumber;

    // Update is called once per frame
    void Update()
    {
        TrialShow();

    }

    private void TrialShow()
    {
        if (ExperimentManager.Instance.currentMapIndex < 4)
        {
            mapNumber = ExperimentManager.Instance.currentMapIndex + 1;
            trialText.color = Color.red;
            trialText.text = mapNumber + "/" + "4"; 
            trialText.color = Color.gray;
        }
        else
        {
            mapNumber = ExperimentManager.Instance.currentMapIndex - 3;
            trialText.text = mapNumber + "/" + "80";
            trialText.color = Color.white; // Reset the color to white if trials count is greater than 4
        }
}
}
