using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Inventory;

public class Altar : MonoBehaviour
{
	[Serializable]
	public struct TypeAndPrefab
	{
		public Item EnemyType;
		public GameObject Prefab;
	}

	[Serializable]
	public struct TextAndBackground
	{
		public string Dialog;
		public GameObject DialogBackground;
		public AudioClip Clip;
	}

	private int victoryCondition;
	private int sacrificesCount;
	private int dialogCount;
	private bool endGame;
	private bool enableRestart;
	private bool lerpMusicVolumes;

	public Inventory playerInventory;
	public SpriteRenderer playerSacrificeSprite;
	public InputComponent PlayerInput;

	public TypeAndPrefab[] TypeAndPrefabs;

	public GameObject MeowDialogue;
	public GameObject ClosedDoor;

	public GameObject OriginalTilemap;
	public GameObject AnimationTilemaps;

	public TextMeshPro TextMesh;
	public GameObject DialogBackground;
	public GameObject ScreamDialogBackground;
	public TextAndBackground[] Dialogs;

	public SpriteRenderer EndGameFadeSprite;

	public GameObject EndTexts;

	public AudioSource AudioSource;
	public AudioClip MeowClip;
	public AudioClip DoorOpenClip;
	public AudioClip DoorCloseClip;

	public SimpleAudioEvent SacrificeDrop;

	public AudioSource MenuMusic;
	public AudioSource GameMusic;
	public float MenuMusicVolume;

	public void Start()
	{
		var enemies = FindObjectsOfType<EnemyType>();
		victoryCondition = enemies.Length;
		sacrificesCount = 0;
		dialogCount = 0;
		endGame = false;
		enableRestart = false;
		lerpMusicVolumes = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (playerInventory.CarriedItem != Inventory.Item.NONE)
			{
				playerSacrificeSprite.enabled = false;

				for(int i = 0; i < TypeAndPrefabs.Length; i++)
				{
					if(playerInventory.CarriedItem == TypeAndPrefabs[i].EnemyType)
					{
						Vector3 euler = Quaternion.identity.eulerAngles;
						euler.z = UnityEngine.Random.Range(0f, 360f);
						Instantiate(TypeAndPrefabs[i].Prefab, transform.position + Vector3.up * 0.5f + Vector3.right * UnityEngine.Random.Range(-0.1f, 0.1f), Quaternion.Euler(euler));
						continue;
					}
				}

				SacrificeDrop.Play(AudioSource);

				playerInventory.CarriedItem = Inventory.Item.NONE;

				sacrificesCount++;

				if(sacrificesCount == victoryCondition)
				{
					PlayerInput.Jump = false;
					PlayerInput.Run = false;
					PlayerInput.Horizontal = 0;

					PlayerInput.enabled = false;

					StartCoroutine(PlayEndGame());
				}
			}
		}
	}

	private IEnumerator PlayEndGame()
	{
		yield return new WaitForSeconds(1f);

		MeowDialogue.SetActive(true);
		AudioSource.volume = 1f;
		AudioSource.pitch = 1f;
		AudioSource.PlayOneShot(MeowClip);

		yield return new WaitForSeconds(2f);

		MeowDialogue.SetActive(false);

		yield return new WaitForSeconds(1f);

		ShowNextDialog();

		yield return new WaitForSeconds(2f);

		HideDialog();

		yield return new WaitForSeconds(3f);

		OriginalTilemap.SetActive(false);
		AnimationTilemaps.SetActive(true);

		ClosedDoor.SetActive(false);
		AudioSource.volume = 0.2f;
		AudioSource.PlayOneShot(DoorOpenClip);

		yield return new WaitForSeconds(1f);

		ShowNextDialog();

		yield return new WaitForSeconds(2f);

		HideDialog();

		yield return new WaitForSeconds(.5f);

		ShowNextDialog();

		yield return new WaitForSeconds(1f);

		PlayerInput.Run = false;
		PlayerInput.Horizontal = -1;

		yield return new WaitForSeconds(1f);

		HideDialog();

		yield return new WaitForSeconds(1f);

		ClosedDoor.SetActive(true);
		AudioSource.volume = 0.2f;
		AudioSource.PlayOneShot(DoorCloseClip);
		
		yield return new WaitForSeconds(1f);

		ShowNextDialog();

		yield return new WaitForSeconds(2f);

		endGame = true;
		MenuMusic.Play();
		lerpMusicVolumes = true;

		yield return new WaitForSeconds(4f);

		HideDialog();
		EndTexts.SetActive(true);

		enableRestart = true;
	}

	private void ShowNextDialog()
	{
		TextMesh.text = Dialogs[dialogCount].Dialog;
		Dialogs[dialogCount].DialogBackground.SetActive(true);
		TextMesh.gameObject.SetActive(true);
		AudioSource.volume = 1f;
		AudioSource.PlayOneShot(Dialogs[dialogCount].Clip);
		dialogCount++;
	}

	private void HideDialog()
	{
		DialogBackground.SetActive(false);
		ScreamDialogBackground.SetActive(false);
		TextMesh.gameObject.SetActive(false);
	}

	public void Update()
	{
		if(endGame)
		{
			EndGameFadeSprite.color = Color.Lerp(EndGameFadeSprite.color, Color.black, Time.deltaTime * 2f);
		}

		if(enableRestart && Input.anyKeyDown)
		{
			SceneManager.LoadScene(0);
		}

		if (lerpMusicVolumes)
		{
			MenuMusic.volume = Mathf.Lerp(MenuMusic.volume, MenuMusicVolume, Time.deltaTime);
			GameMusic.volume = Mathf.Lerp(GameMusic.volume, 0, Time.deltaTime);

			if (GameMusic.volume <= 0.1f && MenuMusicVolume - MenuMusic.volume < 0.1f)
			{
				MenuMusic.volume = MenuMusicVolume;
				GameMusic.volume = 0;
				lerpMusicVolumes = false;
			}
		}
	}
}
