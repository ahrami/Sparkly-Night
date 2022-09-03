using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour {

    [SerializeField] private GameObject _pausePanel;
    private bool _pause = false;

    [SerializeField] private TextMeshProUGUI _resume;
    [SerializeField] private TextMeshProUGUI _quit;
    [SerializeField] private TextMeshProUGUI _tangerines;
    private int _state = 0;

    private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (_pause) {
                UnPause();
            } else {
                _tangerines.text = "Tangerines: " + Player.Tangerines.ToString();
                _pausePanel.SetActive(true);
                _pause = true;
                Time.timeScale = 0;
            }
		}
		if (_pause) {
            if (InputReader.GetInputVector().y == 1) {
                SwitchState(0);
            } else if (InputReader.GetInputVector().y == -1) {
                SwitchState(1);
            }
            if (InputReader.GetActionKey()) {
                switch (_state) {
                    case 0:
                        UnPause();
                        break;
                    case 1:
                        Application.Quit();
                        break;
                    default: break;
                }
            }
        }
    }

    private void UnPause() {
        SwitchState(0);
        _pausePanel.SetActive(false);
        _pause = false;
        Time.timeScale = 1;
    }

    private void SwitchState(int state) {
        switch (state) {
            case 0:
                _state = 0;
                _resume.fontStyle = FontStyles.UpperCase | FontStyles.Underline;
                _quit.fontStyle = FontStyles.UpperCase;
                break;
            case 1:
                _state = 1;
                _resume.fontStyle = FontStyles.UpperCase;
                _quit.fontStyle = FontStyles.UpperCase | FontStyles.Underline;
                break;
            default: break;
        }
    }
}
