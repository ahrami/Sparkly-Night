using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Hello!",
            "I have never seen you before in Sparkly Town.",
            "I am Crab.",
            "I am friends with Hedgehog.",
            "You should visit him by the way."
        }),
        new Stage(new string[]{ // stage 1
            "Me Crab."
        }),
        new Stage(new string[]{ // stage 2
            "Parrot invites me to dinner?",
            "And Hedgehog will be there?",
            "Of course I am coming!",
            "Here.",
            "Tangerine."
        }),
        new Stage(new string[]{ // stage 3
            "Merry Christmas!"
        })
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Crab].Length - 1) {
            return _dialogue[Stages.Crab][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Crab][_dialogue[Stages.Crab].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Crab) {
            case 0:
                if(Stages.Parrot == 7) {
                    Stages.Crab = 2;
                } else {
                    Stages.Crab = 1;
                    Talk.ExitTalk();
                }
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                Player.AddTangerines(1);
                Stages.Crab = 3;
                Stages.Hedgehog = 4;
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
        switch (Stages.Crab) {
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
