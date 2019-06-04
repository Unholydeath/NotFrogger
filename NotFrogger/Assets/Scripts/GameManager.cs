using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] float m_transitionTime = 5.0f;
	[SerializeField] GameObject m_winPanel;
	[SerializeField] GameObject m_losePanel;
	[SerializeField] GameObject m_hud;

	[SerializeField] int m_goalsToWin = 5;
	[SerializeField] float m_goalBase = 1f;
	[SerializeField] float m_deathBase = 1f;

	static float m_score = 0.0f;
	static float m_time = 0.0f;
	static GameManager m_selfRef = null;

	float m_lap = 0.0f;
	bool m_inTransition = false;

	public static int Score { get { return (int)m_score; } }
	public static int GameTime { get { return (int)m_time; } }
	public static bool ManagerExists { get { return m_selfRef != null; } }

	public Text GameScore;
	public Text GTime;

	static bool m_lost = false;

	private void Start()
	{
		m_selfRef = this;

		Interactable.OnGoalCollision += Goal;

		GameScore.text = "" + Score.ToString();

		m_losePanel.SetActive(false);
		m_winPanel.SetActive(false);
		m_hud.SetActive(true);
	}

	private void OnDestroy()
	{
		Interactable.OnGoalCollision -= Goal;
	}

	private void FixedUpdate()
	{
		if (!m_lost && m_goalsToWin > 0)
		{
			m_time += Time.deltaTime;
			GTime.text = "" + GameTime.ToString();
		}
		else
		{
			if (m_goalsToWin == 0) m_winPanel.SetActive(true);
			else if (m_lost) m_losePanel.SetActive(true);

			if (!m_inTransition)
			{
				StartCoroutine("ToStart");
			}
		}
	}

	void Goal()
	{
		m_score += m_goalBase + (m_lap /  GameTime);
		GameScore.text = "" + Score.ToString();

		m_lap = GameTime;
		--m_goalsToWin;
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

	IEnumerator ToStart()
	{
		Debug.Log("Back to start");
		m_hud.SetActive(false);
		m_inTransition = true;
		yield return new WaitForSeconds(m_transitionTime);
		SceneManager.LoadScene("Start");
	}
}
