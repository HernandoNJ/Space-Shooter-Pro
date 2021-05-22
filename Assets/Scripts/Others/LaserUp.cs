using System;
using UnityEngine;

public class LaserUp : LaserBase
{
    private void Awake()
    {
      SetMoveDirection(Vector3.up);
      SetParentTag("Player");
    }
}
