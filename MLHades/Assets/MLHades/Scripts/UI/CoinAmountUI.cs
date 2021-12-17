using UnityEngine;
using UnityEngine.UI;

public class CoinAmountUI : MonoBehaviour
{
    [SerializeField] private IntValue coins;
    [SerializeField] private Text coinAmountText;

    public void UpdateCoinsAmount()
    {
        if(coinAmountText != null)
        {
            coinAmountText.text = coins.value.ToString();
        }
    }
}
