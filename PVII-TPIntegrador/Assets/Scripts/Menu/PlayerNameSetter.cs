using UnityEngine;
using TMPro;

public class PlayerNameSetter : MonoBehaviour
{
    public TMP_InputField nameInput;

    void Start()
    {
        // cargar nombre guardado si es que existe
        if (PlayerPrefs.HasKey("PlayerName"))
            nameInput.text = PlayerPrefs.GetString("PlayerName");
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.Save();
    }
}
