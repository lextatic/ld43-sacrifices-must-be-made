using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
	private bool introStarted;
	private bool introEnded;
	private Color fadeColor = new Color(0, 0, 0, 0);
	private Color targetColor;
	private Color baseColor;
	private bool fading;
	private float startTime;
	private Coroutine introCoroutine;
	private bool lerpMusicVolumes;
	

	public InputComponent PlayerInput;
	public SpriteRenderer IntroBackground;
	public TextMeshPro IntroText;
	public string[] Texts;
	public Color SacrificesColor;

	public AudioSource MenuMusic;
	public AudioSource GameMusic;
	public float GameMusicVolume;

	// Start is called before the first frame update
	public void Start()
    {
		IntroBackground.gameObject.SetActive(true);
		introEnded = false;
		introStarted = false;
		PlayerInput.enabled = false;
		IntroText.gameObject.SetActive(false);
		fading = false;
		lerpMusicVolumes = false;

		introCoroutine = StartCoroutine(PlayIntro());
	}

	private IEnumerator PlayIntro()
	{
		yield return new WaitForSeconds(3f);
		introStarted = true;
		IntroText.gameObject.SetActive(true);
		IntroText.text = Texts[0];
		yield return StartCoroutine(ShowText(Color.white));

		IntroText.text = Texts[1];
		yield return StartCoroutine(ShowText(Color.white));

		IntroText.text = Texts[2];
		yield return StartCoroutine(ShowText(SacrificesColor));
		
		yield return new WaitForSeconds(1f);
		 
		fading = true;

		yield return new WaitForSeconds(1f);

		lerpMusicVolumes = true;
		IntroText.text = Texts[3];
		yield return StartCoroutine(ShowText(Color.white, 5f));

		EndIntro();
	}

    // Update is called once per frame
    public void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (introStarted && !introEnded)
		{
			IntroText.color = Color.Lerp(baseColor, targetColor, Mathf.Min((Time.time - startTime) / 1f, 1));

			if (fading)
			{
				IntroBackground.color = Color.Lerp(IntroBackground.color, fadeColor, Mathf.Min(Time.deltaTime * 2f, 1));
			}

			if (Input.anyKeyDown)
			{
				EndIntro();
			}
		}

		if(lerpMusicVolumes)
		{
			MenuMusic.volume = Mathf.Lerp(MenuMusic.volume, 0, Time.deltaTime);
			GameMusic.volume = Mathf.Lerp(GameMusic.volume, GameMusicVolume, Time.deltaTime);

			if (MenuMusic.volume <= 0.1f && GameMusicVolume - GameMusic.volume < 0.1f)
			{
				MenuMusic.volume = 0;
				GameMusic.volume = GameMusicVolume;
				lerpMusicVolumes = false;
			}
		}
	}

	private void EndIntro()
	{
		introEnded = true;
		PlayerInput.enabled = true;
		IntroText.gameObject.SetActive(false);
		IntroBackground.color = fadeColor;
		lerpMusicVolumes = true;

		StopCoroutine(introCoroutine);
	}

	private IEnumerator ShowText(Color color, float time = 3f)
	{
		baseColor = IntroText.color;
		startTime = Time.time;
		targetColor = color;
		yield return new WaitForSeconds(time);

		baseColor = IntroText.color;
		startTime = Time.time;
		targetColor = fadeColor;
		yield return new WaitForSeconds(2f);
	}
}
