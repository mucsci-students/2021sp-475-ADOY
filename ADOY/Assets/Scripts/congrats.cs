using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class congrats : MonoBehaviour
{
    public Text context;
    // Start is called before the first frame update
    void Start()
    {
        float complet = PlayerPrefs.GetFloat("completed");
        if(complet == 1)
           context.enabled = true;
        else
        {
            context.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
