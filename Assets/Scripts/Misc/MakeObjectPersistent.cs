using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectPersistent : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
