using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMode : MonoBehaviour {

    public static Mode mode = Mode.TRADITIONAL;

    public void onChangeMode(bool modeBool)
    {
        Debug.Log(modeBool);
        mode = modeBool ? Mode.CHESS960 : Mode.TRADITIONAL;
        Debug.Log(mode);
    }
}
