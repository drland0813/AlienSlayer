using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private UICharacterEntity _playerInfo;

        private void Start()
        {
            _playerInfo.Init(Player.Instance.Info, Player.Instance.HealthComponent);
        }
    }
}
