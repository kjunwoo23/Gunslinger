using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;
    public int phase;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeToPhase1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeToPhase2();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeToPhase3();
    }

    void ChangeToPhase1()
    {

    }
    void ChangeToPhase2()
    {
        PlayerShoot.instance.isRifle = false;
        PlayerShoot.instance.rifleAim.SetActive(false);
        PlayerShoot.instance.revolverAim.SetActive(true);
    }
    void ChangeToPhase3()
    {

    }

}
