using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour 
{
    public bool IsPassable;
    public bool IsNotAction = true;

    public virtual void Action()
    {

    }
}
