using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;  

public class PlayerScript : MonoBehaviour
{
    public int points = 0;  

    void Start()
    {
       
    }

    void Update()
    {
        
    }
    private void OnGUI()
    {
        GUI.skin.label.fontSize = 50;

        GUI.Label(new Rect(10, 10, 400, 60), "Score: " + points);

        GUIStyle colorStyle = new GUIStyle(GUI.skin.label);
        colorStyle.fontSize = 35;

        colorStyle.normal.textColor = Color.yellow;
        GUI.Label(new Rect(10, Screen.height - 70, 300, 45), "Yellow coins = +1", colorStyle);

        colorStyle.normal.textColor = Color.red;
        GUI.Label(new Rect(10, Screen.height - 40, 300, 45), "Red coins = -1", colorStyle);
    }

}
