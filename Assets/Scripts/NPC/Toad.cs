using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toad : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Hello.",
            "You are new?",
            "We have sort of a tradition here to give each other tangerines on Christmas.",
            "So...",
            "Here is yours.",
        }),
        new Stage(new string[]{ // stage 1
            "Merry Christmas!"
        }),
        new Stage(new string[]{ // stage 2
            "What?",
            "Goose asks for food again?",
            "Sigh.",
            "Fine...",
            "It is Christmas.",
            "All right.",
            "Take this.",
            "<i>You got a jar of insects.</i>",
            "And here's a tangerine for Mike.",
            "<i>Mike immediately eats his tangerine.</i>"
        }),
        new Stage(new string[]{ // stage 3
            "Merry Christmas!"
        })
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Toad].Length - 1) {
            return _dialogue[Stages.Toad][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Toad][_dialogue[Stages.Toad].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Toad) {
            case 0:
                if (Stages.Parrot >= 2) {
                    Stages.Toad = 2;
                } else {
                    Stages.Toad = 1;
                }
                Player.AddTangerines(1);
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                Stages.Goose = 3;
                Stages.Toad = 3;
                break;
            case 3:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Toad) {
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
