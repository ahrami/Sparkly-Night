using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "I am busy."
        }),
        new Stage(new string[]{ // stage 1
            "I will not tell you anything while I am hungry."
        }),
        new Stage(new string[]{ // stage 2
            "Get me some food and then we will talk."
        }),
        new Stage(new string[]{ // stage 3
            "What is that?",
            "Where did you even get that?",
            "Toad gave you?...",
            "All right...",
            "Well...",
            "It'll do for a snack...",
            "I hope...",
            "Here is your skis.",
            "And a tangerine.",
            "I guess..."
        }),
        new Stage(new string[]{ // stage 4
            "Merry Christmas."
        }),
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Goose].Length - 1) {
            return _dialogue[Stages.Goose][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Goose][_dialogue[Stages.Goose].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Goose) {
            case 0:
                Talk.ExitTalk();
                break;
            case 1:
                Stages.Goose = 2;
                break;
            case 2:
                if(Stages.Parrot == 1) {
                    Stages.Parrot = 2;
                }
                Talk.ExitTalk();
                break;
            case 3:
                Player.AddTangerines(1);
                Stages.Cat = 4;
                Stages.Duck = 4;
                Stages.Goose = 4;
                break;
            case 4:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Goose) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
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
