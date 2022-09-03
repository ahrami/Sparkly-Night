using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class IceLevelManager : MonoBehaviour {

    public static IceLevelManager instance;

    public static int Tangerines;
    private static GameObject _tangerines;

    private Image _panel;

    [SerializeField] private Talkable _mike;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        Cursor.visible = false;
        AudioManager.instance.PlayLakeTheme();
        _tangerines = GameObject.Find("Tangerines");
        Tangerines = Player.Tangerines;
        InputReader.Start();
        Player.SetGameStateToWalk();
        Player.Animator.SetBool("Mike", true);
        Player.Animator.Play("Idle front 0");
        Stages.Mike = 7;
        GameObject.Find("Player").GetComponent<Talk>().EnterTalk(_mike);
        _panel = GameObject.Find("FadePanel").GetComponent<Image>();
    }

    private void Update() {
        InputReader.Update();
    }

    public static void Reset() {
        for (int i = 0; i < _tangerines.transform.childCount; ++i) {
            _tangerines.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void LoadNextScene() {
        AudioManager.instance.StopLakeTheme();
        StartCoroutine(FadeOut(3));
    }

    private IEnumerator FadeOut(float time) {
        float a = 0f;
        while (a < 1) {
            a += Time.deltaTime / time;
            if(a > 1) {
                a = 1;
			}
            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, a);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("End");
    }
}
