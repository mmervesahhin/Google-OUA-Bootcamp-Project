using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyLoopObject", 1f);
    }

    void DestroyLoopObject()
    {
        Destroy(gameObject);
    }
}
