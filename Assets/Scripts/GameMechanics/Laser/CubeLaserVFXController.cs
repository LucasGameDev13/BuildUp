using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeLaserVFXController : MonoBehaviour
{

    private MeshRenderer myMesh;
    private Transform meshTransform;


    void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
        meshTransform = myMesh.transform;
    }

    public void SetMesh(bool _value)
    {
        myMesh.enabled = _value;
    }

    public void SetLaserHeight(float height)
    {

        float newHeight = meshTransform.localScale.y + height;

        meshTransform.localScale = new Vector3(
            meshTransform.localScale.x,
            newHeight,
            meshTransform.localScale.z
        );

        meshTransform.localPosition = new Vector3(
            meshTransform.localPosition.x,
            -newHeight,
            meshTransform.localPosition.z
        );
    }
}
