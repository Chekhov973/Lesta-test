using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Character _characterPfb;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private TextMeshProUGUI _hpText;
    private Character _character;


    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _character = Instantiate(_characterPfb);
        _character.OnHPChanged += UpdateHpView;
        _character.transform.position = _spawnPoint.position;
        UpdateHpView(_character.Max_hp);
        _character.OnStateChanged += EndGame;
    }

    private void UpdateHpView(int currentHp)
    {
        _hpText.text = currentHp.ToString();
    }

    private void EndGame(bool isWin)
    {
        _character.OnStateChanged -= EndGame;

        (isWin ? _winPanel : _loosePanel).SetActive(true);
        _character.Rb.isKinematic = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
