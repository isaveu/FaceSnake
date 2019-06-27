using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using milook;
using System;

[System.Serializable]
public class FaceEvent : UnityEvent<MilookEngine.FaceEX> { }


public class MiloReceiver : MonoBehaviour
{
    public FaceEvent faceEvent;

    private static Dictionary<MilookEngine.FaceEX, string> dic = new Dictionary<MilookEngine.FaceEX, string>();
    private static Dictionary<MilookEngine.FaceEX, float> dicThreshold = new Dictionary<MilookEngine.FaceEX, float>();

    private bool[] expPrevious = new bool[21];
    private float[] attributeProgress = new float[21];

    private void Awake()
    {

        dic[MilookEngine.FaceEX.EX_E_CLOSE_SYM] = "E_CLOSE_SYM";//"Symmetric eye close";
        dic[MilookEngine.FaceEX.EX_E_CLOSE_R] = "E_CLOSE_R";//"Right eye close";
        dic[MilookEngine.FaceEX.EX_E_CLOSE_L] = "E_CLOSE_L";//"Left eye close";
        dic[MilookEngine.FaceEX.EX_E_OPEN_SYM] = "OPEN_SYM";//"Symmetric wide eye open";
        dic[MilookEngine.FaceEX.EX_B_RAISE_SYM] = "B_RAISE_SYM";//"Symmetric eyebrow raise";
        dic[MilookEngine.FaceEX.EX_B_RAISE_R] = "B_RAISE_R";//"Right eyebrow raise";
        dic[MilookEngine.FaceEX.EX_B_RAISE_L] = "B_RAISE_L";//"Left eyebrow raise";


        dic[MilookEngine.FaceEX.EX_B_FURROW_SYM] = "B_FURROW_SYM";//"Symmetric eyebrow furrow";
        dic[MilookEngine.FaceEX.EX_M_AH] = "M_AH";//"Ah-shape mouth open";
        dic[MilookEngine.FaceEX.EX_M_DIS] = "M_DIS";//"Disgusted mouth shape";
        dic[MilookEngine.FaceEX.EX_M_DOWN] = "M_DOWN";//"Downward displacement of the mouth";
        dic[MilookEngine.FaceEX.EX_M_OH] = "M_OH";//"Oh-shaped mouth";
        dic[MilookEngine.FaceEX.EX_M_EH] = "M_EH";//"Eh-shaped mouth";
        dic[MilookEngine.FaceEX.EX_M_CLOSE_SMILE] = "CLOSE_SMILE";//"Mouth-closed smile";
        dic[MilookEngine.FaceEX.EX_M_OPEN_SMILE] = "OPEN_SMILE";//"Mouth-open smile";
        dic[MilookEngine.FaceEX.EX_M_FROWN] = "M_FROWN";//"Frown mouth shape";
        dic[MilookEngine.FaceEX.EX_M_PULL_RIGHT] = "PULL_RIGHT";//"Pull of the right mouth corner";
        dic[MilookEngine.FaceEX.EX_M_PULL_LEFT] = "PULL_LEFT";//"Pull of the left mouth corner";


        dicThreshold[MilookEngine.FaceEX.EX_E_CLOSE_SYM] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_E_CLOSE_R] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_E_CLOSE_L] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_E_OPEN_SYM] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_B_RAISE_SYM] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_B_RAISE_R] = 0.2f;
        dicThreshold[MilookEngine.FaceEX.EX_B_RAISE_L] = 0.2f;


        dicThreshold[MilookEngine.FaceEX.EX_B_FURROW_SYM] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_AH] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_DIS] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_DOWN] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_OH] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_EH] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_CLOSE_SMILE] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_OPEN_SMILE] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_FROWN] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_PULL_RIGHT] = 0.5f;
        dicThreshold[MilookEngine.FaceEX.EX_M_PULL_LEFT] = 0.5f;
    }



    void OnTrack(MilookEngine.TrackerData tdata)
    {
        if (tdata.error == 0)
        {
            for (int i = 0; i < tdata.exp.Length; i++)
            {
                attributeProgress[i] = tdata.exp[i];// * attributeProgressOffset [i];
            }
        }
        DetectStateChange();
    }


    // Detects state changes (rising edge) on estimators
    private void DetectStateChange()
    {
        for(int i=0; i<18;i++)
        {
            bool current = attributeProgress[i] > dicThreshold[(MilookEngine.FaceEX)i];
            if (!expPrevious[i] && current)
            {
                //print("[MiloReceiver] BANG");
                faceEvent.Invoke((MilookEngine.FaceEX)i);
            }

            expPrevious[i] = current;
        }

       
    }

    private float MapValue(float a0, float a1, float b0, float b1, float a)
    {
        return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
    }

    void OnGUI()
    {
        if (attributeProgress.Length <= 0) return;

        float offsetY = 38;
        foreach (KeyValuePair<MilookEngine.FaceEX, string> k in dic)
        {
            GUI.HorizontalSlider(new Rect(122, offsetY, 75, 20), attributeProgress[(int)k.Key], 0, 1);

            GUI.Label(new Rect(10, offsetY, 120, 30), dic[k.Key] + ":");
            GUI.Label(new Rect(200, offsetY, 25, 30), Math.Round(attributeProgress[(int)k.Key], 2).ToString());
            offsetY += 22;
        }
    }
}