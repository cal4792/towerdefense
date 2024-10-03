using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
        [SerializeField] private Image fillImage;
        [SerializeField] private DamageableEntity targetEntity;

        private void Start()
        {
            if (targetEntity == null)
            {
                targetEntity = GetComponentInParent<DamageableEntity>();
            }

            if (targetEntity != null)
            {
                targetEntity.OnHealthChanged.AddListener(UpdateHealthBar);
            }
            else
            {
                Debug.LogWarning("No DamageableEntity found for HealthBar.");
            }
        }

        private void UpdateHealthBar(float healthPercentage)
        {
            fillImage.fillAmount = healthPercentage;
        }

        private void OnDestroy()
        {
            if (targetEntity != null)
            {
                targetEntity.OnHealthChanged.RemoveListener(UpdateHealthBar);
            }
        }
    }
