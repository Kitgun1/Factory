using UnityEngine;
using DG.Tweening;

public static class EntityTurner
{
    private const float _rotationStep = 90;

    public static void Action(Transform transform, float rotateDuration)
    {
        Vector3 targetRotation = transform.rotation.eulerAngles;

        targetRotation.y += _rotationStep;
        transform.DORotate(targetRotation, rotateDuration, RotateMode.Fast);
    }
}