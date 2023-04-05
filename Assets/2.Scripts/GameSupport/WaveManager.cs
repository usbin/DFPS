using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public CombatSystem CombatSystem;
    public EnemySpawner EnemySpawner;
    public SkillSelectionUi WaveSkillSelectionUi;
    public ModeSwitcher ModeSwitcher;

    public BaseSkill[] FirstRewardSkills = new BaseSkill[3];
    public BaseSkill[] SecondRewardSkills = new BaseSkill[3];
    public BaseSkill[] ThirdRewardSkills = new BaseSkill[3];



    private void Awake()
    {

        EnemySpawner.OnNextWave += OnNextWave;
    }

    void OnNextWave(int wave)
    {
        //2웨이브에 스킬 선택
        if (wave == 2)
        {
            PlayerSkillController playerSkillController;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out playerSkillController))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                    ModeSwitcher,
                    FirstRewardSkills[0].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(FirstRewardSkills[0], "Q");
                    },

                    FirstRewardSkills[1].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(FirstRewardSkills[1], "Q");
                    },

                    FirstRewardSkills[2].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(FirstRewardSkills[2], "Q");
                    });
                }
            }
        }

        //3웨이브에 스킬 선택
        if (wave == 3)
        {
            PlayerSkillController playerSkillController;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out playerSkillController))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                    ModeSwitcher,
                    SecondRewardSkills[0].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(SecondRewardSkills[0], "E");
                    },

                    SecondRewardSkills[1].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(SecondRewardSkills[1], "E");
                    },

                    SecondRewardSkills[2].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(SecondRewardSkills[2], "E");
                    });
                }
            }
        }


        //4웨이브에 스킬 선택
        if (wave == 4)
        {
            PlayerSkillController playerSkillController;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out playerSkillController))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                    ModeSwitcher,
                    ThirdRewardSkills[0].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(ThirdRewardSkills[0], "R");
                    },

                    ThirdRewardSkills[1].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(ThirdRewardSkills[1], "R");
                    },

                    ThirdRewardSkills[2].SkillName,
                    () =>
                    {
                        playerSkillController.SetupSkill(ThirdRewardSkills[2], "R");
                    });
                }
            }
        }
    }
}
