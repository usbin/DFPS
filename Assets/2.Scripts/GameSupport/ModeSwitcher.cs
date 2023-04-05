using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ModeSwitcher : MonoBehaviour
{
    public UnityEvent<ViewModeChangedArgs> ViewModeChanged;
    public UnityEvent<bool> UiModeChanged;

    // ÇÃ·¹À× ¸ðµå¿¡¼± ºä¿¡ µû¶ó ´Ù¸¥ Äµ¹ö½º
    public Canvas Canvas_FirstPersonView_OnPlaying;
    public Canvas Canvas_TopdownView_OnPlaying;
    // Ui¸ðµåÀÏ ¶© µ¿ÀÏ
    public Canvas Canvas_OnUi;
    public Camera Camera_FirstPerson;
    public Camera Camera_Topdown;

    bool _uiMode;

    Canvas[] _canvases_onPlaying = new Canvas[2];
    Camera[] _cameras = new Camera[2];
    PlayerInput _input;

    ViewModeChangedArgs _viewModeArgs;



    private void Awake()
    {
        _input = PlayerInput.GetPlayerByIndex(0);

        _canvases_onPlaying[(int)Player.ViewMode.FIRST_PERSON] = Canvas_FirstPersonView_OnPlaying;
        _canvases_onPlaying[(int)Player.ViewMode.TOPDOWN] = Canvas_TopdownView_OnPlaying;

        _cameras[(int)Player.ViewMode.FIRST_PERSON] = Camera_FirstPerson;
        _cameras[(int)Player.ViewMode.TOPDOWN] = Camera_Topdown;
    }
    void Start()
    {
        SetViewMode(Player.ViewMode.FIRST_PERSON);
        SetUiMode(false);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ºä¸ðµå º¯°æ
    public void SetViewMode(Player.ViewMode mode)
    {
        // ÇöÀç Ä«¸Þ¶ó¿Í Äµ¹ö½º ºñÈ°¼ºÈ­
        if (_viewModeArgs.ViewCamera) _viewModeArgs.ViewCamera.gameObject.SetActive(false);
        if (_viewModeArgs.Canvas_OnPlaying) _viewModeArgs.Canvas_OnPlaying.gameObject.SetActive(false);



        _cameras[(int)mode].gameObject.SetActive(true);
        _canvases_onPlaying[(int)mode].gameObject.SetActive(!_uiMode);
        _viewModeArgs.ViewMode = mode;
        _viewModeArgs.ViewCamera = _cameras[(int)mode];
        _viewModeArgs.Canvas_OnPlaying = _canvases_onPlaying[(int)mode];
        ViewModeChanged.Invoke(_viewModeArgs);
    }
    // ºä¸ðµå Åä±Û
    public void ToggleViewMode()
    {
        switch (_viewModeArgs.ViewMode)
        {
            case Player.ViewMode.FIRST_PERSON:
                SetViewMode(Player.ViewMode.TOPDOWN);
                break;
            case Player.ViewMode.TOPDOWN:
                SetViewMode(Player.ViewMode.FIRST_PERSON);
                break;
            default:
                break;
        }
    }

    // UiMode º¯°æ
    public void SetUiMode(bool b)
    {
        _uiMode = b;

        // InputSystem º¯°æÇÏ±â (Player <-> Ui)
        if (_uiMode)
        {
            _input.SwitchCurrentActionMap("UI");

            //1ÀÎÄª ºä¶ó¸é Ä¿¼­ º¸ÀÌ±â
            if (_viewModeArgs.ViewMode == Player.ViewMode.FIRST_PERSON)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            Time.timeScale = 0;
        }
        else
        {
            _input.SwitchCurrentActionMap("Player");
            //1ÀÎÄª ºä¶ó¸é Ä¿¼­ ¼û±è.
            if(_viewModeArgs.ViewMode == Player.ViewMode.FIRST_PERSON)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Time.timeScale = 1;
        }

        // Ui Äµ¹ö½º 
        Canvas_OnUi.gameObject.SetActive(b);
        // ÇÃ·¹À× Äµ¹ö½º
        if (_viewModeArgs.Canvas_OnPlaying) _viewModeArgs.Canvas_OnPlaying.gameObject.SetActive(!b);
    }

    public void OnActivateUiMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SetUiMode(true);

        }
    }
    public void OnDeactivateUiMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SetUiMode(false);
        }
    }


    public struct ViewModeChangedArgs
    {
        public Player.ViewMode ViewMode;
        public Camera ViewCamera;
        public Canvas Canvas_OnPlaying;
    }


}
