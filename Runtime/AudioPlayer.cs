using UnityEngine;

namespace Ypmits.Unitytools
{
	public class AudioPlayerVO
	{
		public float startVolume = 0f;
		public float showTime = .25f;
		public float hideTime = .4f;
	}

	public class AudioPlayer
	{
		private AudioSource _audioSource;
		private AudioPlayerVO _vo;
		public bool mute { get; set; }
		private float _volume = 0f;
		public float volume
		{
			get => _volume;
			set
			{
				t = 0f;
				_volume = value;
				if (!_audioSource.isPlaying) _audioSource.Play();
			}
		}
		static float t = 0f;


		public AudioPlayer(AudioClip clip, Camera _camera, AudioPlayerVO vo = null)
		{
			if (vo == null) _vo = new AudioPlayerVO();

			_audioSource = _camera.gameObject.AddComponent<AudioSource>();
			_audioSource.clip = clip;
			_audioSource.loop = true;
			_audioSource.volume = _volume = _vo.startVolume;
		}

		public void Update()
		{
			var time = ((_audioSource.volume < _volume) ? _vo.showTime : _vo.hideTime);
			t += Time.deltaTime / time;
			_audioSource.volume = Mathf.Clamp01(Mathf.Lerp(_audioSource.volume, _volume, t));
			if (_audioSource.volume <= .01)
			{
				_audioSource.volume = 0;
				_audioSource.Stop();
			}
		}
	}
}