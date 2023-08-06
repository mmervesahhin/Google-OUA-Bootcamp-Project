using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleRun : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Update()
    {
        Run();
    }

    public void Run()
    {
        anim.SetBool("run",true);
    }
}
