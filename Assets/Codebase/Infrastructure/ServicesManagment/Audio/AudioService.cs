using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Audio
{
    /// <summary>
    /// Implementation which loads sounds through asset provider.
    /// </summary>
    public class AudioService : IAudioService, IDisposable
    {
        private const string AudioPath = "Audio/AudioDataContainer";

        private IAssetProvider _assets;
        private IProgressModel _progress;

        // All clips loaded from container
        private Dictionary<SoundId, AudioClip> _clips;

        private AudioSource _sfxSource;
        private CompositeDisposable _disposables;

        public AudioService(IAssetProvider assetProvider, IProgressModel progressModel, AudioSource sfxSource)
        {
            _disposables = new CompositeDisposable();
            _assets = assetProvider;
            _progress = progressModel;
            _sfxSource = sfxSource;

            InitData();

            SetSoundVolume(_progress.SessionProgress.SoundVolume.Value);
            _progress.SessionProgress.SoundVolume.Subscribe(value => SetSoundVolume(value)).AddTo(_disposables);
        }

        private void InitData()
        {
            var audioData = _assets.LoadResource<AudioDataContainer>(AudioPath);

            if (audioData.AudioClips == null) return;

            _clips = new Dictionary<SoundId, AudioClip>();
            foreach (var clip in audioData.AudioClips)
            {
                _clips.Add(clip.Id, clip.Clip);
            }
        }

        public void SetSoundVolume(float value)
        {
            _sfxSource.volume = value;
        }

        public void ChangeMusic(SoundId musicId)
        {
            throw new System.NotImplementedException();
        }

        public void PlaySfxSound(SoundId soundId)
        {
            _sfxSource.PlayOneShot(_clips[soundId]);
        }

        public void MuteAll()
        {
            AudioListener.pause = true;
        }

        public void UnmuteAll()
        {
            AudioListener.pause = false;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
