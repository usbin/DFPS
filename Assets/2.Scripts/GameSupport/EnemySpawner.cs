using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event System.Action<int> OnNextWave;
    

    public Wave[] Waves;        //***���̺�: �ε��� 0���� ����
    public CapsuleCollider SpawnArea;  //������ ����

    int _currentWave = 0;       //***���� ���̺�: �ε��� 1���� ��.
    int _remainEnemyToSpawn;    //�̹� ���̺꿡 �����ؾ� �� �� ��
    int _deadEnemyInWave;       //�̹� ���̺꿡 ���� �� ��
    float _remainSpawnCooltime; // ���� �ֱ�
    const float cSpawnCooltime = 0.5f;       // ���� �������� ���� �ð�
    ParticleSystem _enemyDeathEffector;

    private void Awake()
    {
        _enemyDeathEffector = GetComponent<ParticleSystem>();
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
        if (Waves.Length > _currentWave)
        {
            if (_remainEnemyToSpawn == 0 && _deadEnemyInWave == Waves[_currentWave - 1].EnemyAmount)
            {
                NextWave();
            }
        }
    }
    public void NextWave()
    {
        _currentWave++;
        OnNextWave.Invoke(_currentWave);
        Debug.Log("���̺�" + _currentWave + " ����");
        _remainEnemyToSpawn = Waves[_currentWave - 1].EnemyAmount;
        _deadEnemyInWave = 0;

    }
    public void SpawnEnemy()
    {

        //���� ��������(_spawnArea Ÿ�� �ݰ� �ȿ���)
        Vector3 center = SpawnArea.transform.position;
        float xRadius = SpawnArea.radius * SpawnArea.transform.localScale.x;
        float zRadius = SpawnArea.radius * SpawnArea.transform.localScale.z;

        Vector2 randomBase = Random.insideUnitCircle;
        Vector3 randomPoint = new Vector3(center.x + xRadius * randomBase.x, center.y, center.z + zRadius * randomBase.y);




        // �ش� ��ġ�� �浹ü�� ���� ��(�뷫 2���� �ݰ�)
        LayerMask layerMask = 1<<LayerMask.NameToLayer("Item");
        if(Physics.OverlapSphere(randomPoint, 2, ~layerMask).Length == 0)
        {
            //���� ����
            Wave currentWaveData = Waves[_currentWave - 1];
            int randomIndex = Random.Range(0, currentWaveData.EnemySpecies.Length);
            Enemy enemy = Instantiate(currentWaveData.EnemySpecies[randomIndex], new Vector3(randomPoint.x, 1, randomPoint.z), Quaternion.identity);
            enemy.Atk = currentWaveData.Atk;
            enemy.Def = currentWaveData.Def;
            enemy.Speed = currentWaveData.Speed;
            enemy.MaxHp = currentWaveData.maxHp;
            enemy.OnDeath += OnEnemyDeath;
            enemy.DeathEffector = _enemyDeathEffector;

            _remainEnemyToSpawn--;
        }
    }
    public void OnEnemyDeath(LivingEntity enemy)
    {
        _deadEnemyInWave++;
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
