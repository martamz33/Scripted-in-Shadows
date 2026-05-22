using UnityEngine;
using TMPro;

public class InkUIUpdater : MonoBehaviour
{
    private TextMeshProUGUI myTextComponent;

    void Start()
    {
        myTextComponent = GetComponent<TextMeshProUGUI>();

        // Si el manager existe, le decimos que este es el nuevo texto a actualizar
        if (InkManager.instance != null)
        {
            InkManager.instance.RegisterUI(myTextComponent);
        }
    }
}