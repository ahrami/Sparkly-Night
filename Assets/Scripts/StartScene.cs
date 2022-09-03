using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    private void Start() {
        InputReader.Start();
        Cursor.visible = false;
    }

    private void Update() {
        InputReader.Update();
        if (InputReader.GetActionKey()) {
            FindObjectOfType<AudioManager>().PlayMenuTheme();
            SceneManager.LoadScene("Menu");
        }
    }
}
