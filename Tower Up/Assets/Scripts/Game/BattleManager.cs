using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    [SerializeField] private Card[] attackCard;
    [SerializeField] private Card[] defenceCard;
    private int card1;
    private int card2;
    private int card3;
    private int card4;

    private void Start()
    {
       // CardCombine();
    }
    public void AttackCardCombine()
    {
        if(cards != null)
        {
            for(int i = 0; i<cards.Count; i++)
            {
                cards.Remove(cards[i]);
                card1 = 0;
                card2 = 0;
                card3 = 0;
                card4 = 0;
            }
        }


        for(int i = 0; i<100; i++)
        {
            int a = Random.Range(0,4);
            if(a == 0)
            {
                if(card1 < 40)
                {
                    card1++;
                    cards.Add(attackCard[0]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(attackCard[1]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(attackCard[2]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(attackCard[3]);
                    }
                }
            }

            if(a == 1)
            {
                if(card2 < 40)
                {
                    card2++;
                    cards.Add(attackCard[1]);
                }
                else
                {
                    if(card1 < 40)
                    {
                        card1++;
                        cards.Add(attackCard[0]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(attackCard[2]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(attackCard[3]);
                    }
                }
            }

            if(a == 2)
            {
                if(card3 < 10)
                {
                    card3++;
                    cards.Add(attackCard[2]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(attackCard[1]);
                    }
                    else if(card1 < 40)
                    {
                        card1++;
                        cards.Add(attackCard[0]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(attackCard[3]);
                    }
                }
            }

            if(a == 3)
            {
                if(card4 < 10)
                {
                    card4++;
                    cards.Add(attackCard[3]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(attackCard[1]);
                    }
                    else if(card1 < 40)
                    {
                        card1++;
                        cards.Add(attackCard[0]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(attackCard[2]);
                    }
                }
            }

        }
    }

    public void DefenceCardCombine()
    {
        if(cards != null)
        {
            for(int i = 0; i<cards.Count; i++)
            {
                cards.Remove(cards[i]);
                card1 = 0;
                card2 = 0;
                card3 = 0;
                card4 = 0;
            }
        }


        for(int i = 0; i<100; i++)
        {
            int a = Random.Range(0,4);
            if(a == 0)
            {
                if(card1 < 40)
                {
                    card1++;
                    cards.Add(defenceCard[0]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(defenceCard[1]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(defenceCard[2]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(defenceCard[3]);
                    }
                }
            }

            if(a == 1)
            {
                if(card2 < 40)
                {
                    card2++;
                    cards.Add(defenceCard[1]);
                }
                else
                {
                    if(card1 < 40)
                    {
                        card1++;
                        cards.Add(defenceCard[0]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(defenceCard[2]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(defenceCard[3]);
                    }
                }
            }

            if(a == 2)
            {
                if(card3 < 10)
                {
                    card3++;
                    cards.Add(defenceCard[2]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(defenceCard[1]);
                    }
                    else if(card1 < 40)
                    {
                        card1++;
                        cards.Add(defenceCard[0]);
                    }
                    else if(card4 < 10)
                    {
                        card4++;
                        cards.Add(defenceCard[3]);
                    }
                }
            }

            if(a == 3)
            {
                if(card4 < 10)
                {
                    card4++;
                    cards.Add(defenceCard[3]);
                }
                else
                {
                    if(card2 < 40)
                    {
                        card2++;
                        cards.Add(defenceCard[1]);
                    }
                    else if(card1 < 40)
                    {
                        card1++;
                        cards.Add(defenceCard[0]);
                    }
                    else if(card3 < 10)
                    {
                        card3++;
                        cards.Add(defenceCard[2]);
                    }
                }
            }

        }
    }


}
