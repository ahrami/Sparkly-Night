using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Want a tangerine?"
        }),
        new Stage(new string[]{ // stage 1
            "Here."
        }),
        new Stage(new string[]{ // stage 2
            "Yes you want."
        }),
        new Stage(new string[]{ // stage 3
            "Ah, yes...",
            "Hi!",
            "I am Potato.",
            "Nice to meet you!",
            "Please don't put me in a soup though...",
            "You won't put me in a soup, right?...",
            "<i>Would you put Potato in a soup?</i>"
        }),
        new Stage(new string[]{ // stage 4
            "Why?",
            "You are joking, right?..."
        }),
        new Stage(new string[]{ // stage 5
            "<i>Potato doesn't want to talk.</i>"
        }),
        new Stage(new string[]{ // stage 6
            "Ahah, thank you!"
        }),
        new Stage(new string[]{ // stage 7
            "Merry Christmas!"
        })
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Potato].Length - 1) {
            return _dialogue[Stages.Potato][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Potato][_dialogue[Stages.Potato].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Potato) {
            case 0:
                if (!_waitingForChoice) {
                    Talk.Choosing = true;
                    _waitingForChoice = true;
                    Continue = false;
                } else {
                    _waitingForChoice = false;
                    if (choice) {
                        Stages.Potato = 1;
                    } else {
                        Stages.Potato = 2;
                    }
                }
                break;
            case 1:
                Player.AddTangerines(1);
                Stages.Potato = 3;
                Talk.ExitTalk();
                break;
            case 2:
                Stages.Potato = 1;
                break;
            case 3:
                if (!_waitingForChoice) {
                    Talk.Choosing = true;
                    _waitingForChoice = true;
                    Continue = false;
                } else {
                    _waitingForChoice = false;
                    if (choice) {
                        Stages.Potato = 4;
                    } else {
                        Stages.Potato = 6;
                    }
                }
                break;
            case 4:
                Stages.Potato = 5;
                Talk.ExitTalk();
                break;
            case 5:
                Talk.ExitTalk();
                break;
            case 6:
                Stages.Potato = 7;
                break;
            case 7:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Potato) {
            case 0:
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
}
