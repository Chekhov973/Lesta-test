using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPlatform : BasePlatform
{
    [SerializeField] private float _changeGap;
    [SerializeField] private float _windForce;
    
    private List<Character> _characters;
    private Vector3 _windDirection;

    private void ChangeWindDirection()
    {
        _windDirection.x = Random.Range(-1f, 1f);
        _windDirection.z = Random.Range(-1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character target))
        {
            if (!_characters.Contains(target))
                _characters.Add(target);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character target))
        {
            if (_characters.Contains(target))
            {
                _characters.Remove(target);
                target.SetCustomForce(Vector3.zero);
            }
         
        }
    }


    void Start()
    {
        _characters = new List<Character>();
        ChangeWindDirection();
        StartCoroutine(ChangeDirectionDelayer());
    }

    private IEnumerator ChangeDirectionDelayer()
    {
        while(Application.isPlaying)
        {
            yield return new WaitForSeconds(_changeGap);
            ChangeWindDirection();
        }
    }

    void FixedUpdate()
    {
        for (int i =  0; i < _characters.Count; i++)
        {
            _characters[i].SetCustomForce(_windDirection * _windForce);
        }
    }
}
