using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _yes;
    [SerializeField] private TextMeshProUGUI _no;

    private static bool _appearing = false;
    public static bool Appearing {
        get {
            return _appearing;
        }
		set {
            _appearing = value;
		}
    }

    private static bool _skip = false;
    public static void Skip() {
        _skip = true;
    }

	private void Update() {
		if (InputReader.GetActionKey() && _appearing) {
            Skip();
		}
	}

	public void Text(string text) {
        StartCoroutine(Appear(text));
    }

    private IEnumerator Appear(string text) {
        int i = 0;
        string currentText = string.Empty;
        while (i < text.Length) {
            currentText = currentText.Insert(i, text[i].ToString());
            if(text[i].ToString() == "<") {
                while (i < text.Length && text[i].ToString() != ">") {
                    ++i;
                    currentText = currentText.Insert(i, text[i].ToString());
                }
            }
            ++i;
            _tmp.text = currentText;
            if (_skip) {
                _tmp.text = text;
                break;
            } else {
                yield return new WaitForSeconds(0.03f);
            }
        }
        _skip = false;
        _appearing = false;
    }

    public void Yes() {
        _yes.fontStyle = FontStyles.Normal | FontStyles.Underline;
        _no.fontStyle = FontStyles.Normal;
    }

    public void No() {
        _yes.fontStyle = FontStyles.Normal;
        _no.fontStyle = FontStyles.Normal | FontStyles.Underline;
    }

    public void Clear() {
        _tmp.text = "";
    }
}
