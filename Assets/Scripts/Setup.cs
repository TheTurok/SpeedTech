using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour {

    public CardStack dealer, leftDraw, rightDraw, leftPlay, rightPlay, player1, player2;
    public Hand p1Hand, p2Hand;
    public GamePlay p1g, p2g;

    public GameObject card;
    CardModel cm;
    CardFlipper flip;
    

    void Awake()
    {
        cm = card.GetComponent<CardModel>();
        flip = card.GetComponent<CardFlipper>();
    }

    // Update is called once per frame
    void Update() {
        if (dealer.cardCount == 52) //deal out the cards
        {
            for (int i = 0; i < 5; i++)
            {
                leftDraw.Push(dealer.Pop());
                rightDraw.Push(dealer.Pop());
                p1Hand.Push(dealer.Pop());
                p2Hand.Push(dealer.Pop());
            }
            for (int i = 0; i < 15; i++)
            {
                player1.Push(dealer.Pop());
                player2.Push(dealer.Pop());
            }
            leftPlay.Push(dealer.Pop());
            rightPlay.Push(dealer.Pop());
        }

        if(p1g.IsReady() && p2g.IsReady()){
            p1g.Unready();
            p2g.Unready();
            Restack();
        }


        //Card Flip From Both Sides Method
        if ( !Check(p1Hand) && !Check(p2Hand) ) //Checks Every Card 
        {
            if (p1Hand.getHandCount() < 5 && player1.cardCount > 0)
            {
            }
            else if (p2Hand.getHandCount() < 5 && player2.cardCount > 0)
            {
            }
            else
            {
                Restack();
            }
        }
    }
    

    private bool Check(Hand theHand)  //Checks if any cards in their current hand is playable
    {
        bool works = false;
        int card;
        int cardRank;
        int pileRank;

        for (int i = 0; i < 5; i++)
        {

            card = theHand.getCard(i);
            if(card == -2)
            {
                continue;
            }

            cardRank = card % 13;

            pileRank = leftPlay.PeekFace() % 13;

            if (cardRank + 1 == pileRank || cardRank - 1 == pileRank ||
                cardRank == pileRank + 12 || cardRank == pileRank - 12)
            {
                return true;
            }
            pileRank = rightPlay.PeekFace() % 13;

            if (cardRank + 1 == pileRank || cardRank - 1 == pileRank ||
                cardRank == pileRank + 12 || cardRank == pileRank - 12)
            {
                return true;
            }
        }
        return works;
    }

    private void Refill(CardStack play, CardStack draw)
    {
        while(play.cardCount > 0)
        {
            draw.Push(play.Pop());
        }
    }

    private void Restack()
    {
        if (leftDraw.cardCount == 0)
        {
            Refill(leftPlay, leftDraw);
        }
        if (rightDraw.cardCount == 0)
        {
            Refill(rightPlay, rightDraw);
        }

        int left = leftDraw.Pop();
        int right = rightDraw.Pop();

        flip.FlipCard(cm.cardBack, cm.faces[left], left);  // fix card won't flip TODO: ***/
        flip.FlipCard(cm.cardBack, cm.faces[right], right);

        leftPlay.Push(left);
        rightPlay.Push(right);
    }
}
