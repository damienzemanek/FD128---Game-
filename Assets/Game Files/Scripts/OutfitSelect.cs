using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using static Extensions.AnimEX;
using static Extensions.UIEX;

public class OutfitSelect : MonoBehaviour
{
    public Animatable anim;
    public ChangePosSettings change;

    public string openAnimName = "open";
    public string closeAnimName = "close";

    public List<GameObject> regularUIObjects;

    public void Move(bool newPos)
    {
        this.ChangePos(newPos, change);

        string animName = "";
        if (newPos) animName = openAnimName;
        else animName = closeAnimName;

        anim.Animate(animName, this);
        regularUIObjects.SetAllActive(!newPos);
    }



}
