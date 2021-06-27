using System.Collections;
using UnityEngine;

namespace EnemyNS
{
public class EnemyShotAvoider : EnemyBase
{
    [SerializeField] private int laserplayerLayermask;
    [SerializeField] private bool avoidlaserActive;

    // todo: enemy shot avoider

    /*Layermask blog:
     http://gyanendushekhar.com/2017/06/29/understanding-layer-mask-unity-5-tutorial

    //Get layermask int or name
    Debug.Log(LayerMask.NameToLayer("Cube")); // 8
    Debug.Log(LayerMask.LayerToName(8)); // Cube

    laserplayerLayermask = 1 << 14; // assign layermask
    LayerMask CubeLayerMask = 1 << LayerMask.NameToLayer("Cube"); // create variable of type LayerMask or Integer

    Use OR to mask several layers
    //Hide the game object on click if it is cube or cylinder
    if (Input.GetMouseButtonDown (0))
        if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out rayhit, 100f, CubeLayerMask | CylinderMask))
            if (rayhit.collider != null) rayhit.collider.gameObject.SetActive(false);
    */
    /* Dodging blog:
     * https://lukeduckett.medium.com/phase-ii-progress-report-new-functionality-dodging-399e526c6255
     */

    protected override void ConfigureEnemy(EnemyData _data)
    {
        base.ConfigureEnemy(_data);
        laserplayerLayermask = LayerMask.NameToLayer("LaserPlayer");
    }

    protected override void MoveEnemy()
    {
        base.MoveEnemy();
        ScanForAttacks();
    }

    private void ScanForAttacks()
    {
        var hitInfo = Physics2D.CircleCast(transform.position, 2,transform.TransformDirection(Vector2.down), laserplayerLayermask);
        var hitCol = hitInfo.collider;
        if (hitCol == null || !hitCol.CompareTag("Laser")) return;
        StartCoroutine(AvoidPlayerLaserRoutine(hitInfo.point));
    }

    private IEnumerator AvoidPlayerLaserRoutine(Vector3 hitPoint)
    {
        var pos = transform.position;
        if (pos.y < hitPoint.y + 1)
            transform.Translate(pos.x += 1 * 3*Time.deltaTime,0,0);
        yield break;
    }
}
}
