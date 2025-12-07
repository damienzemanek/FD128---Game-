using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using static Extensions.AnimEX;

public class LevelSelect : MonoBehaviour
{
    public Animatable anims;
    public string openAnimName;
    public string closeAnimName;

    public void Open() => anims.Animate(openAnimName);
    public void Close() => anims.Animate(closeAnimName);
}
