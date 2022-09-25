using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="GameData", menuName ="GameData")]
public class GameData : ScriptableObject
{
    public int checkPointCounter;
    public bool planeAngleCheck;
    public int score;
    public int speed;
    public bool planeFlying;
}
