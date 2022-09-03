using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Oh, hi, visitor.",
            "I am Hedgehog.",
            "Not many guests I have usually.",
            "I love trees, so that is why live here."
        }),
        new Stage(new string[]{ // stage 1
            "I love trees."
        }),
        new Stage(new string[]{ // stage 2
            "Parrot invited me to dinner?",
            "Oh, thanks for telling me!",
            "Could you ask him please if Crab can come?",
            "We are friends, I would like him to be there too."
        }),
        new Stage(new string[]{ // stage 3
            "I still love trees."
        }),
        new Stage(new string[]{ // stage 4
            "Crab will come?",
            "Yeah!",
            "Thank you!",
            "Take this tangerine!"
        }),
        new Stage(new string[]{ // stage 5
            "Merry Christmas!"
        })
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Hedgehog].Length - 1) {
            return _dialogue[Stages.Hedgehog][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Hedgehog][_dialogue[Stages.Hedgehog].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Hedgehog) {
            case 0:
                if (Stages.Parrot == 4) {
                    Stages.Hedgehog = 2;
                } else {
                    Stages.Hedgehog = 1;
                    Talk.ExitTalk();
                }
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                Stages.Parrot = 5;
                Stages.Hedgehog = 3;
                Talk.ExitTalk();
                break;
            case 3:
                Talk.ExitTalk();
                break;
            case 4:
                Player.AddTangerines(1);
                Stages.Hedgehog = 5;
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
        switch (Stages.Hedgehog) {
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
