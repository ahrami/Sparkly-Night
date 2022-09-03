using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Hello!",
            "I am Blob.",
            "Have we met before?"
        }),
        new Stage(new string[]{ // stage 1
            "Oh, I do not remember...",
            "Please forgive an old blob."
        }),
        new Stage(new string[]{ // stage 2
            "Maybe I am just a crazy old blob", 
            "Nice to meet you, then."
        }),
        new Stage(new string[]{ // stage 3
            "I have got to prepare something for dinner now, it is Christmas after all."
        }),
        new Stage(new string[]{ // stage 4
            "I have got to prepare something for dinner now."
        }),
        new Stage(new string[]{ // stage 5
            "Oh, Mike?",
            "He lost his Christmas hat at the Sparkly Lake.",
            "And he can't go there alone.",
            "But everybody is preparing for Christmas...",
            "There is no time for that.",
            "You can help him, though, if you want."
        }),
        new Stage(new string[]{ // stage 6
            "I am preparing dinner."
        }),
        new Stage(new string[]{ // stage 7
            "So you are helping Mike?",
            "How nice of you!",
            "Have a tangerine!"
        }),
        new Stage(new string[]{ // stage 8
            "Merry Christmas!"
        })
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Blob].Length - 1) {
            return _dialogue[Stages.Blob][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Blob][_dialogue[Stages.Blob].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Blob) {
            case 0:
                if (!_waitingForChoice) {
                    Talk.Choosing = true;
                    _waitingForChoice = true;
                    Continue = false;
                } else {
                    _waitingForChoice = false;
                    if (choice) {
                        Stages.Blob = 1;
                    } else {
                        Stages.Blob = 2;
                    }
                }
                break;
            case 1:
                if (Stages.Mike == 0) {
                    Stages.Blob = 3;
                } else {
                    Stages.Blob = 5;
                }
                break;
            case 2:
                if (Stages.Mike == 0) {
                    Stages.Blob = 3;
                } else {
                    Stages.Blob = 5;
                }
                break;
            case 3:
                Stages.Blob = 4;
                Talk.ExitTalk();
                break;
            case 4:
                Talk.ExitTalk();
                break;
            case 5:
                Stages.Blob = 6;
                Talk.ExitTalk();
                break;
            case 6:
                Talk.ExitTalk();
                break;
            case 7:
                Player.AddTangerines(1);
                Stages.Blob = 8;
                break;
            case 8:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Blob) {
            default:
                break;
        }
    }

	public override void Choice(bool choice) {
        if (_waitingForChoice) {
            EndOfStage(choice);
        }
    }
}
