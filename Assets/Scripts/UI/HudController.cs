using System;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Button showUpgradesButton;
        [SerializeField] private Slider timeSlider;
        [SerializeField] private GameObject upgradeAvailableText;

        private Character _character;
        private Stamina _stamina;

        private UpgradeManager _upgradeManager;
        private UpgradeMenuController _upgradePanel;
        private Timer _timer;

        private Tweener _tweener;

        private void Awake()
        {
            _character = GetComponentInParent<Character>();
            _stamina = _character.GetComponent<Stamina>();

            _upgradeManager = _character.GetComponentInChildren<UpgradeManager>();
            _upgradePanel = _character.GetComponentInChildren<UpgradeMenuController>();
            _timer = _character.GetComponentInChildren<Timer>();

            UpdateHealth(_character.Health);
            UpdateMaxHealth(_character.MaxHealth);
            UpdateStamina(_stamina.CurrentStamina);
            UpdateMaxStamina(_stamina.MaxStamina);

            _character.OnHealthChange += UpdateHealth;
            _character.OnMaxHealthChange += UpdateMaxHealth;
            _stamina.OnCurrentStaminaChange += UpdateStamina;
            _stamina.OnMaxStaminaChange += UpdateMaxStamina;

            _upgradeManager.OnAvailableLvlChange += UpdateUpgradeButtonLevel;
            _timer.OnCurrentCycleTimeChange += UpdateTimer;
        }

        public void ShowUpgradePanel()
        {
            _upgradePanel.Show();
        }

        private void UpdateHealth(float health)
        {
            if (_character.MaxHealth != 0)
                healthSlider.value = health / _character.MaxHealth;
            healthText.text = $"{health}/{_character.MaxHealth}";
        }

        private void UpdateMaxHealth(float maxHealth)
        {
            if (maxHealth != 0)
                healthSlider.value = _character.Health / maxHealth;
            healthText.text = $"{_character.Health}/{maxHealth}";
        }

        private void UpdateStamina(float stamina)
        {
            if (_stamina.MaxStamina != 0)

                staminaSlider.value = stamina / _stamina.MaxStamina;
        }

        private void UpdateMaxStamina(float maxStamina)
        {
            if (maxStamina != 0)
                staminaSlider.value = _stamina.CurrentStamina / maxStamina;
        }

        private void UpdateUpgradeButtonLevel(int lvlAvailable)
        {
            showUpgradesButton.GetComponentInChildren<TMP_Text>().text = lvlAvailable.ToString();
            var available = lvlAvailable > 0;
            showUpgradesButton.interactable = available;
            upgradeAvailableText.gameObject.SetActive(available);
            if (available && _tweener is not {active: true})
            {
                upgradeAvailableText.transform.rotation = Quaternion.identity;
                _tweener = upgradeAvailableText.transform.DOPunchRotation(new Vector3(0f, 0f, 10f), .5f)
                    .SetLoops(10000);
            }
        }

        private void UpdateTimer(float time)
        {
            timeSlider.value = time;
        }
    }
}