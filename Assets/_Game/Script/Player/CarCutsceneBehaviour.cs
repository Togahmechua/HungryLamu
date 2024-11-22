using UnityEngine;

public class CarCutsceneBehaviour : MonoBehaviour
{
	[Header("Components")]
	[SerializeField]
	private GameObject blackScreen;

	[SerializeField]
	private LamuBehaviour lamuBehave;

	[Header("Audio")]
	[SerializeField]
	private AudioClip screech_sfx;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		blackScreen.SetActive(value: false);
	}

	public void BlackScreen()
	{
		blackScreen.SetActive(value: true);
	}

	public void PlayScreechSound()
	{
		SoundFXManager.Ins.PlaySFX(screech_sfx);
	}

	public void PlayLamuSound()
	{
		lamuBehave.Jumpscare();
	}

	public void ChangeMusicTheme()
	{
		SoundFXManager.Ins.StopMusicSource();
    }
}
