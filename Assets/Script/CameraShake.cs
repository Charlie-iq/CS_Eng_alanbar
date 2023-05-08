using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake _inst;
    Vector3 basePos;
    private void Start()
    {
        basePos=transform.position;
    }
    private void Awake()
    {
        _inst = this;
    }
    public void Shake(float duration = 0.3f,float power =1f)
    {
        transform.DOShakePosition(duration,power).OnComplete(OnSHakeComplete);
    }
    void OnSHakeComplete()
    {
        transform.position= basePos;
    }
}

