using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : Singleton<TurnManager>
{
    public List<GameState> player_list = new List<GameState>();
    public int TurnLimit = 4;
    private int flag = 0;
    public int RemainTurn;
    public UnityEvent DefeatEvent;
    public UnityEvent EnemyAllDeadEvent;
    public UnityEvent VictoryEvent;

    void Start()
    {
        RemainTurn = TurnLimit;
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
        GameStateManager.instance.Change(player_list[flag]);
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
            GameStateManager.instance.Change(player_list[flag]);
        else
            DefeatEvent.Invoke();
    }
}
