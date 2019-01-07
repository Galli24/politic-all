using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjs : MonoBehaviour {

    public static UIObjs instance;

    public GameObject question;
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    public GameObject answer4;

    public GameObject answer1_obj;
    public GameObject answer2_obj;
    public GameObject answer3_obj;
    public GameObject answer4_obj;

    public GameObject continue_button;

    public GameObject goodPoint;
    public GameObject badPoint;

    public GameObject armyFill;
    public GameObject bankFill;
    public GameObject peopleFill;

    public GameObject peopleUnhappy;
    public GameObject peopleNormal;
    public GameObject peopleHappy;

    public GameObject ArmyMe;
    public GameObject BankMe;
    public GameObject PeopleMe;

    void Awake()
    {
        instance = this;
    }
}
