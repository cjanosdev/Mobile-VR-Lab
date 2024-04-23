using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotInEditor : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_EDITOR
        gameObject.SetActive((false));
        #endif
    }
}
