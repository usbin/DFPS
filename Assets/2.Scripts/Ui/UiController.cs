using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UiController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI AimPoint;
    public Slider PlayerHpBar;
    public DamageEffect EnemyDamageEffect;
    public Canvas DamageCanvas;
    public Canvas Canvas;
    public TMPro.TextMeshProUGUI WaveEndText;
    public TMPro.TextMeshProUGUI GameOverTextUi;
    public Player Player;
    public EnemySpawner WaveManager;
    public PlayerInput Input;
    public Statistics Statistics;
    Weapon _currentWeapon;

    Color _aimColor = Color.red;
    Color _nonAimColor = Color.gray;
    
    // Start is called before the first frame update
    void Awake()
    {
        WaveManager.OnWaveStart += OnWaveStart;
        WaveManager.OnWaveEnd += OnWaveEnd;
        Player.OnDeath += OnPlayerDead;
    }

    // Update is called once per frame
    void Update()
    {
        //hp바 갱신
        PlayerHpBar.maxValue = Player.MaxHp;
        PlayerHpBar.value = Player.Hp;

        //사정거리에 적이 있을 때 조준점 색깔 바꾸기
        if(_currentWeapon != null && _currentWeapon.Type == Weapon.WeaponType.Gun)
        {
            Ray ray = Player.ViewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, ((Gun)_currentWeapon).Distance+Player.Distance, 1 << LayerMask.NameToLayer("Enemy")))
            {

                AimPoint.color = _aimColor;
            }
            else AimPoint.color = _nonAimColor;
        }

    }
    public void OnAttack(Weapon weapon)
    {
        StartCoroutine(GunAttackEffect());
    }
    public void OnEnemyTakeHit(Enemy enemy, int damage)
    {
        DamageEffect effect = Instantiate(EnemyDamageEffect);
        float randomX = Random.Range(0.5f, 1f);
        effect.Show(damage.ToString(), enemy.DamageViewPoint.position+Vector3.right*randomX, Player.ViewCamera.transform.forward);
        effect.transform.SetParent(DamageCanvas.transform);

    }
    public void OnPlayerDead(LivingEntity entity)
    {
        if (entity.tag == "Player")
        {
            //죽음 띄움.
            TMPro.TextMeshProUGUI text = Instantiate(GameOverTextUi);
            text.transform.SetParent(Canvas.transform, false);
            text.text = "Game Over!";
            StartCoroutine(Delay());
        }
    }
    IEnumerator GunAttackEffect()
    {
        Vector3 orgScale = new Vector3(1, 1, 1);
        Vector3 effectScale = orgScale * 1.5f;
        AimPoint.transform.localScale = effectScale;

        float duration = 0.05f;
        while (duration>0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

        AimPoint.transform.localScale = orgScale;


    }
    public void OnEquipWeapon(Weapon weapon)
    {
        if(weapon.Type == Weapon.WeaponType.Gun)
        {
            //조준점 보이기
            AimPoint.gameObject.SetActive(true);
            _currentWeapon = weapon;
        }
        else
        {
            AimPoint.gameObject.SetActive(false);
        }
    }

    public void OnWaveStart(int wave)
    {
        TMPro.TextMeshProUGUI text = Instantiate(WaveEndText);
        text.transform.SetParent(Canvas.transform, false);
        text.text = "Wave " + (wave);
    }
    public void OnWaveEnd(int wave)
    {
        // 웨이브 종료
        if (wave == WaveManager.FinalWave)
        {
            Input.DeactivateInput();
            TMPro.TextMeshProUGUI text = Instantiate(WaveEndText);
            text.transform.SetParent(Canvas.transform, false);
            text.text = "Game Clear!!!";
            StartCoroutine(Delay());
        }

    }
    IEnumerator Delay()
    {
        float delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        Cursor.lockState = CursorLockMode.None;
        Statistics.ShowStatistics();
    }
}
