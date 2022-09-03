using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Talkable {

    protected new Stage[] _dialogue = {
        new Stage(new string[]{ // stage 0
            "Meow!",
            "I don't know you.",
            "What business do you have here?",
            "Although, you seem to be a nice girl.",
            "Hi.",
            "I am Cat.",
            "Very glad to meet you here in Sparkly Town.",
            "Now, what did you want?"
        }),
        new Stage(new string[]{ // stage 1
            "I am listening."
        }),
        new Stage(new string[]{ // stage 2
            "You need my skis?",
            "Well...",
            "I do not have them on me right now.",
            "Duck took them and still has not returned them yet.",
            "If you can get my skis from her I will let you borrow them."
        }),
        new Stage(new string[]{ // stage 3
            "If you can get my skis from Duck I will let you borrow them."
        }),
        new Stage(new string[]{ // stage 4
            "Oh, so you got my skis.",
            "Since I promised...",
            "You can borrow them.",
            "Here, take a tangerine."
        }),
        new Stage(new string[]{ // stage 5
            "If you are going to go down the Sparkly Hill...",
            "...that is just to the north from here, be careful.",
            "There are trees."
        }),
        new Stage(new string[]{ // stage 5
            "Merry Christmas!",
            "Be safe."
        }),
    };

    public override string Next() {

        if (_phraze == 0) {
            StartOfStage();
        }

        if (_phraze < _dialogue[Stages.Cat].Length - 1) {
            return _dialogue[Stages.Cat][_phraze++];
        }

        Talk.Suspend();
        StartCoroutine(WaitForEnd());
        return _dialogue[Stages.Cat][_dialogue[Stages.Cat].Length - 1];
    }

    protected override void EndOfStage(bool choice = false) {
        bool Continue = true;
        _phraze = 0;
        switch (Stages.Cat) {
            case 0:
                if(Stages.Mike == 4) {
                    Stages.Cat = 2;
				} else {
                    Stages.Cat = 1;
                    Talk.ExitTalk();
                }
                break;
            case 1:
                Talk.ExitTalk();
                break;
            case 2:
                Stages.Cat = 3;
                if(Stages.Duck == 1) {
                    Stages.Duck = 2;
				}
                Talk.ExitTalk();
                break;
            case 3:
                Talk.ExitTalk();
                break;
            case 4:
                Player.AddTangerines(1);
                Stages.Cat = 5;
                break;
            case 5:
                Stages.Cat = 6;
                break;
            case 6:
                Talk.ExitTalk();
                break;
            default:
                Talk.ExitTalk();
                break;
        }
        if (Continue) Talk.Continue();
    }

    protected override void StartOfStage() {
        switch (Stages.Cat) {
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
