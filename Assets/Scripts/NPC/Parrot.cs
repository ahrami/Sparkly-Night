using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Duck and Goose are fighting again.",
            "So funny to watch them and realize that I am the smartest bird.",
            "..."
        }),
        new Stage(new string[]{ // stage 1
            "You wanted something?"
        }),
        new Stage(new string[]{ // stage 2
            "Food for Goose?",
            "Hell no!",
            "I am not giving him a single grain after he stole all my oat.",
            "You can ask Toad.",
            "Maybe she has something."
        }),
        new Stage(new string[]{ // stage 3
            "Do you know Hedgehog?",
            "That guy that lives on the side of the town.",
            "Could you please tell him that he is invited to dinner?"
        }),
        new Stage(new string[]{ // stage 4
            "You wanted something?"
        }),
        new Stage(new string[]{ // stage 5
            "You told him?",
            "Thank you!",
            "Take this tangerine.",
        }),
        new Stage(new string[]{ // stage 6
            "Oh, Crab?",
            "Yeah, he can come too!",
            "Does he know?",
            "Tell him, please."
        }),
        new Stage(new string[]{ // stage 7
            "Merry Christmas!"
        }),
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Parrot].Length - 1) {
            return _dialogue[Stages.Parrot][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Parrot][_dialogue[Stages.Parrot].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Parrot) {
            case 0:
                if (Stages.Goose == 2) {
                    Stages.Parrot = 2;
                } else {
                    Stages.Parrot = 1;
                    Talk.ExitTalk();
                }
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                if(Stages.Toad == 1) {
                    Stages.Toad = 2;
				}
                Stages.Parrot = 3;
                Talk.ExitTalk();
                break;
            case 3:
                if(Stages.Hedgehog == 1) {
                    Stages.Hedgehog = 2;
				}
                Stages.Parrot = 4;
                Talk.ExitTalk();
                break;
            case 4:
                Talk.ExitTalk();
                break;
            case 5:
                Stages.Parrot = 6;
                Player.AddTangerines(1);
                break;
            case 6:
                if(Stages.Crab == 1) {
                    Stages.Crab = 2;
                }
                Stages.Parrot = 7;
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
        switch (Stages.Parrot) {
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
