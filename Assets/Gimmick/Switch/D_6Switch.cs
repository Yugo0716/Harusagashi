using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_6Switch : MonoBehaviour
{
    public GameObject switch1;
    public GameObject switch2;
    public GameObject switch3;
    public GameObject switch4;

    SwitchAction action1;
    SwitchAction action2;
    SwitchAction action3;
    SwitchAction action4;

    public GameObject target;



    // Start is called before the first frame update
    void Start()
    {
        action1 = switch1.GetComponent<SwitchAction>();
        action2 = switch2.GetComponent<SwitchAction>();
        action3 = switch3.GetComponent<SwitchAction>();
        action4 = switch4.GetComponent<SwitchAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(action1.on && action2.on && action3.on && action4.on)
        {
            target.SetActive(false);
        }
    }
}
