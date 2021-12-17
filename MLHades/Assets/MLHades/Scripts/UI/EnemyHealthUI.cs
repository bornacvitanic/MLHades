using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Slider sliderComponent;
    [SerializeField] private Health healthComponent;

    private void Awake()
    {
        healthComponent.OnHealthChangedEvent += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        sliderComponent.gameObject.SetActive(true);
        var health = healthComponent.GetHealth();
        var max_health = healthComponent.GetMaxHealth();
        sliderComponent.maxValue = max_health;
        sliderComponent.value = health;
    }

    private void OnDestroy()
    {
        healthComponent.OnHealthChangedEvent -= UpdateHealthBar;
    }
}
