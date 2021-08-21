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
    public void UnitDead(GameState deadunit)
    {
        player_list.Remove(deadunit);
        if(player_list.Count==1)
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
        GameStateManager.instance.Change(player_list[flag]);
        current_player = player_list[flag].GetComponent<Player>();
        TurnChangeEvent.Invoke();
    }

     public void NextTurn()
    {
        flag++;
        RemainTurn--;
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
