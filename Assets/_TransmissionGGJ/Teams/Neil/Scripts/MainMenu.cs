using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _panelControls;
    public GameObject _panelVideo;
    public GameObject _panelGame;
    public GameObject _panelKeyBindings;
    public GameObject _panelMovement;
    public GameObject _panelCombat;
    public GameObject _panelGeneral;
   // public GameObject hoverSound;
    public AudioSource _hoverSound;
    public AudioSource _sfxhoversound;
    public AudioSource _clickSound;

    public GameObject _areYouSure;
    public Animator _cameraObject;

    public GameObject _continueBtn;
    public GameObject _newGameBtn;
    public GameObject _loadGameBtn;

    public GameObject _lineGame;
    public GameObject _lineVideo;
    public GameObject _lineControls;
    public GameObject _lineKeyBindings;
    public GameObject _lineMovement;
    public GameObject _lineCombat;
    public GameObject _lineGeneral;

    public GameObject _animationCredit;

    public void PlayCampaign()
    {
        _areYouSure.SetActive(false);
      //  continueBtn.SetActive(true);
        _newGameBtn.SetActive(true);
       // loadGameBtn.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("PlayScene");
    }

    public void DisablePlayCampaign()
    {
        _continueBtn.SetActive(false);
        _newGameBtn.SetActive(false);
        _loadGameBtn.SetActive(false);
    }

    public void Position1()
    {
        _cameraObject.SetFloat("Animate", 0);
    }

    public void Position2()
    {
        DisablePlayCampaign();
        _cameraObject.SetFloat("Animate", 1);
    }

    public void Position3()
    {
        DisablePlayCampaign();
        _cameraObject.SetFloat("Animate2", 1);
    }

    public void Position4()
    {
        
        _cameraObject.SetFloat("Animate2", 0);
    }

    public void GamePanel()
    {
        _panelControls.SetActive(false);
        _panelVideo.SetActive(false);
        _panelGame.SetActive(true);
        _panelKeyBindings.SetActive(false);

        _lineGame.SetActive(true);
        _lineControls.SetActive(false);
        _lineVideo.SetActive(false);
        _lineKeyBindings.SetActive(false);
    }

    public void VideoPanel()
    {
        _panelControls.SetActive(false);
        _panelVideo.SetActive(true);
        _panelGame.SetActive(false);
        _panelKeyBindings.SetActive(false);

        _lineGame.SetActive(false);
        _lineControls.SetActive(false);
        _lineVideo.SetActive(true);
        _lineKeyBindings.SetActive(false);
    }

    public void ControlsPanel()
    {
        _panelControls.SetActive(true);
        _panelVideo.SetActive(false);
        _panelGame.SetActive(false);
        _panelKeyBindings.SetActive(false);

        _lineGame.SetActive(false);
        _lineControls.SetActive(true);
        _lineVideo.SetActive(false);
        _lineKeyBindings.SetActive(false);
    }

    public void KeyBindingsPanel()
    {
        _panelControls.SetActive(false);
        _panelVideo.SetActive(false);
        _panelGame.SetActive(false);
        _panelKeyBindings.SetActive(true);

        _lineGame.SetActive(false);
        _lineControls.SetActive(false);
        _lineVideo.SetActive(true);
        _lineKeyBindings.SetActive(true);
    }

    public void MovementPanel()
    {
        _panelMovement.SetActive(true);
        _panelCombat.SetActive(false);
        _panelGeneral.SetActive(false);

        _lineMovement.SetActive(true);
        _lineCombat.SetActive(false);
        _lineGeneral.SetActive(false);
    }

     public void CombatPanel()
     {
        _panelMovement.SetActive(false);
        _panelCombat.SetActive(true);
        _panelGeneral.SetActive(false);

        _lineMovement.SetActive(false);
        _lineCombat.SetActive(true);
        _lineGeneral.SetActive(false);
    }

    public void GeneralPanel()
    {
        _panelMovement.SetActive(false);
        _panelCombat.SetActive(false);
        _panelGeneral.SetActive(true);

        _lineMovement.SetActive(false);
        _lineCombat.SetActive(false);
        _lineGeneral.SetActive(true);
    }

    public void PlayHover()
    {
        _hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        _sfxhoversound.Play();
    }

    public void PlayClick()
    {
        _clickSound.Play();
    }

    public void AreYouSure()
    {
        _areYouSure.SetActive(true);
        DisablePlayCampaign();
    }

    public void No()
    {
        _areYouSure.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void CreditsActivated()
    {
        _animationCredit.SetActive(true);
    }

    public void CreditsDesactivated()
    {
        _animationCredit.SetActive(false);
    }
}
