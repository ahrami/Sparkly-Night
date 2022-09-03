using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

    private Image _panel;

    [SerializeField] private GameObject _hint;

    private void Start() {
        Cursor.visible = false;
        AudioManager.instance.PlayEndTheme();
        _panel = GameObject.Find("FadePanel").GetComponent<Image>();
        StartCoroutine(FadeIn(3));
    }

    private IEnumerator FadeIn(float time) {
        float a = 1f;
        while (a > 0f) {
            a -= Time.deltaTime / time;
            if (a < 0f) {
                a = 0f;
            }
            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, a);
            yield return new WaitForEndOfFrame();
        }
        float start = Time.time;
        while (true) {
            yield return null;
            if (Time.time - start > 2f && Time.time - start < 7f) {
                _hint.SetActive(true);
            } else {
                _hint.SetActive(false);
            }
            if (Time.time - start > 53f - time || InputReader.GetActionKey()) {
                break;
			}
        }
        StartCoroutine(FadeOut(time));
    }

    private IEnumerator FadeOut(float time) {
        AudioManager.instance.StopEndTheme();
        float a = 0f;
        while (a < 1) {
            a += Time.deltaTime / time;
            if (a > 1) {
                a = 1;
            }
            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, a);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("VeryEnd");
    }
}
