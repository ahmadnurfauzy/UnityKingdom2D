using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActive : MonoBehaviour
{
    public Transform Target;
    public Vector2 MinPos;
    public Vector2 MaxPos;
    public float Speed;

    private void LateUpdate()
    {
        if (transform.position != Target.position)
        {
            var targetpos = new Vector3(Target.position.x,
                                        Target.position.y,
                                        transform.position.z);
            targetpos.x = Mathf.Clamp(targetpos.x, MinPos.x, MaxPos.x);
            targetpos.y = Mathf.Clamp(targetpos.y, MinPos.y, MaxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetpos, Speed);
        }
    }
}
