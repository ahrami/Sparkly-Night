using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "I can not stand that stupid goose!",
            "He always steals my food!",
            "God's forsaken child..."
        }),
        new Stage(new string[]{ // stage 1
            "I do not have time for that."
        }),
        new Stage(new string[]{ // stage 2
            "You want Cat's skis?",
            "Guess what?",
            "Goose stole them!",
            "That brainless bird never stops..."
        }),
        new Stage(new string[]{ // stage 3
            "Go to goose if you want to get skis."
        }),
        new Stage(new string[]{ // stage 4
            "Wow!",
            "You got them!",
            "Thank you!",
            "You keep them...",
            "Now you have to return them to Cat.",
            "Here is a tangerine for that."
        }),
        new Stage(new string[]{ // stage 5
            "Merry Christmas!"
        }),
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Duck].Length - 1) {
            return _dialogue[Stages.Duck][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Duck][_dialogue[Stages.Duck].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Duck) {
            case 0:
                if (Stages.Cat == 3) {
                    Stages.Duck = 2;
                } else {
                    Stages.Duck = 1;
                    Talk.ExitTalk();
                }
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                Stages.Goose = 1;
                Stages.Duck = 3;
                Talk.ExitTalk();
                break;
            case 3:
                Talk.ExitTalk();
                break;
            case 4:
                Stages.Duck = 5;
                Player.AddTangerines(1);
                break;
            case 5:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Duck) {
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
