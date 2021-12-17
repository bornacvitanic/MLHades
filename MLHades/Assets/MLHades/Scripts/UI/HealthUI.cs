using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObjectReference targetGameObject;
    [Header("Optional")]
    [SerializeField] private Text healthText;

    private Slider sliderComponent;

    private void Awake()
    {
        sliderComponent = GetComponent<Slider>();
    }

    public void UpdateHealthBar()
    {
        Health targetHealthComponent = targetGameObject.GetReference().GetComponent<Health>();
        var health = targetHealthComponent.GetHealth();
        var max_health = targetHealthComponent.GetMaxHealth();
        sliderComponent.maxValue = max_health;
        sliderComponent.value = health;
        if(healthText != null)
        {
            healthText.text = health.ToString() + "/" + max_health.ToString();
        }
    }
}
