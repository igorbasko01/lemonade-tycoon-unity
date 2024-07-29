using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNavigator : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPanelMapping
    {
        public UnityEngine.UI.Button button;
        public GameObject panel;
    }

    [SerializeField] private List<ButtonPanelMapping> buttonPanelMappings;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var mapping in buttonPanelMappings)
        {
            if (mapping.button != null && mapping.panel != null)
            {
                mapping.button.onClick.AddListener(() => NavigateToPanel(mapping.panel));
            }
        }
    }

    private void NavigateToPanel(GameObject targetPanel)
    {
        foreach (Transform child in transform.parent)
        {
            child.gameObject.SetActive(false);
        }

        targetPanel.SetActive(true);
    }
}
