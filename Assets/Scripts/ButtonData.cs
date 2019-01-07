using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonData : MonoBehaviour {

    [SerializeField]
    Vector3 influenceData = Vector3.zero;
    [SerializeField]
    Vector3 caracsData = Vector3.zero;

    public void setInfluenceData(Vector3 data)
    {
        AudioManager.instance.PlaySound2D("clic");
        influenceData = data;
    }

    public void setCaracsData(Vector3 data)
    {
        AudioManager.instance.PlaySound2D("clic");
        caracsData = data;
    }

    public void ClickAnswer()
    {
        AudioManager.instance.PlaySound2D("clic");
        Questions.instance.ChangeInfluence(influenceData);
    }

    public void ClickAnswerCaracs()
    {
        AudioManager.instance.PlaySound2D("clic");
        Questions.instance.ChangeInfluence(influenceData);
        Questions.instance.ChangeCaracs(caracsData);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
