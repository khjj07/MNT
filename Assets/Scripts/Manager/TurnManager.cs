using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : Singleton<TurnManager>
{
    public GameState[] player_list;
    public int TurnLimit = 4;
    private int flag = 0;
    public int RemainTurn;
    private int player_count;
    public UnityEvent DefeatEvent;
    public UnityEvent EnemyAllDeadEvent;
    public UnityEvent VictoryEvent;

    void Start()
    {
        RemainTurn = TurnLimit;
        player_count = player_list.Length;
    }
    public void HostageDead()
    {
        DefeatEvent.Invoke();
    }
    public void UnitDead()
    {
        player_count--;
        if (player_count == 1)
        {
            EnemyAllDeadEvent.Invoke();
        }
    }
  
    public void NextTurn()
    {
        flag++;
        RemainTurn--;
        if (player_list.Length<=flag)
        {
            flag = 0;
        }
        if (RemainTurn >= 0)
            GameStateManager.instance.Change(player_list[flag]);
        else
            DefeatEvent.Invoke();
    }
}
