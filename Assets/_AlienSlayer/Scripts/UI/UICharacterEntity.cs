using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Random = UnityEngine.Random;

namespace drland.AlienSlayer
{
    [Serializable]
    public class CharacterUIInfo
    {
        public string Name;
        public int Health;
    }
    
    public class UICharacterEntity : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _healthLayer;

        private IHasProgress _progress;

        public void Init(CharacterUIInfo info, IHasProgress objectHasProgress)
        {
            _nameText.text = info.Name;
            _healthText.text = info.Health.ToString();
            _progress = objectHasProgress;
            _progress.OnProgressChanged += UpdateHeathBarValue;
        }

        private void UpdateHeathBarValue(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            _healthText.text = e.CurrentValue.ToString();
            _healthBar.fillAmount = e.ProgressNormalized;
            _healthLayer.DOFillAmount(e.ProgressNormalized, 0.3f).SetEase(Ease.InCubic);
        }
    }
}