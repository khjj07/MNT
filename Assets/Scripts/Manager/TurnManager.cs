using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : Singleton<TurnManager>
{
    public List<GameState> player_list = new List<GameState>();
    public Player current_player;
    public int TurnLimit = 4;
    private int flag = 0;
    public int RemainTurn;
    public UnityEvent DefeatEvent;
    public UnityEvent EnemyAllDeadEvent;
    public UnityEvent VictoryEvent;
    public UnityEvent TurnChangeEvent;

    void Start()
    {
        RemainTurn = TurnLimit;
        UpdatePlayer();
    }
    public void HostageDead()
    {
        DefeatEvent.Invoke();
    }

    public void FixPlayerList(int i)
    {
       if( i == player_list.Count-1)
       {
            if (i != 0 && player_list[i] == player_list[0])
            {
                player_list.Remove(player_list[i]);
            }
            return;
        }
        while (player_list[i] == player_list[i+1] && i<player_list.Count)
        {
            player_list.Remove(player_list[i+1]);
        }
        if (i < player_list.Count)
        {
            FixPlayerList(i + 1);
        }
        


    }
    public void UnitDead(GameState deadunit)
    {
        int count = 0;
        for (int i = 0; i < player_list.Count; i++)
        {
            if (player_list[i] == deadunit)
            {
                count++;
            }   
        }
        for(int i=0;i<count;i++)
        {
            player_list.Remove(deadunit);
        }

        FixPlayerList(0);


        if (player_list.Count==1)
        {
            EnemyAllDeadEvent.Invoke();
        }
    }
    public void UpdatePlayer()
    {
        if (player_list.Count <= flag)
        {
            flag = 0;
        }
        RemainTurn--;
        GameStateManager.instance.Change(player_list[flag]);
        current_player = player_list[flag].GetComponent<Player>();
        TurnChangeEvent.Invoke();
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
            DefeatEvent.Invoke();
    }
}
