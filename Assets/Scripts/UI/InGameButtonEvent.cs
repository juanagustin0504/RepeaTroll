using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButtonEvent : MonoBehaviour {

    public delegate void AttackButtonEvent();
    public static event AttackButtonEvent OnAttackButton;

    public delegate void PortalButtonEvent();
    public static event PortalButtonEvent OnPortalButton;

    private void Awake() {
        OnAttackButton = delegate { };
        OnPortalButton = delegate { };
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            AttackButton();

        if (Input.GetKeyDown(KeyCode.LeftControl) && PortalScript.Instance.checkTriggerEnter)
            PortalButton();
    }

    public void AttackButton()
    {
        OnAttackButton();
    }

    public void PortalButton() {
        OnPortalButton();
    }

}
