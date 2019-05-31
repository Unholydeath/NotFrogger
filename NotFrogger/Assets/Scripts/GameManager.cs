using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] float m_goalBase = 1f;
	[SerializeField] float m_deathBase = 1f;

	static float m_score = 0.0f;
	static float m_time = 0.0f;
	static GameManager m_selfRef = null;

	float m_lap = 0.0f;

	public static int Score { get { return (int)m_score; } }
	public static int GameTime { get { return (int)m_time; } }
	public static bool ManagerExists { get { return m_selfRef != null; } }

    public Text GameScore;
    public Text gTime;

	private void Start()
	{
		m_selfRef = this;

		Interactable.OnGoalCollision += Goal;

        SetScore();
	}

    private void SetScore()
    {
        GameScore.text = Score.ToString();
    }

	private void OnDestroy()
	{
		Interactable.OnGoalCollision -= Goal;
	}

	private void FixedUpdate()
	{
		m_time += Time.deltaTime;
        gTime.text = "" + m_time.ToString();
	}

	void Goal()
	{
		m_score += m_goalBase * (m_lap /  GameTime);
		m_lap = GameTime;
	}

	void Death()
	{
		m_score -= m_deathBase;
	}

	public static void DeathAlert()
	{
		DeathAlert();
	}

	public static void GameOver()
	{
		Debug.Log("GG");
	}
}
