using UnityEngine;

public class SourceManager3D : MonoBehaviour
{
	[SerializeField]
	private AudioClip audio;

	[SerializeField]
	private AudioSource source;

	[Header("Extra Settings")]
	[SerializeField]
	private float volume;

	[SerializeField]
	private bool randomizePitch;

	private void Update()
	{
		PlayAudio();
	}

	private void PlayAudio()
	{
		if (!source.isPlaying)
		{
			if (randomizePitch)
			{
				source.pitch = Random.Range(0.8f, 1.1f);
			}
			source.volume = volume;
			source.PlayOneShot(audio);
		}
	}
}
