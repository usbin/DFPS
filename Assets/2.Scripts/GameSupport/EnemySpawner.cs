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

    public Wave[] Waves;        //***���̺�: �ε��� 0���� ����
    public CapsuleCollider SpawnArea;  //������ ����

    bool _isCleared = false;

    int _currentWave = 0;       //***���� ���̺�: �ε��� 1���� ��.
    int _remainEnemyToSpawn;    //�̹� ���̺꿡 �����ؾ� �� �� ��
    int _deadEnemyInWave;       //�̹� ���̺꿡 ���� �� ��
    float _remainSpawnCooltime; // ���� �ֱ�
    const float cSpawnCooltime = 0.5f;       // ���� �������� ���� �ð�
    private void Awake()
    {
    }

    private void Start()
    {
        NextWave();
    }

    private void Update()
    {
        //���� �����ؾ� �� ���� �����ִٸ� ���� �ð����� ����
        if (_remainEnemyToSpawn > 0) {
            _remainSpawnCooltime -= Time.deltaTime;
            if (_remainSpawnCooltime <= 0)
            {
                SpawnEnemy();
                _remainSpawnCooltime = cSpawnCooltime;
            }
        }

        //���� ��� �����߰� �̹� ���̺꿡 ���� ���� ��ȯ�� �� ���� ���ٸ� => ���� ���̺�
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
        Debug.Log("���̺�" + _currentWave + " ����");
        _remainEnemyToSpawn = Waves[_currentWave - 1].EnemyAmount;
        _deadEnemyInWave = 0;
        RemainEnemyTextUi.text = _remainEnemyToSpawn.ToString() ;

    }
    public void SpawnEnemy()
    {

        //���� ��������(_spawnArea Ÿ�� �ݰ� �ȿ���)
        Vector3 center = SpawnArea.transform.position;
        float xRadius = SpawnArea.radius * SpawnArea.transform.localScale.x;
        float zRadius = SpawnArea.radius * SpawnArea.transform.localScale.z;

        Vector2 randomBase = Random.insideUnitCircle;
        Vector3 randomPoint = new Vector3(center.x + xRadius * randomBase.x, 2.5f, center.z + zRadius * randomBase.y);




        // �ش� ��ġ�� �浹ü�� ���� ��(�뷫 2���� �ݰ�)
        LayerMask layerMask = 1<<LayerMask.NameToLayer("Item");
        if(Physics.OverlapSphere(randomPoint, 2, ~layerMask).Length == 0)
        {
            //���� ����
            StartCoroutine(SpawnAfterEffect(randomPoint));
            
        }
    }
    IEnumerator SpawnAfterEffect(Vector3 randomPoint)
    {
        _remainEnemyToSpawn--;
        GameObject spawnEffect= Instantiate(SpawnEffectPrefab, new Vector3(randomPoint.x, 1, randomPoint.z), Quaternion.identity);//�� 2�� �� ��ȯ
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
