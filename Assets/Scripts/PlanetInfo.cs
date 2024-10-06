using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public string planetName;

    [TextArea(3, 8)]
    public string planetDetails;
    public string planetType;

    public bool hasDisplayedInfo = false;
}
