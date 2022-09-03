using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SkiingManager : MonoBehaviour {

    public static int Tangerines;
    private static GameObject _tangerines;

    [SerializeField] private Talkable _mike;

    private void Start() {
        Cursor.visible = false;
        AudioManager.instance.PlayHillTheme();
        _tangerines = GameObject.Find("Tangerines");
        Tangerines = Player.Tangerines;
        InputReader.Start();
        Player.SetGameStateToSkiing();
        Player.Animator.Play("Ski mid");
        Player.Animator.speed = 0;
        Player.CanMove = false;
        Stages.Mike = 5;
        GameObject.Find("Player").GetComponent<Talk>().EnterTalk(_mike);
    }

    private void Update() {
        InputReader.Update();
    }

    public static void Reset() {
        for (int i = 0; i < _tangerines.transform.childCount; ++i) {
            _tangerines.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public static void LoadNextScene() {
        SceneManager.LoadScene("Lake");
    }
}
