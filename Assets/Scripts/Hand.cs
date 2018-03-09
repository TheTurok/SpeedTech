using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    int[] cards = new int[5];
    int totalCards = 0;

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            cards[i] = -2;
        }
    }

    public event CRDEventHandler CardRemoved;

    public int PopAt(int i) //pop index at given and return
    {
        int temp = cards[i];
        cards[i] = -2; 

        if (CardRemoved != null)  //activate update
        {
            CardRemoved(this, new CRDEventArgs(temp));
        }
        totalCards--;
        return temp;
    }

    public int getCard(int i)
    { 
        return cards[i];
    }

    public void Push(int i)  //TODO: Figure out if i want it fill up from left to right or it fills up from emptiness
    {
        int x = 0;
        while(x < 5) //go through hand
        {
            if (cards[x] == -2) //if one is empty
            {
                cards[x] = i; //replace card
                totalCards++;
                break;
            }
            x++;
        }
    }

    public int getHandCount()  //total hand count
    {
        return totalCards;
    }


	
}
