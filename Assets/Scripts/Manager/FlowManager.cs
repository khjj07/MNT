using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public GameState[] player_list;
    private int flag = 0;
 
    public void Next()
    {

        flag++;
        if(player_list.Length<=flag)
        {
            flag = 0;
        }
        GameStateManager.instance.Change(player_list[flag]);
    }
}
