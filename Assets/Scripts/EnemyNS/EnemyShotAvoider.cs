using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace EnemyNS
{
public class EnemyShotAvoider : EnemyBase
{
    [SerializeField] private GameObject scannerBox;
    [SerializeField] private LayerMask laserplayerLayer;
    [SerializeField] private bool isAvoidingLaser;
    [SerializeField] private bool scanningPlayerLaser;
    [SerializeField] private float distance;

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

    protected override void SetInitialEnemyValues(EnemyData _data)
    {
        base.SetInitialEnemyValues(_data);
        scanningPlayerLaser = true;
        isAvoidingLaser = false;
    }

    protected override void MoveEnemy()
    {
        base.MoveEnemy();
        ScanForAttacks();
    }

    private void ScanForAttacks()
    {
        Debug.Log("entering scan for attacks");

        var boxPosition = scannerBox.transform.position;
        var boxSize = new Vector2(1, 1);
        var downDirection = Vector2.down;

        var hitInfo = Physics2D.BoxCast(boxPosition, boxSize, 0, downDirection, distance, laserplayerLayer);

        if (hitInfo)
        {
            if (scanningPlayerLaser)
            {
                scanningPlayerLaser = false;
                StartCoroutine(AvoidPlayerLaserRoutine());
            }
        }

        // for blog: if not checking avoidLaserActive, it executes several routines
    }

    private IEnumerator AvoidPlayerLaserRoutine()
    {
        Debug.Log("entering routine");
        isAvoidingLaser = true;
        float timePassedCounter = 0;
        float timeToWait1 = 0.35f;
        float timeToWait2 = 0.7f;

        while (isAvoidingLaser && scanningPlayerLaser == false)
        {
            while (timePassedCounter < timeToWait1)
            {
                transform.Translate((Vector2.right * 6f * Time.deltaTime));
                yield return new WaitForEndOfFrame();
                timePassedCounter += Time.deltaTime;
            }

            while (timePassedCounter > timeToWait1 && timePassedCounter < timeToWait2)
            {
                transform.Translate((Vector2.left * 6f * Time.deltaTime));
                yield return new WaitForEndOfFrame();
                timePassedCounter += Time.deltaTime;

                if (timePassedCounter > timeToWait2)
                {
                    isAvoidingLaser = false;
                    yield return new WaitForSeconds(1);
                    scanningPlayerLaser = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(scannerBox.transform.position, new Vector2(1, 1));
    }
}
}
