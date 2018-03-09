using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour {
    CardStack deck;
    Dictionary<int, GameObject> fetchedCards;
    int lastCount;

    public Vector3 start;
    public GameObject cardPrefab;
    public bool faceUp = false; //if true keep card face down
    public bool reverseLayerOrder = false;
    public float cardOffset;


    void Start()
    {
        fetchedCards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardStack>();
        ShowCards();
        lastCount = deck.cardCount;

        deck.CardRemoved += deck_CardRemoved;   //added event handler
    }

    private void deck_CardRemoved(object sender, CRDEventArgs e)
    {
        if(fetchedCards.ContainsKey(e.CardIndex))   //if this card contains this key
        {
            Destroy(fetchedCards[e.CardIndex]); //destory gameobject
            fetchedCards.Remove(e.CardIndex); //dont' ever ref again
        }
    }

    private void Update()
    {
        if(lastCount != deck.cardCount)
        {
            lastCount = deck.cardCount;
            ShowCards();
        }
        
    }

    void ShowCards()
    {
        int cardCount = 0;

        if (deck.HasCards) {
            foreach (int i in deck.GetCards()){
                float co = cardOffset * cardCount;
                Vector3 temp;

                if (faceUp == false){
                    temp = start + new Vector3(-co, -co );
                }
                else { 
                     temp = start + new Vector3(co, 0f);
                }
                AddCard(temp, i,cardCount);


                cardCount++;
            }
        }
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex)
    {
        if (fetchedCards.ContainsKey(cardIndex)){
            return;
        }

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;
        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        cardModel.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (reverseLayerOrder){
            spriteRenderer.sortingOrder = 51 - positionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = positionalIndex;
        }

        fetchedCards.Add(cardIndex, cardCopy);
    }

}
