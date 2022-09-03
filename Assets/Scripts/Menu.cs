using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _play;
    [SerializeField] private TextMeshProUGUI _quit;
    private int _state = 0;

    private void Start() {
        Cursor.visible = false;
        InputReader.Start();
    }

    private void Update() {
        InputReader.Update();
        if(InputReader.GetInputVector().y == 1) {
            _state = 0;
            _play.fontStyle = FontStyles.UpperCase | FontStyles.Underline;
            _quit.fontStyle = FontStyles.UpperCase;
        } else if (InputReader.GetInputVector().y == -1) {
            _state = 1;
            _play.fontStyle = FontStyles.UpperCase;
            _quit.fontStyle = FontStyles.UpperCase | FontStyles.Underline;
        }
		if (InputReader.GetActionKey()) {
			switch (_state) {
                case 0:
                    SceneManager.LoadScene("Town");
                    break;
                case 1:
                    Application.Quit();
                    break;
                default: break;
			}
		}
    }


}
