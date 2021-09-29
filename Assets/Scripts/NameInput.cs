using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    

    void Start()
    {
        var input = gameObject.GetComponent<InputField>();

        input.onEndEdit.AddListener(SubmitName);  // This also works
    }

    public void SubmitName(string name)
    {
        GameManager.gm.playerName = name;
    }
}
