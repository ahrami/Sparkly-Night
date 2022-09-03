using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VeryEnd : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _text;

    private void Start() {
        _text.text = "You got " + Player.Tangerines + "/30 tangerines";
    }

    private void Update() {
        
        if (InputReader.GetActionKey()) {
            Application.Quit();
        }
    }
}
