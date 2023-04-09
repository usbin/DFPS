using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public int FinalWave => Waves.Length;
    public event System.Action<int> OnWaveStart;
    public event System.Action<int> OnWaveEnd;
    public event System.Action<LivingEntity> OnEnemyDeathHandler;
    public event System.Action<Enemy, int> OnEnemyTakeHitHandler;
    public UiController UiController;
    public GameObject SpawnEffectPrefab;
    public TMPro.TextMeshProUGUI RemainEnemyTextUi;

    public Wave[] Waves;        //***웨이브: 인덱스 0부터 저장
    public CapsuleCollider SpawnArea;  //스폰할 지역

    bool _isCleared = false;

    int _currentWave = 0;       //***현재 웨이브: 인덱스 1부터 셈.
    int _remainEnemyToSpawn;    //이번 웨이브에 스폰해야 할 적 수
    int _deadEnemyInWave;       //이번 웨이브에 죽은 적 수
    float _remainSpawnCooltime; // 스폰 주기
    const float cSpawnCooltime = 0.5f;       // 다음 스폰까지 남은 시간
    private void Awake()
    {
    }

    private void Start()
    {
        NextWave();
    }

    private void Update()
    {
        //아직 스폰해야 할 적이 남아있다면 일정 시간마다 스폰
        if (_remainEnemyToSpawn > 0) {
            _remainSpawnCooltime -= Time.deltaTime;
            if (_remainSpawnCooltime <= 0)
            {
                SpawnEnemy();
                _remainSpawnCooltime = cSpawnCooltime;
            }
        }

        //적을 모두 스폰했고 이번 웨이브에 죽은 적이 소환한 적 수와 같다면 => 다음 웨이브
        if(_currentWave == 0)
        {
            NextWave();
        }
        else if ( _remainEnemyToSpawn == 0 && _deadEnemyInWave == Waves[_currentWave - 1].EnemyAmount)
        {
            if (FinalWave > _currentWave)
            {
                if (OnWaveEnd != null) OnWaveEnd.Invoke(_currentWave);
                NextWave();
            }
            else if(_currentWave == FinalWave && !_isCleared)
            {
                _isCleared = true;
                if (OnWaveEnd != null) OnWaveEnd.Invoke(_currentWave);
            }
        }
        
    }
    public void NextWave()
    {
        _currentWave++;
        if(OnWaveStart != null) OnWaveStart.Invoke(_currentWave);
        Debug.Log("웨이브" + _currentWave + " 시작");
        _remainEnemyToSpawn = Waves[_currentWave - 1].EnemyAmount;
        _deadEnemyInWave = 0;
        RemainEnemyTextUi.text = _remainEnemyToSpawn.ToString() ;

    }
    public void SpawnEnemy()
    {

        //랜덤 스폰지점(_spawnArea 타원 반경 안에서)
        Vector3 center = SpawnArea.transform.position;
        float xRadius = SpawnArea.radius * SpawnArea.transform.localScale.x;
        float zRadius = SpawnArea.radius * SpawnArea.transform.localScale.z;

        Vector2 randomBase = Random.insideUnitCircle;
        Vector3 randomPoint = new Vector3(center.x + xRadius * randomBase.x, 2.5f, center.z + zRadius * randomBase.y);




        // 해당 위치에 충돌체가 없을 때(대략 2미터 반경)
        LayerMask layerMask = 1<<LayerMask.NameToLayer("Item");
        if(Physics.OverlapSphere(randomPoint, 2, ~layerMask).Length == 0)
        {
            //랜덤 몬스터
            StartCoroutine(SpawnAfterEffect(randomPoint));
            
        }
    }
    IEnumerator SpawnAfterEffect(Vector3 randomPoint)
    {
        _remainEnemyToSpawn--;
        GameObject spawnEffect= Instantiate(SpawnEffectPrefab, new Vector3(randomPoint.x, 1, randomPoint.z), Quaternion.identity);//약 2초 후 소환
        yield return new WaitForSeconds(2.5f);
        Wave currentWaveData = Waves[_currentWave - 1];
        int randomIndex = Random.Range(0, currentWaveData.EnemySpecies.Length);
        Enemy enemy = Instantiate(currentWaveData.EnemySpecies[randomIndex], new Vector3(randomPoint.x, 1, randomPoint.z), Quaternion.identity);
        enemy.Atk = currentWaveData.Atk + currentWaveData.EnemySpecies[randomIndex].Atk;
        enemy.Def = currentWaveData.Def + currentWaveData.EnemySpecies[randomIndex].Def;
        enemy.Speed = currentWaveData.Speed + currentWaveData.EnemySpecies[randomIndex].Speed;
        enemy.MaxHp = currentWaveData.maxHp + currentWaveData.EnemySpecies[randomIndex].MaxHp;
        enemy.OnDeath += OnEnemyDeath;
        enemy.OnDeath += OnEnemyDeathHandler;
        enemy.OnEnemyHit += UiController.OnEnemyTakeHit;
        enemy.OnEnemyHit += OnEnemyTakeHitHandler;
        Destroy(spawnEffect.gameObject);
        

    }
    public void OnEnemyDeath(LivingEntity enemy)
    {
        _deadEnemyInWave++;
        RemainEnemyTextUi.text = (Waves[_currentWave-1].EnemyAmount - _deadEnemyInWave).ToString();
    }

    [System.Serializable]
    public struct Wave
    {
        public Enemy[] EnemySpecies;
        public int EnemyAmount;
        public int Atk;
        public int Def;
        public int Speed;
        public int maxHp;
    }
}
