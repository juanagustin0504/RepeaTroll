using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour {

    public GameObject[] _panel;
    public GameObject _help;

    private int _index;

    private void Start() {
        for(int i = 0; i < _panel.Length; i++) {
            _panel[i].SetActive(false);
        }
    }

    public void OpenPage() {
        SoundManager.instance.PlayClick();
        _help.SetActive(false);
        _panel[0].SetActive(true);

    }

    public void NextPage() {
        SoundManager.instance.PlayClick();
        _panel[_index].SetActive(false);
        _panel[++_index].SetActive(true);

    }

    public void BackPage() {
        SoundManager.instance.PlayClick();
        _panel[_index].SetActive(false);
        _panel[--_index].SetActive(true);

    }

    public void ClosePage() {
        SoundManager.instance.PlayClick();
        _help.SetActive(true);
        _panel[_index].SetActive(false);
        _index = 0;

    }


    
}
