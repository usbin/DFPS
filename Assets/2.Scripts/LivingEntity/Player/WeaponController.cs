using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
public class WeaponController : MonoBehaviour
{
    public Transform GunHold;
    public Weapon InitialWeapon;
    public UnityEvent<Weapon> OnEquipWeapon;
    public UnityEvent<Weapon> OnAttack;

    protected Weapon equippedWeapon;
    Player _player;
    PlayerInput _input;
    Player.ViewMode _viewMode;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
    }

    protected virtual void Start()
    {
        if (InitialWeapon) EquipWeapon(InitialWeapon);
    }
    private void Update()
    {
        switch (_viewMode)
        {
            case Player.ViewMode.FIRST_PERSON:
                // �Ϲ� ����
                if (_input.actions["NormalAttack"].ReadValue<float>() > 0
                    && equippedWeapon)
                {
                    //1��Ī�� ��
                    //: ī�޶��� ���콺 ��ġ�� ����.
                    Ray ray = _player.ViewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    
                    WeaponAttackArgs args;
                    args.Attacker = _player;
                    args.Origin = ray.origin;
                    args.Direction = ray.direction;
                    
                    if(equippedWeapon.NormalAttack(args)
                        && OnAttack != null) OnAttack.Invoke(equippedWeapon);
                }
                break;
            case Player.ViewMode.TOPDOWN:
                // �Ϲ� ����
                if (_input.actions["NormalAttack"].ReadValue<float>() > 0
                    && equippedWeapon)
                {
                    //���콺 ���������� ���
                    Ray ray = _player.ViewCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Plane plane = new Plane(Vector3.up, Vector3.zero);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        Vector3 point = ray.GetPoint(distance);
                        Vector3 direction = point - ray.origin;
                        direction.y = 0;

                        WeaponAttackArgs args;
                        args.Attacker = _player;
                        args.Origin = _player.transform.position;   //�÷��̾� ��ġ�� ����������
                        args.Direction = direction;
                        if (equippedWeapon.NormalAttack(args)
                        && OnAttack != null) OnAttack.Invoke(equippedWeapon);
                    }
                }
                break;
            default:
                break;
        }

        
    }
    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapon)
        {
            _player.Atk -= equippedWeapon.Atk;
            Destroy(equippedWeapon.gameObject);
        }


        //weapon�� Ÿ�Կ� ���� ���� ��ġ�� �ٸ�.
        switch (weapon.Type)
        {
            case Weapon.WeaponType.Gun:
                equippedWeapon = Instantiate(weapon, GunHold.position, GunHold.rotation);
                equippedWeapon.gameObject.layer = GunHold.gameObject.layer;
                equippedWeapon.transform.parent = GunHold;
                break;
            default:
                break;
        }
        OnEquipWeapon.Invoke(equippedWeapon);
    }

    public void OnViewModeChanged(ModeSwitcher.ViewModeChangedArgs args)
    {
        _viewMode = args.ViewMode;
        // �ѱ� �� ���̰�
        
    }

    
}