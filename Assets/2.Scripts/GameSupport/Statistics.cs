using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Statistics : MonoBehaviour
{
    public EnemySpawner Spawner;
    public Player Player;
    public StatisticsUi UiPrefab;

    //잡은 적
    //준 데미지
    //받은 데미지
    int _kill;
    int _giveDamage;
    int _takeDamage;


    private void Awake()
    {
        Spawner.OnEnemyDeathHandler += OnEnemyDeath;
        Spawner.OnEnemyTakeHitHandler += OnEnemyTakeHit;
        Player.OnPlayerTakeHit += OnPlayerTakeHit;
        Spawner.OnWaveEnd += OnNextWave;
    }
    void OnNextWave(int wave)
    {
        //게임 클리어시 띄우기
        if(Spawner.FinalWave == wave)
        {
            ShowStatistics();
        }
    }


    public void OnEnemyDeath(LivingEntity enemy)
    {
        _kill++;
    }
    public void OnEnemyTakeHit(Enemy enemy, int damage)
    {
        _giveDamage += damage;
    }
    public void OnPlayerTakeHit(Player player, int damage)
    {
        _takeDamage += damage;
    }

    public void ShowStatistics()
    {
        StatisticsUi ui = Instantiate(UiPrefab);
        
        ui.SetPlayTime((int)Time.time);
        
        ui.SetKill(_kill);
        
        ui.SetGiveDamage(_giveDamage);
        
        ui.SetTakeDamage(_takeDamage);

        ui.Show();
    }
}
