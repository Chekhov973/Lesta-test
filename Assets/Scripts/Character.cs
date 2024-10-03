using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private int MAX_HP;

    private int _hp;
    public int Max_hp => MAX_HP;
    public Rigidbody Rb => _movement.Rb;

    public Action<bool> OnStateChanged;
    public Action<int> OnHPChanged;



    public void SetCustomForce(Vector3 force)
    {
        _movement.AdditionalForce = force;
    }

    void Start()
    {
        _hp = MAX_HP;
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        _hp = Mathf.Clamp(_hp, 0, MAX_HP);

        if (_hp == 0)
            OnStateChanged?.Invoke(false);

        OnHPChanged?.Invoke(_hp);
    }

    public void Finish()
    {
        OnStateChanged?.Invoke(true);
    }
}
