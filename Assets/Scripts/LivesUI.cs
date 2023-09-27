using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI lives;

    void Update()
    {
        lives.text = PlayerStats.Lives.ToString() + " LIVES";
    }
}
