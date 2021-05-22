using UnityEngine;

public class LaserDown : LaserBase
{
    private void Awake()
    {
        SetMoveDirection(Vector3.down);
        SetParentTag("Enemy");
    }
}
