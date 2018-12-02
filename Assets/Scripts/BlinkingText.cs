using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
	private TextMeshPro TextMesh;
	private Color targetColor;
	private Color baseColor;
	private bool fading;
	private float startTime;

	public float blinkDuration = 2f;
	public Color fullColor;
	public Color fadedColor;

	void Start()
    {
		TextMesh = GetComponent<TextMeshPro>();
		fading = true;
		baseColor = fullColor;
		targetColor = fadedColor;
		TextMesh.color = fullColor;

		StartCoroutine(BlinkLoop());
	}

    void Update()
    {
		TextMesh.color = Color.Lerp(baseColor, targetColor, (Time.time - startTime) / blinkDuration);
    }

	public IEnumerator BlinkLoop()
	{
		startTime = Time.time;

		yield return new WaitForSeconds(blinkDuration);

		if(fading)
		{
			targetColor = fullColor;
			baseColor = fadedColor;
		}
		else
		{
			targetColor = fadedColor;
			baseColor = fullColor;
		}

		fading = !fading;

		StartCoroutine(BlinkLoop());
	}
}
