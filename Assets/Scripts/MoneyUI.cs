using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{

    public TextMeshProUGUI money;
    void Update()
    {
        money.text = "$" + PlayerStats.Money.ToString();
    }
}
