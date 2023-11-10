using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using UnityEngine;

namespace Assets.Codebase.Utils.GOComponents
{
    public class PlaySoundComponent : MonoBehaviour
    {
        [SerializeField] private SoundId _soundId;

        private IAudioService _audioService;

        private void Awake()
        {
            _audioService = ServiceLocator.Container.Single<IAudioService>();
        }

        public void PlaySound()
        {
            _audioService.PlaySfxSound(_soundId);
        }
    }
}