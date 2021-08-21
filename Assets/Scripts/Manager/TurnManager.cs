using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : Singleton<TurnManager>
{
    public List<Player> player_list = new List<Player>();
    public List<Player> nonplayer_list = new List<Player>();//friend 제외
    public Player current_player;
    public int TurnLimit = 4;
    private int flag = 0;
    public int RemainTurn;
    public UnityEvent DefeatEvent;
    public UnityEvent EnemyAllDeadEvent;
    public UnityEvent VictoryEvent;
    public UnityEvent TurnChangeEvent;

    public void GameStart()
    {
        RemainTurn = TurnLimit;
        SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_ingame);
        UpdatePlayer();
    }
    public void Defeat()
    {
        Debug.Log("defeat");
        DefeatEvent.Invoke();
    }

    public void FixPlayerList(int i)
    {
        if (i < player_list.Count - 1)
        {
            while (player_list[i] == player_list[i + 1] && i + 1 < player_list.Count)
            {
                player_list.RemoveAt(i + 1);
                if (i == player_list.Count - 1)
                {
                    if (i > 0 && player_list[i] == player_list[0])
                    {
                        player_list.RemoveAt(i);
                    }
                    return;
                }
            }
            if (i < player_list.Count - 1)
            {
                FixPlayerList(i + 1);
            }
        }
    }
    
    public void UnitDead(Player deadunit)
    {
        int count = 0;
        for (int i = 0; i < player_list.Count; i++)
        {
            if (player_list[i] == deadunit)
            {
                count++;
            }   
        }
        if(count>0)
        {
            for (int i = 0; i < count; i++)
            {
                player_list.Remove(deadunit);
            }
            FixPlayerList(0);
        }
        else
        {
            if(nonplayer_list.Contains(deadunit))
            {
                nonplayer_list.Remove(deadunit);
            }
        }
       


        if (player_list.Count==1 && nonplayer_list.Count==0)
        {
            EnemyAllDeadEvent.Invoke();
            Victory();
            //바뀔수도있음
        }
    }
    public void UpdatePlayer()
    {
        if (player_list.Count <= flag)
        {
            flag = 0;
        }
        RemainTurn--;
        if (current_player == null || player_list[flag] != current_player)
        {
            GameStateManager.instance.Change(player_list[flag].GetComponent<GameState>());
            current_player = player_list[flag].GetComponent<Player>();
            TurnChangeEvent.Invoke();
        }
    }

     public void NextTurn()
    {
        flag++;
        if (player_list.Count<=flag)
        {
            flag = 0;
        }
        if (RemainTurn >= 0)
        {
            UpdatePlayer();
        }
        else
            Defeat();
    }
    public void Victory()
    {
        Debug.Log("Victory");
        VictoryEvent.Invoke();
    }
}
