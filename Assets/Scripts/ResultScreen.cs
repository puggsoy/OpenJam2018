using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public Text broccoliText;
    public Text eggText;
    public Text mushroomText;
    public Text pineappleText;
    public Text shrimpText;
    public Text mouthfulText;

    public void SetStats(int brocs, int eggs, int mush, int pine, int shri, int bites)
    {
        broccoliText.text = "x " + brocs;
        eggText.text = "x " + eggs;
        mushroomText.text = "x " + mush;
        pineappleText.text = "x " + pine;
        shrimpText.text = "x " + shri;
        mouthfulText.text = bites + " bites total!";
    }
}
