using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ System.Serializable]
public class Shot
{
    //Datos que todos tiro necesita 
    public float upForce;
    public float hitForce;
}

public class ShotManager : MonoBehaviour
{
    //Distintos tipos de tiros que se pueden realizar
    public Shot topSpin;
    public Shot Flat;
    public Shot flatServe;
    public Shot kickServe;

}
