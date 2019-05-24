using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] float m_goalBase = 10.0f;
	[SerializeField] float m_deathBase = 5.0f;

	[SerializeField] Interactable[] m_goals;

	static float m_score = 0.0f;
	static float m_time = 0.0f;
	float m_lap = 0.0f;

	static GameManager s_thisRef;

	public static float Score { get { return m_score; } }
	public static float Timer { get { return m_time; } }
	public static bool ManagerExists { get { return s_thisRef != null; } }

    public Text CurrentScore;
    public Text TimeLeft;

	void Start()
    {
		Interactable.OnDeathCollision += DeathReaction;
		Interactable.OnGoalCollision += GoalReaction;

		s_thisRef = this;

        SetScore();
        SetTime();
	}

    public void SetScore()
    {
        CurrentScore.text = Score.ToString();
    }

    public void SetTime()
    {
        TimeLeft.text = Timer.ToString();
    }

	private void OnDestroy()
	{
		Interactable.OnDeathCollision -= DeathReaction;
		Interactable.OnGoalCollision -= GoalReaction;
	}

	private void FixedUpdate()
	{
		m_time += Time.deltaTime;
	}

	void GoalReaction()
	{
		m_score += m_goalBase / (m_time - m_lap);
		m_lap = m_time;

		bool active = false;
		foreach(var goal in m_goals)
		{
			if (goal.gameObject.activeInHierarchy)
			{
				active = true;
				break;
			}
		}

		if(active)
		{
			GameOver();
		}
	}

	void DeathReaction()
	{
		m_score -= m_deathBase;
	}

	void GameOver()
	{
		//TODO do something
	}
}
