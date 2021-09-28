using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextMeshProEdit : MonoBehaviour
{
    public int textSize;
    public int textAlligment;
    public bool autoSize;
    public Color textColor;
    [Header("Button Colors")]
    public Color normalColor;
    public Color highlightedColor;
    public Color selectedColor;
    public Color pressedColor;
    public Color disabledColor;

    public void ChangeButtonColors(string tag = "")
    {
        List<Button> buttons = GetAllButtons(tag);

        if (buttons == null || buttons.Count <= 0)
        {
            Debug.LogWarning("Change Button Color aborted: no Button objects were found'");
            return;
        }

        foreach (Button button in buttons)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = normalColor;
            colorBlock.disabledColor = disabledColor;
            colorBlock.selectedColor = selectedColor;
            colorBlock.highlightedColor = highlightedColor;
            colorBlock.pressedColor = pressedColor;
            button.colors = colorBlock;
        }
    }

    public void ChangeTextColor(string tag = "")
    {
        List<TextMeshProUGUI> tmObjects = GetAllTMGameObjects(tag);

        if (tmObjects == null || tmObjects.Count <= 0)
        {
            Debug.LogWarning("Change Color aborted: no TMP objects were found'");
            return;
        }

        foreach(TextMeshProUGUI obj in tmObjects)
        {
            obj.color = textColor;
        }
    }

    public List<TextMeshProUGUI> GetAllTMGameObjects(string tag = "")
    {
        if (tag != "")
        {
            return GetTaggedTMGameObjects(tag);
        }

        List<TextMeshProUGUI> listToReturn = new List<TextMeshProUGUI>();
        object[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject));
        
        foreach(object obj in allObjects)
        {
            GameObject go = (GameObject)obj;
            if (go == null)
            {
                return null;
            }

            TextMeshProUGUI tmComp = go.GetComponent<TextMeshProUGUI>();

            if (tmComp != null)
            {
                listToReturn.Add(tmComp);
            }
        }

        if (listToReturn != null && listToReturn.Count > 0)
        {
            return listToReturn;
        }
        else
        {
            Debug.LogError("No TMPro GameObjects where found in the scene");
            return null;
        }
    }

    public List<TextMeshProUGUI> GetTaggedTMGameObjects(string tag)
    {
        List<TextMeshProUGUI> listToReturn = new List<TextMeshProUGUI>();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in taggedObjects)
        {
            TextMeshProUGUI tmComp = go.GetComponent<TextMeshProUGUI>();
            if (tmComp != null)
            {
                listToReturn.Add(tmComp);
            }
        }

        if (listToReturn != null && listToReturn.Count > 0)
        {
            return listToReturn;
        }
        else
        {
            Debug.LogError("No Tagged TMPro GameObjects where found in the scene");
            return null;
        }

    }

    public List<Button> GetAllButtons(string tag = "")
    {
        if (tag != "")
        {
            return GetTaggedButtons(tag);
        }

        List<Button> listToReturn = new List<Button>();
        object[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (object obj in allObjects)
        {
            GameObject go = (GameObject)obj;
            if (go == null)
            {
                return null;
            }

            Button buttonComp = go.GetComponent<Button>();

            if (buttonComp != null)
            {
                listToReturn.Add(buttonComp);
            }
        }

        if (listToReturn != null && listToReturn.Count > 0)
        {
            return listToReturn;
        }
        else
        {
            Debug.LogError("No TMPro GameObjects where found in the scene");
            return null;
        }
    }

    public List<Button> GetTaggedButtons (string tag)
    {
        List<Button> listToReturn = new List<Button>();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in taggedObjects)
        {
            Button comp = go.GetComponent<Button>();
            if (comp != null)
            {
                listToReturn.Add(comp);
            }
        }

        if (listToReturn != null && listToReturn.Count > 0)
        {
            return listToReturn;
        }
        else
        {
            Debug.LogError("No Tagged TMPro GameObjects where found in the scene");
            return null;
        }

    }
}
