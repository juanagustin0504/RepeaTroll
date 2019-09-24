using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestStageScript : MonoBehaviour {

    public Text _bestStageLabel;

    private int _bestStage;

    private void Update() {

        _bestStage = PlayerPrefs.GetInt("BestStage");

        _bestStageLabel.text = "STAGE " + _bestStage;

    }
}
