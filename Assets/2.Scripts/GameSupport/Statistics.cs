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
    float _startTime;


    private void Awake()
    {
        Spawner.OnEnemyDeathHandler += OnEnemyDeath;
        Spawner.OnEnemyTakeHitHandler += OnEnemyTakeHit;
        Player.OnPlayerTakeHit += OnPlayerTakeHit;
        _startTime = Time.time;
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
        
        ui.SetPlayTime((int)(Time.time - _startTime));
        
        ui.SetKill(_kill);
        
        ui.SetGiveDamage(_giveDamage);
        
        ui.SetTakeDamage(_takeDamage);

        ui.Show();
    }
}
