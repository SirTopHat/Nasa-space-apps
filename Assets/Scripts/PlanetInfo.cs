using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public string planetName;

    [TextArea(3, 8)]
    public string planetDetails;
    public string planetType;

    [TextArea(3, 8)]
    public string planetMoreDetails;
    public Sprite planetImage;

    public bool hasDisplayedInfo = false;
    
}
