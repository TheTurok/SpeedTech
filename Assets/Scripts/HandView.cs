using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hand))]
public class HandView : MonoBehaviour {
    Hand hand;
    Dictionary<int, GameObject> fetchedCards;
    public Vector3 start;
    int lastCount;

    public GameObject cardPrefab;
    public bool rev;

    void Start()
    {
        fetchedCards = new Dictionary<int, GameObject>();
        hand = GetComponent<Hand>();

        lastCount = hand.getHandCount();

        hand.CardRemoved += hand_CardRemoved;
    }

    private void Update()
    {
        if (lastCount != hand.getHandCount())
        {
            lastCount = hand.getHandCount();
            ShowCards();
        }
    }

    private void hand_CardRemoved(object sender, CRDEventArgs e)
    {
        if (fetchedCards.ContainsKey(e.CardIndex))   //if this card contains this key
        {
            Destroy(fetchedCards[e.CardIndex]); //destory gameobject
            fetchedCards.Remove(e.CardIndex); //dont' ever ref again
        }
    }

    void ShowCards()
    {
        Vector3 temp;
        int layout;
        for (int i = 0; i < 5; i++) {
            layout = i;
            if (rev == true)
                layout = layout * -1;
            temp = start + new Vector3(layout, 0f);
            AddCard(temp, hand.getCard(i));
        }

    }

    void AddCard(Vector3 position, int cardIndex)
    {
        if (cardIndex == -2)
            return;

        if (fetchedCards.ContainsKey(cardIndex)){
            return;
        }

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;
        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        cardModel.ToggleFace(true);

        fetchedCards.Add(cardIndex, cardCopy);
    }
}
