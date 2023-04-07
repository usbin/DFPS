using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StatisticsUi : MonoBehaviour
{
    public GameObject Panel;
    public TMPro.TextMeshProUGUI PlayTimeTextUi;
    public TMPro.TextMeshProUGUI KillTextUi;
    public TMPro.TextMeshProUGUI GiveDamageTextUi;
    public TMPro.TextMeshProUGUI TakeDamageTextUi;
    public Button ToTitleButton;

    private void Awake()
    {
        Panel.gameObject.SetActive(false);
        PlayTimeTextUi.transform.parent.gameObject.SetActive(false);
        KillTextUi.transform.parent.gameObject.SetActive(false);
        GiveDamageTextUi.transform.parent.gameObject.SetActive(false);
        TakeDamageTextUi.transform.parent.gameObject.SetActive(false);
        ToTitleButton.gameObject.SetActive(false);
        ToTitleButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    public void SetPlayTime(int second)
    {
        int hour = second / 60 / 60;
        int min = second / 60 % 60;
        int sec = second % 60;
        PlayTimeTextUi.text = hour+"시간 "+min+"분 " + sec + "초";
    }
    public void SetKill(int kill)
    {
        KillTextUi.text = kill.ToString();
    }
    public void SetGiveDamage(int damage)
    {
        GiveDamageTextUi.text = damage.ToString();
    }
    public void SetTakeDamage(int damage)
    {
        TakeDamageTextUi.text = damage.ToString();
    }
    public void Show()
    {
        StartCoroutine(ShowEffect());
    }

    IEnumerator ShowEffect()
    {
        float delay;
        delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        Panel.gameObject.SetActive(true);
        PlayTimeTextUi.transform.parent.gameObject.SetActive(true);
        delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        KillTextUi.transform.parent.gameObject.SetActive(true);
        delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        GiveDamageTextUi.transform.parent.gameObject.SetActive(true);
        delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        TakeDamageTextUi.transform.parent.gameObject.SetActive(true);
        delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        ToTitleButton.gameObject.SetActive(true);


    }
}
