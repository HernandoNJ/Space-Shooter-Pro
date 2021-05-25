using UnityEngine;

namespace Lasers {
public class Laser : LaserBase
{
  protected override void OnEnable()
  {
    base.OnEnable();
    SetAdditionalValues();
    Invoke(nameof(DeactivateLaser),3);
  }

  protected override void SetAdditionalValues()
  {
    if(transform.parent.CompareTag("Player"))
      laserMoveDirection = Vector3.up;
    else if(transform.parent.CompareTag("Enemy"))
      laserMoveDirection = Vector3.down;
    else laserMoveDirection = Vector3.up; // Set direction to local
  }

  public void DeactivateLaser()
  {
    gameObject.SetActive(false);
  }
}
}