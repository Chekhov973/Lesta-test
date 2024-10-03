using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlatform : BasePlatform
{
    [SerializeField] private float _coolDown;
    [SerializeField] private float _timeOut;
    [SerializeField] private int _damage;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _colorCharged;
    [SerializeField] private Color _colorPrepare;
    [SerializeField] private Color _colorDamage;

    private List<Character> _characters;
    private bool _isCharged;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character target))
        {
            if (!_characters.Contains(target))
                _characters.Add(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_characters.Count > 0 && _isCharged)
        {
            StartCoroutine(PreDamage());
        }
    }

    private IEnumerator PreDamage()
    {
        _isCharged = false;
        SetColor(_colorPrepare);

        yield return new WaitForSeconds(_timeOut);
        TakeDamage();
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);

        SetColor(_colorCharged);

        yield return new WaitForSeconds(_coolDown - 0.5f);

        _isCharged = true;
    }

    private void TakeDamage()
    {
        SetColor(_colorDamage);


        for (int i = 0; i < _characters.Count; i++)
            _characters[i].GetDamage(_damage);

        StartCoroutine(CoolDown());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character target))
        {
            if (_characters.Contains(target))
                _characters.Remove(target);
        }
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    void Start()
    {
        _characters = new List<Character>();
        _isCharged = true;
        SetColor(_colorCharged);
    }
}
