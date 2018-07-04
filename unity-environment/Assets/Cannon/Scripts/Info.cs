using UnityEngine;
using MLAgents;
using System.Collections.Generic;

public class Info : MonoBehaviour
{
    private GUIStyle style;
    private Texture2D bg;
    private CannonAgent[] agents;
    private CannonAgent agent;
    private int scale = 1;

    private void Start()
    {
        agents = FindObjectsOfType<CannonAgent>();

        style = new GUIStyle
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;
        bg = TextureHelper.CreateTexture(Screen.width, Screen.height);
        bg.Fill(new Color32(50, 50, 50, 255));
    }

    private void Update()
    {
        if (Input.inputString != "")
        {
            char c = Input.inputString[0];
            int i = (int)c - 48;
            if (i > 0 && i < 10)
            {
                string game = "Game" + i;
                foreach (Agent a in agents)
                {
                    if (a.transform.parent.name == game)
                    {
                        agent = (CannonAgent)((agent == a) ? null : a);
                        break;
                    }
                }
            }
            else if (i == -3)
            {
                // - key
                scale--;
            }
            else if (i == -5)
            {
                // + key
                scale++;
            }
            scale = Mathf.Clamp(scale, 1, 10);
        }
    }

    private void OnGUI()
    {
        if (agent != null)
        {
            int spacing = 10;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg);

            string info = agent.transform.parent.name + "   -   " + 
                               agent.shots + " Shots   " + agent.hits + " Hits";
            
            List<Texture2D> tex = agent.VisualObservationsTextureList; 
            if (tex.Count > 0)
            {
                info += "   -   Visual Observations x " + scale;

                int x = 10;
                int y = 40;
                int width = 0;
                foreach (Texture2D t in tex)
                {
                    if (y + t.height * scale > Screen.height)
                    {
                        x += width;
                        y = 40;
                    }
                    GUI.DrawTexture(new Rect(x, y, t.width * scale, t.height * scale), t);
                    width = Mathf.Max(width, t.width * scale + spacing);
                    y += (t.height * scale + spacing);
                } 
            }

            GUI.Label(new Rect(10, 10, 300, 15), info, style);
        }
    }
}
