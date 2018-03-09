using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public CardStack player, leftPlay, rightPlay;
    public Hand p1Hand;
    bool ready = false;


    private void Update()  //every Action
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ready = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow) )
        {
            cardCheck(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftArrow))
        {
            cardCheck(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.D )&& Input.GetKey(KeyCode.LeftArrow))
        {
            cardCheck(2, 0);
        }
        if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftArrow))
        {
            cardCheck(3, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
        {
            cardCheck(4, 0);
        }
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
        {
            cardCheck(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.RightArrow))
        {
            cardCheck(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.RightArrow))
        {
            cardCheck(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.RightArrow))
        {
            cardCheck(3, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        {
            cardCheck(4, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (p1Hand.getHandCount() < 5 && player.cardCount >0)
            {
                int card = player.Pop();
                p1Hand.Push(card);
            }
        }

    }


    public void cardCheck(int i, int side) 
    {
        CardStack pile;
        if(side == 0)
            { pile = leftPlay; }
        else
            { pile = rightPlay; }

        if (p1Hand.getCard(i) == -2)
            return;

        
        int card = p1Hand.getCard(i);
        int cardRank = card % 13;
        int pileRank = pile.PeekFace() % 13;

        if (cardRank + 1 == pileRank || cardRank - 1 == pileRank || 
            cardRank == pileRank + 12 || cardRank == pileRank - 12)
        {
            p1Hand.PopAt(i);
            pile.Push(card);
        }
    }

    public bool IsReady()
    {
        return ready;
    }

    public void Unready()
    {
        ready = false;
    }



}
