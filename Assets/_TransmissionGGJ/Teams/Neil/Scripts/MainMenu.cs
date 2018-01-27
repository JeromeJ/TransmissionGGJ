using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject PanelControls;
    public GameObject PanelVideo;
    public GameObject PanelGame;
    public GameObject PanelKeyBindings;
    public GameObject PanelMovement;
    public GameObject PanelCombat;
    public GameObject PanelGeneral;
   // public GameObject hoverSound;
    public AudioSource hoverSound;
    public AudioSource sfxhoversound;
    public AudioSource clickSound;

    public GameObject areYouSure;
    public Animator CameraObject;

    public GameObject continueBtn;
    public GameObject newGameBtn;
    public GameObject loadGameBtn;

    public GameObject lineGame;
    public GameObject lineVideo;
    public GameObject lineControls;
    public GameObject lineKeyBindings;
    public GameObject lineMovement;
    public GameObject lineCombat;
    public GameObject lineGeneral;

    public void PlayCampaign()
    {
        areYouSure.SetActive(false);
        continueBtn.SetActive(true);
        newGameBtn.SetActive(true);
        loadGameBtn.SetActive(true);
    }

    public void DisablePlayCampaign()
    {
        continueBtn.SetActive(false);
        newGameBtn.SetActive(false);
        loadGameBtn.SetActive(false);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }

    public void Position2()
    {
        DisablePlayCampaign();
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position3()
    {
        DisablePlayCampaign();
        CameraObject.SetFloat("Animate2", 1);
    }

    public void Position4()
    {
        
        CameraObject.SetFloat("Animate2", 0);
    }

    public void GamePanel()
    {
        PanelControls.SetActive(false);
        PanelVideo.SetActive(false);
        PanelGame.SetActive(true);
        PanelKeyBindings.SetActive(false);

        lineGame.SetActive(true);
        lineControls.SetActive(false);
        lineVideo.SetActive(false);
        lineKeyBindings.SetActive(false);
    }

    public void VideoPanel()
    {
        PanelControls.SetActive(false);
        PanelVideo.SetActive(true);
        PanelGame.SetActive(false);
        PanelKeyBindings.SetActive(false);

        lineGame.SetActive(false);
        lineControls.SetActive(false);
        lineVideo.SetActive(true);
        lineKeyBindings.SetActive(false);
    }

    public void ControlsPanel()
    {
        PanelControls.SetActive(true);
        PanelVideo.SetActive(false);
        PanelGame.SetActive(false);
        PanelKeyBindings.SetActive(false);

        lineGame.SetActive(false);
        lineControls.SetActive(true);
        lineVideo.SetActive(false);
        lineKeyBindings.SetActive(false);
    }

    public void KeyBindingsPanel()
    {
        PanelControls.SetActive(false);
        PanelVideo.SetActive(false);
        PanelGame.SetActive(false);
        PanelKeyBindings.SetActive(true);

        lineGame.SetActive(false);
        lineControls.SetActive(false);
        lineVideo.SetActive(true);
        lineKeyBindings.SetActive(true);
    }

    public void MovementPanel()
    {
        PanelMovement.SetActive(true);
        PanelCombat.SetActive(false);
        PanelGeneral.SetActive(false);

        lineMovement.SetActive(true);
        lineCombat.SetActive(false);
        lineGeneral.SetActive(false);
    }

     public void CombatPanel()
     {
        PanelMovement.SetActive(false);
        PanelCombat.SetActive(true);
        PanelGeneral.SetActive(false);

        lineMovement.SetActive(false);
        lineCombat.SetActive(true);
        lineGeneral.SetActive(false);
    }

    public void GeneralPanel()
    {
        PanelMovement.SetActive(false);
        PanelCombat.SetActive(false);
        PanelGeneral.SetActive(true);

        lineMovement.SetActive(false);
        lineCombat.SetActive(false);
        lineGeneral.SetActive(true);
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        sfxhoversound.Play();
    }

    public void PlayClick()
    {
        clickSound.Play();
    }

    public void AreYouSure()
    {
        areYouSure.SetActive(true);
        DisablePlayCampaign();
    }

    public void No()
    {
        areYouSure.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }
}
