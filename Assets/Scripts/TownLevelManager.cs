using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TownLevelManager : MonoBehaviour {

    private void Start() {
        Cursor.visible = false;
        AudioManager.instance.PlayTownTheme();
        InputReader.Start();
        Player.SetGameStateToWalk();
        Player.Animator.SetBool("Mike", false);
    }

    private void Update() {
        InputReader.Update();
    }

    public static void LoadNextScene() {
        SceneManager.LoadScene("Hill");
    }
}
