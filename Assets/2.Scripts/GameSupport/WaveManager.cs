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

        EnemySpawner.OnWaveEnd += OnWaveEnd;
    }

    void OnWaveEnd(int wave)
    {
        //1웨이브 끝나면 스킬 선택
        if (wave == 1)
        {
            Player player;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out player))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                        "Q 스킬을 선택하세요.",
                    ModeSwitcher,
                    FirstRewardSkills[0],
                    () =>
                    {

                        player.SetupSkill(FirstRewardSkills[0], "Q");
                    },

                    FirstRewardSkills[1],
                    () =>
                    {
                        player.SetupSkill(FirstRewardSkills[1], "Q");
                    },

                    FirstRewardSkills[2],
                    () =>
                    {
                        player.SetupSkill(FirstRewardSkills[2], "Q");
                    });
                }
            }
        }

        //3웨이브에 스킬 선택
        if (wave == 2)
        {
            Player player;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out player))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                        "E 스킬을 선택하세요.",
                    ModeSwitcher,
                    SecondRewardSkills[0],
                    () =>
                    {
                        player.SetupSkill(SecondRewardSkills[0], "E");
                    },

                    SecondRewardSkills[1],
                    () =>
                    {
                        player.SetupSkill(SecondRewardSkills[1], "E");
                    },

                    SecondRewardSkills[2],
                    () =>
                    {
                        player.SetupSkill(SecondRewardSkills[2], "E");
                    });
                }
            }
        }


        //4웨이브에 스킬 선택
        if (wave == 3)
        {
            Player player;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                if (playerObj.TryGetComponent(out player))
                {
                    SkillSelectionUi ui = Instantiate(WaveSkillSelectionUi);
                    ui.Init(
                        "R 스킬을 선택하세요.",
                    ModeSwitcher,
                    ThirdRewardSkills[0],
                    () =>
                    {
                        player.SetupSkill(ThirdRewardSkills[0], "R");
                    },

                    ThirdRewardSkills[1],
                    () =>
                    {
                        player.SetupSkill(ThirdRewardSkills[1], "R");
                    },

                    ThirdRewardSkills[2],
                    () =>
                    {
                        player.SetupSkill(ThirdRewardSkills[2], "R");
                    });
                }
            }
        }
    }
}
