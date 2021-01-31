using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int dayOneMoneyGoal = 20;
    [SerializeField] int dayTwoMoneyGoal = 50;
    [SerializeField] int dayThreeMoneyGoal = 80;
    [SerializeField] int dayFourMoneyGoal = 100;
    [SerializeField] int dayFiveMoneyGoal = 200;

    [SerializeField]
    Text moneyTextOnUI;

    public static int moneyAmount;
    public static int currentMoneyGoal;
    bool passedDay;

    GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentMoneyGoal = dayOneMoneyGoal;
        moneyAmount = 0;

        moneyTextOnUI.text = moneyAmount + " / " +  currentMoneyGoal;

        gameOverPanel = GameObject.Find( "GameOverPanel" );
        gameOverPanel.transform.Find( "Retry" ).GetComponent<Button>().onClick.AddListener( delegate { SceneManager.LoadScene( SceneManager.GetActiveScene().name ); } );
        gameOverPanel.transform.Find( "Retry" ).GetComponent<Button>().onClick.AddListener( delegate { Application.Quit(); } );
        gameOverPanel.SetActive( false );
    }

    void Update()
    {
        moneyTextOnUI.text = moneyAmount + " / " + currentMoneyGoal;
    }

    static public void changeMoneyAmount(int moneyToAdd)
    {
        moneyAmount += moneyToAdd;
    }

    public void checkIfMoneyGoalIsMet(int daysPassed)
    {
        --daysPassed;
        switch (daysPassed)
        {
            default:
                passedDay = true;
                break;
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
        passedDay = true;

        // If the player did not pass the day
        // if (!passedDay) Application.Quit();
        if( !passedDay )
        {
            gameOverPanel.SetActive( true );
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            TimePassage.DAY = 1;
            GetComponent<SFXPlayer>().PlaySFX( "give liar item" );
        }
        else if( daysPassed >= 5 )
		{
            gameOverPanel.SetActive( true );
            gameOverPanel.transform.Find( "Text" ).GetComponent<Text>().text = "YOU WIN";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            TimePassage.DAY = 1;
            GetComponent<SFXPlayer>().PlaySFX( "give customer right item" );
        }
    }
}
