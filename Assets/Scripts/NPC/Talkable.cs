using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]

abstract public class Talkable : MonoBehaviour {

    protected struct Stage {
        public string[] stage;
        public int Length { get { return stage.Length; } }
		public Stage(string[] init) {
            stage = init;
		}
        public string this[int i] {
            get { return stage[i]; }
        }
    }

    protected Stage[] _dialogue;

    protected int _phraze = 0;
    protected bool _waitingForChoice = false;

    public abstract string Next();

    protected abstract void EndOfStage(bool choice = false);

    protected abstract void StartOfStage();

    public abstract void Choice(bool choice);

    protected IEnumerator WaitForEnd() {
        while (TextAppear.Appearing) {
            yield return new WaitForSeconds(0.01f);
        }
        EndOfStage();
    }

    public virtual void Action() {
        
    }
}
