using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {
    List<int> cards;

    public bool isGameDeck;


    public bool HasCards  // if it has cards
    {
        get { return cards != null && cards.Count > 0; }
    }

    public event CRDEventHandler CardRemoved;


    public int cardCount  // the count of cards
    {
        get {
            if (cards == null) {
                return 0;

            }
            else {
                return cards.Count;
            }
        }
    }

    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    public int Pop() //take our the card on top of the deck
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        if (CardRemoved != null) {
            CardRemoved(this, new CRDEventArgs(temp));
        }

        return temp;
    }

    public int PeekFace() //get Value of card
    {
        if (cards.Count == 0)
        {
            return -2;
        }

        return cards[cards.Count - 1];
    }

    public int GetCard(int i)
    {
        return cards[i];
    }

    public int PopAt(int i)
    {
        int temp = cards[i];
        cards.RemoveAt(i);

        if (CardRemoved != null)
        {
            CardRemoved(this, new CRDEventArgs(temp));
        }

        return temp;
    }


    public void Push(int card) //ad the card to the bottom of the deck
    {
        cards.Add(card);
    }


    public void CreateDeck()
    {

        for(int i = 0; i < 52;  i++) //created unshuffled card deck
        {
            cards.Add(i);
        }

        int n = cards.Count;
        int randomNum, temp;

        while (n > 1) //Random Shuffle
        {
            n--;
            randomNum = Random.Range(0, n + 1);
            temp = cards[randomNum];
            cards[randomNum] = cards[n];
            cards[n] = temp;
        }
          
    }

	void Start () {
        cards = new List<int>();
        if (isGameDeck)
        {
            CreateDeck();  //create a shuffled deck
        }


	}
	
}
