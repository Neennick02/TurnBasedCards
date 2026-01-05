using TMPro;
using UnityEngine;

public class HealthPopup : MonoBehaviour
{
    [SerializeField] GameObject DamagePopup;
    [SerializeField] GameObject HealPopup;

    public void Create(Vector3 pos, int amount, bool isDamage)
    {

        GameObject prefab = isDamage ? DamagePopup : HealPopup;
        GameObject popUpGameObject = Instantiate(prefab, pos, Quaternion.identity);

        PopUp popUp = popUpGameObject.GetComponent<PopUp>();

        popUp.SetText(amount);
    }
}
