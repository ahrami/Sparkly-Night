using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mike : Talkable {
    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Hi!",
            "I am Mike the frog.",
            "But you can just call me Mike!"
        }),
        new Stage(new string[]{ // stage 1
            "I lost my Christmas Hat...",
            "Now I am sad."
        }),
        new Stage(new string[]{ // stage 2
            "Sob..."
        }),
        new Stage(new string[]{ // stage 3
            "<i>Offer Mike Help?</i>"
        }),
        new Stage(new string[]{ // stage 4
            "You will go with me to the lake?",
            "Oh my God thank you!",
            "We are going to need skis though.",
            "I think we can ask Cat to give us her skis."
        }),
        new Stage(new string[]{ // stage 5
            "We are going to go down this hill.",
            "The lake is at the bottom.",
            "And...",
            "As Cat said...",
            "Beware of trees!"
        }),
        new Stage(new string[]{ // stage 6
            "There is the lake!",
            "You are such a good skier!",
            "Let's go find my hat now!"
        }),
        new Stage(new string[]{ // stage 7
            "Be careful when you walk on ice...",
            "There are cracks in it.",
            "Don't step on them.",
            "And don't stay too long in one place or ice might break."
        }),
        new Stage(new string[]{ // stage 8
            "There it is!",
            "My hat!",
            "Thank you so much for helping me finding it!",
            "It is almost midnight though...",
            "I am very tired...",
            "Let's rest for a little bit."
        })
    };

    private bool _waitingForAction = false;

    public override string Next() {

        if(_phraze == 0) {
            StartOfStage();
		}

        if (_phraze < _dialogue[Stages.Mike].Length - 1) {
            return _dialogue[Stages.Mike][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Mike][_dialogue[Stages.Mike].Length - 1];
    }

    protected override void EndOfStage(bool yes = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Mike) {
            case 0:
                Stages.Mike = 1;
                break;
            case 1:
                Stages.Mike = 2;
                if (Stages.Blob == 4) {
                    Stages.Blob = 5;
                }
                Talk.ExitTalk();
                break;
            case 2:
                if (Stages.Blob == 6) {
                    Stages.Mike = 3;
                } else {
                    Talk.ExitTalk();
                }
                break;
            case 3:
                if (!_waitingForChoice) {
                    Talk.Choosing = true;
                    _waitingForChoice = true;
                    Continue = false;
                } else {
                    _waitingForChoice = false;
					if (yes) {
                        Stages.Mike = 4;
					} else {
                        Stages.Mike = 2;
                        Talk.ExitTalk();
                    }
                }
                break;
            case 4:
                Stages.Blob = 7;
                if (Stages.Cat == 1) {
                    Stages.Cat = 2;
                }
                Talk.ExitTalk();
                _waitingForAction = true;
                break;
            case 5:
                Talk.ExitTalk();
                _waitingForAction = true;
                break;
            case 6:
                Talk.ExitTalk();
                _waitingForAction = true;
                break;
            case 7:
                Talk.ExitTalk();
                _waitingForAction = true;
                break;
            case 8:
                Talk.ExitTalk();
                _waitingForAction = true;
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if(Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Mike) {
            case 0:
                gameObject.GetComponent<Animator>().SetBool("Happy", true);
                break;
            case 1:
                gameObject.GetComponent<Animator>().SetBool("Happy", false);
                break;
            case 4:
                gameObject.GetComponent<Animator>().SetBool("Happy", true);
                break;
            default:
                break;
        }
    }

	public override void Choice(bool choice) {
		if (_waitingForChoice) {
            EndOfStage(choice);
        }
	}

    public override void Action() {
        if (!_waitingForAction) {
            return;
        }
        switch (Stages.Mike) {
            case 4:
                Player.Animator.SetBool("Mike", true);
                gameObject.SetActive(false);
                break;
            case 5:
                Stages.Mike = 6;
                Player.CanMove = true;
                Player.Animator.speed = 1;
                break;
            case 6:
                SkiingManager.LoadNextScene();
                break;
            case 7:
                Stages.Mike = 8;
                Player.CanMove = true;
                break;
            case 8:
                IceLevelManager.instance.LoadNextScene();
                break;
            default:
                break;
        }
        _waitingForAction = false;
    }
}
