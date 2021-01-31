using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int dayOneMoneyGoal = 10;
    [SerializeField] int dayTwoMoneyGoal = 20;
    [SerializeField] int dayThreeMoneyGoal = 30;
    [SerializeField] int dayFourMoneyGoal = 40;
    [SerializeField] int dayFiveMoneyGoal = 50;

    [SerializeField]
    Text moneyTextOnUI;

    public static int moneyAmount;
    public static int currentMoneyGoal;
    bool passedDay;
    // Start is called before the first frame update
    void Start()
    {
        currentMoneyGoal = dayOneMoneyGoal;
        moneyAmount = 0;

        moneyTextOnUI.text = moneyAmount + " / " +  currentMoneyGoal;
    }

    void Update()
    {
        moneyTextOnUI.text = moneyAmount + " / " + currentMoneyGoal;
    }

    static public void changeMoneyAmount(int moneyToAdd)
    {
        moneyAmount += moneyToAdd;
    }

    void checkIfMoneyGoalIsMet(int daysPassed)
    {
        switch (daysPassed)
        {
            default:
            case 1:
                passedDay = moneyAmount >= dayOneMoneyGoal ? true : false;
                break;
            case 2:
                passedDay = moneyAmount >= dayTwoMoneyGoal ? true : false;
                break;
            case 3:
                passedDay = moneyAmount >= dayThreeMoneyGoal ? true : false;
                break;
            case 4:
                passedDay = moneyAmount >= dayFourMoneyGoal ? true : false;
                break;
            case 5:
                passedDay = moneyAmount >= dayFiveMoneyGoal ? true : false;
                break;
        }

        // If the player did not pass the day
        if (!passedDay) Application.Quit();
    }
}
