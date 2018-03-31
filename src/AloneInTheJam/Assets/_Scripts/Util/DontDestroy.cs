using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
    public static DontDestroy dontDestroy;

    void Awake()
    {
        this.InstantiateController();
    }

    private void InstantiateController()
    {
        if (dontDestroy == null)
        {
            dontDestroy = this;
            DontDestroyOnLoad(this);
        }
        else if (this != dontDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
