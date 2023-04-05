using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SkillSelectionUi : MonoBehaviour
{
    public Button BtnFirstSkill;
    public Button BtnSecondSkill;
    public Button BtnThirdSkill;

    public void Init(ModeSwitcher modeSwitcher, string firstSkillText, UnityAction onClickFirstSkill, 
        string secondSkillText, UnityAction onClickSecondSkill,
        string thirdSkillText, UnityAction onClickThirdSkill)
    {
        modeSwitcher.SetUiMode(true);
        BtnFirstSkill.onClick.RemoveAllListeners();
        BtnSecondSkill.onClick.RemoveAllListeners();
        BtnThirdSkill.onClick.RemoveAllListeners();


        BtnFirstSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = firstSkillText;
        BtnSecondSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = secondSkillText;
        BtnThirdSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = thirdSkillText;
        BtnFirstSkill.onClick.AddListener(onClickFirstSkill);
        BtnSecondSkill.onClick.AddListener(onClickSecondSkill);
        BtnThirdSkill.onClick.AddListener(onClickThirdSkill);

        //¹¹µç ¼±ÅÃÇÏ¸é Ã¢ ´ÝÈû.
        UnityAction exit = () =>
        {
            modeSwitcher.SetUiMode(false);
            Destroy(gameObject);
        };
        BtnFirstSkill.onClick.AddListener(exit);
        BtnSecondSkill.onClick.AddListener(exit);
        BtnThirdSkill.onClick.AddListener(exit);

    }


}
