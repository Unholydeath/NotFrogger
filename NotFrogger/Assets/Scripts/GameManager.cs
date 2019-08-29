using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] float m_transitionTime = 3.0f;
	[SerializeField] float m_leaderBoardTransitionTime = 5.0f;
    [SerializeField] GameObject m_winPanel;
	[SerializeField] GameObject m_losePanel;
	[SerializeField] GameObject m_hud;
    [SerializeField] GameObject m_leaderboard;
    [SerializeField] GameObject m_createHiScore;
    [SerializeField] GameObject m_hiScoreList;
    [SerializeField] GameObject[] m_hiScoreInitials;
    [SerializeField] Text m_hiScoreSlot1;
    [SerializeField] Text m_hiScoreSlot2;
    [SerializeField] Text m_hiScoreSlot3;
    [SerializeField] Text m_hiScoreSlot4;
    [SerializeField] Text m_hiScoreSlot5;

    [SerializeField] int m_goalsToWin = 5;
	[SerializeField] float m_goalBase = 1f;
	[SerializeField] float m_deathBase = 1f;

	static float m_score = 0.0f;
	static float m_time = 0.0f;
	static GameManager m_selfRef = null;

	float m_lap = 0.0f;
	int m_goals = 0;
	bool m_inTransition = false;
    bool m_playingGame = true;
    int m_hiScore1;
    int m_hiScore2;
    int m_hiScore3;
    int m_hiScore4;
    int m_hiScore5;
    string m_player1Name;
    string m_player2Name;
    string m_player3Name;
    string m_player4Name;
    string m_player5Name;
    string m_hiScoreTemplate1 = ":          ";
    string m_hiScoreTemplate2 = "          ";
    int activeInitialIndex = 0;

    public static int Score { get { return (int)m_score; } }
	public static int GameTime { get { return (int)m_time; } }
	public static bool ManagerExists { get { return m_selfRef != null; } }

	public Text GameScore;
	public Text GTime;

	static bool m_lost = false;

	public delegate void GameAction();
	public static GameAction OnPause;

    private void Start()
	{
		Debug.Log("Start");
		m_selfRef = this;

		Interactable.OnGoalCollision += Goal;

		GameScore.text = "" + Score.ToString();

		m_losePanel.SetActive(false);
		m_winPanel.SetActive(false);
		m_hud.SetActive(true);
        m_leaderboard.SetActive(false);
        m_createHiScore.SetActive(false);
        m_hiScoreList.SetActive(false);

		m_lost = false;
		m_lap = 0.0f;
		m_inTransition = false;
		m_score = 0.0f;
		m_time = 0.0f;
		m_goals = 0;

        SetupHiScores();
    }

	private void OnDestroy()
	{
		Interactable.OnGoalCollision -= Goal;
	}

	private void FixedUpdate()
	{
        if (m_playingGame)
        {
            if (!m_lost && m_goalsToWin > 0)
            {
                m_time += Time.deltaTime;
                GTime.text = "" + GameTime.ToString();
            }
            else
            {
                if (m_goals == m_goalsToWin) m_winPanel.SetActive(true);
                else if (m_lost) m_losePanel.SetActive(true);
                m_playingGame = false;
            }
        }
        else
        {
            if (!m_inTransition)
            {
                StartCoroutine("ToEndGameScreen");
            }
            else
            {

            }
        }

        
    }

    void SetupHiScores()
    {
        m_hiScore1 = PlayerPrefs.GetInt("hiScore1", 30);
        m_hiScore2 = PlayerPrefs.GetInt("hiScore2", 25);
        m_hiScore3 = PlayerPrefs.GetInt("hiScore3", 20);
        m_hiScore4 = PlayerPrefs.GetInt("hiScore4", 15);
        m_hiScore5 = PlayerPrefs.GetInt("hiScore5", 10);
        m_player1Name = PlayerPrefs.GetString("player1Name", "AAA");
        m_player2Name = PlayerPrefs.GetString("player2Name", "AAA");
        m_player3Name = PlayerPrefs.GetString("player3Name", "AAA");
        m_player4Name = PlayerPrefs.GetString("player4Name", "AAA");
        m_player5Name = PlayerPrefs.GetString("player5Name", "AAA");

        m_hiScoreSlot1.text = "1" + m_hiScoreTemplate1 + m_player1Name + m_hiScoreTemplate2 + m_hiScore1;
        m_hiScoreSlot2.text = "2" + m_hiScoreTemplate1 + m_player2Name + m_hiScoreTemplate2 + m_hiScore2;
        m_hiScoreSlot3.text = "3" + m_hiScoreTemplate1 + m_player3Name + m_hiScoreTemplate2 + m_hiScore3;
        m_hiScoreSlot4.text = "4" + m_hiScoreTemplate1 + m_player4Name + m_hiScoreTemplate2 + m_hiScore4;
        m_hiScoreSlot5.text = "5" + m_hiScoreTemplate1 + m_player5Name + m_hiScoreTemplate2 + m_hiScore5;
    }

	public void PauseGame()
	{
		if (OnPause != null) OnPause();
	}

	void Goal()
	{
		m_score += m_goalBase + (m_lap /  GameTime);
		GameScore.text = "" + Score.ToString();

		m_lap = GameTime;
		++m_goals;
	}

	void Death()
	{
		m_score -= m_deathBase;
		GameScore.text = "" + Score.ToString();
	}

	public static void DeathAlert()
	{
		DeathAlert();
	}

	public static void GameOver()
	{
		Debug.Log("Lost");
		m_lost = true;
	}

    private void SetupLeaderboard()
    {
        m_losePanel.SetActive(false);
        m_winPanel.SetActive(false);
        m_hud.SetActive(false);
        m_leaderboard.SetActive(true);
    }

    private void ToCreateHiScore()
    {
        SetupLeaderboard();
        m_createHiScore.SetActive(true);
        m_hiScoreList.SetActive(false);
    }

    IEnumerator ToLeaderboard()
    {
        SetupLeaderboard();
        m_createHiScore.SetActive(false);
        m_hiScoreList.SetActive(true);
        yield return new WaitForSeconds(m_transitionTime);
        SceneManager.LoadScene("Start");
    }

	IEnumerator ToEndGameScreen()
	{
		m_hud.SetActive(false);
		m_inTransition = true;
		yield return new WaitForSeconds(m_transitionTime);

        if(m_score > m_hiScore5)
        {
            ToCreateHiScore();
        }
        else
        {
            StartCoroutine("ToLeaderboard");
        }
	}
}
