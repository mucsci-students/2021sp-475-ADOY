using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int eggCount;
    public Text eggCountText;
    public GameObject levelmanager;
    public Text scoretext;
    private string output = "/8";
    public bool disableText = false;

    void Start()
    {
        
        eggCount = 0;
    }

    void Update()
    {
        if (!disableText)
        {
        scoretext.text = eggCount.ToString() + output;
        if (eggCount == 8)
        {
            //GameObject.Find("LevelChanger").GetComponent<LevelManager>.
            PlayerPrefs.SetFloat("completed", 1);
            GameObject.Find("LevelChanger").GetComponent<LevelManager>().GameEnd();
        }
        }
    }
}
