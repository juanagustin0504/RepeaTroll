using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageView : MonoBehaviour {

    public Text _stageText;

    private CanvasGroup _canvasGroup;

    private void Start() {
        transform.gameObject.SetActive(true);
        _stageText.text = "STAGE " + GameManager.level;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
        StartCoroutine("AlphaControl");
        
    }

    IEnumerator AlphaControl() {

        yield return new WaitForSeconds(1f);
        float i = 1;
        while (i > 0) {
            i -= Time.deltaTime;
            _canvasGroup.alpha = i;

            yield return null;
        }
        transform.gameObject.SetActive(false);
    }
}
