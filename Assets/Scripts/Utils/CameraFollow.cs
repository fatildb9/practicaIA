using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private Vector3 DeltaPosition = Vector2.zero;
    private Transform CurrentTarget;
    private Coroutine CurrentRoutine;

    public void SetTarget(Transform newTarget)
    {
        if (DeltaPosition == Vector3.zero)
        {
            DeltaPosition = transform.position;
        }

        if (newTarget != null)
        {
            CurrentTarget = newTarget;
            if (CurrentRoutine == null)
            {
                CurrentRoutine = StartCoroutine(FollowRoutine());
                transform.LookAt(CurrentTarget);
            }
        }
    }

    private IEnumerator FollowRoutine()
    {
        if (CurrentTarget != null)
        {
            while (true)
            {
                transform.position = CurrentTarget.position + DeltaPosition;
                transform.LookAt(CurrentTarget);
                yield return new WaitForFixedUpdate();
            }
        }

        yield return null;
        CurrentRoutine = null;
    }
}
