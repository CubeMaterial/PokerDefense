using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDeckMaster : Singleton<SubDeckMaster>
{

    private bool IsDrawBan;
    private List<Card> mHandList;

    private List<Card> mSelectCardList;
    private int iHandSum;

    private int iGrade;
    private int iLevel;
    private int iSubLevel;
    private void Start()
    {
        mHandList = new List<Card>();
        mSelectCardList = new List<Card>();

        DeckMaster.instance.NewSubDeck();

        //Card card0 = new Card();
        //Card card1 = new Card();
        //Card card2 = new Card();
        //Card card3 = new Card();
        //Card card4 = new Card();
        //Card card5 = new Card();
        //Card card6 = new Card();
        //card0.Init(18);
        //card1.Init(11);
        //card2.Init(12);
        //card3.Init(13);
        //card4.Init(14);
        //card5.Init(15);
        //card6.Init(16);

        //mHandList.Add(card0);
        //mHandList.Add(card1);
        //mHandList.Add(card2);
        //mHandList.Add(card3);
        //mHandList.Add(card4);
        //mHandList.Add(card5);
        //mHandList.Add(card6);

        for (int i = 0; i < 7; i++)
        {
            mHandList.Add(DeckMaster.instance.DrawSubDeck());
        }
    }

    public void GetCard()
    {
        if (mHandList.Count > 7)
        {
            IsDrawBan = true;
        }
        else
        {
            IsDrawBan = false;
            if (GameManager.instance.ReturnCost(DeckType.Sub))
            {
                mHandList.Add(DeckMaster.instance.DrawSubDeck());
            }
            else
            {
                print("not enought gold");
            }
        }
    }



    public int ReturnSelectGrade()
    {
        return iGrade;
    }
    public int ReturnSelectLevel()
    {
        return iLevel;
    }
    public int ReturnSelectSubLevel()
    {
        return iSubLevel;
    }

    private void UpdateUI()
    { 
        
    }


    public void Extract()
    { 
    
    }



    public void SelectCard(Card mCard, bool hasSelected)
    { 
        if(hasSelected)
        {
            mSelectCardList.Add(mCard);
        }
        else
        {
            for(int i = 0; i < mSelectCardList.Count; i++)
            { 
                if(mSelectCardList[i] == mCard)
                {
                    mSelectCardList.RemoveAt(i);
                    break;
                }
            }
        }

        iGrade = CheckHand();
    }


    public void Apply()
    {
        iGrade = CheckHand();

        //mSelectCardList.Clear();

    }

    private int CheckHand()
    {
        int flush = 0, straight = 0, fourCard = 0, triple = 0, onePair = 0, twoPair = 0, fullHouse = 0, top = 0, grade = 0, straightChk = 0;
        int[] shape = new int[4];
        for (int i = 0; i < 4; i++)
        {
            shape[i] = 0;
        }

        int[] number = new int[13];
        for (int i = 0; i < 13; i++)
        {
            number[i] = 0;
        }


        for (int q = 0; q < mSelectCardList.Count; q++)
        {
            switch (mSelectCardList[q].ReturnCardShape())
            {
                case CardShape.Heart:
                    shape[0]++;
                    break;
                case CardShape.Diamond:
                    shape[1]++;
                    break;
                case CardShape.Clover:
                    shape[2]++;
                    break;
                case CardShape.Spade:
                    shape[3]++;
                    break;
            }

            if (shape[0] >= 5) flush = 1;
            else if (shape[1] >= 5) flush = 2;
            else if (shape[2] >= 5) flush = 3;
            else if (shape[3] >= 5) flush = 4;

            if (flush >= 1 && grade <= 6)
            {
                grade = 6;
            }
        }// flush check

        for (int q = 0; q < mSelectCardList.Count; q++)
        {
            number[(int)mSelectCardList[q].ReturnCardLevel()]++;
        }//number 배열에 카드의 숫자값을 빼서 넣어줌

        for (int q = 0; q < 9; q++)
        {
            for (int w = q; w < q + 5; w++)
            {
                if (number[w] >= 1)
                {
                    straightChk++;
                }
            }//작은 for
            if (straightChk >= 5)
            {
                straight = q + 6;

                if (grade <= 5)
                {
                    grade = 5;
                }
            }
            straightChk = 0;
        }//straight check

        for (int q = 0; q < 13; q++)
        {
            if (number[q] == 4)
            {
                fourCard = q + 2;

                if (grade <= 8)
                    grade = 8;
            }
            else if (triple >= 1 && onePair >= 1)
            {
                fullHouse = triple;
                if (grade <= 7)
                    grade = 7;
            }
            else if (number[q] == 3)
            {
                triple = q + 2;
                if (grade <= 4)
                    grade = 4;
            }
            else if (onePair >= 1 && number[q] == 2 && twoPair == 0)
            {
                twoPair = q + 2;
                if (grade <= 3)
                    grade = 3;
            }
            else if (onePair >= 1 && number[q] == 2 && twoPair >= 1)
            {
                onePair = twoPair;
                twoPair = q + 2;
                if (grade <= 3)
                    grade = 3;
            }// 3페어 방지
            else if (number[q] == 2 && grade != 3)
            { // 주의!!!!!!! 이 부분이 아주 중요함 && grade < 5
                onePair = q + 2;
                if (grade <= 2)
                    grade = 2;
            }//투페어가 당첨되면 원페어는 그대로 있기위한 조건
            else if (number[q] == 1)
            {
                top = q + 2;
                if (grade <= 1)
                    grade = 1;
            }
        }//원,투페어,트리플,풀하우스,포카드 확인


        switch (grade)
        {
            case 1:
                iLevel = gradeResult(top);
                iSubLevel = -1;
                //print("<Top> " + gradeResult(top));
                break;
            case 2:
                iLevel = gradeResult(onePair);
                iSubLevel = -1;
                //print("<One Pair> " + gradeResult(onePair));
                break;
            case 3:
                iLevel = gradeResult(twoPair);
                iSubLevel = gradeResult(onePair);
                //print("<Two Pair> " + gradeResult(twoPair) + " / " + gradeResult(onePair));
                break;
            case 4:
                iLevel = gradeResult(triple);
                iSubLevel = -1;
                //print("<Triple> " + gradeResult(triple));
                break;
            case 5:
                iLevel = gradeResult(straight);
                iSubLevel = -1;
                //print("<Straight> " + gradeResult(straight));
                break;
            case 6:
                iLevel = flush - 1;
                iSubLevel = -1;
                //print("<Flush> " + (CardShape)(flush - 1));
                break;
            case 7:
                iLevel = gradeResult(fullHouse);
                iSubLevel = gradeResult(onePair);
                //print("<FullHouse> " + gradeResult(fullHouse) + " / " + gradeResult(onePair));
                break;
            case 8:
                iLevel = gradeResult(fourCard);
                iSubLevel = -1;
                //print("<FourCard> " + gradeResult(fourCard));
                break;
        }
        return grade;

    }



    private int gradeResult(int grade)
    {
        int tmp = 0;
        //print("grade : " + (grade-2));

        tmp = (grade-2);
        //for (int q = 0; q < mHandList.Count; q++)
        //{
        //    if (mHandList[q].ReturnCardLevel().Equals("" + grade))
        //    {
        //        tmp += mHandList[q].ReturnCardShape() + " ";
        //    }
        //}
        return tmp;
    } //Grade Result
}
