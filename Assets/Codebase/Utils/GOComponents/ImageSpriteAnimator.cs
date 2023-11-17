using Assets.Codebase.Data.WarmUp;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Utils.GOComponents
{
    [RequireComponent(typeof(Image))]
    public class ImageSpriteAnimator : MonoBehaviour
    {
        [SerializeField][Range(1, 30)] private int _frameRate = 10;
        [SerializeField] private bool _loop = true;
        [SerializeField] private List<Sprite> _spiteList;

        private Image _image;

        private float _secPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private bool _isPlaying = true;

        private void Awake()
        {
            _spiteList = new List<Sprite>();
        }

        private void Start()
        {
            _image = GetComponent<Image>();
            _secPerFrame = 1f / _frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetAnimationClip(SimpleAnimationClip newClip)
        {
            if (newClip.Sprites == null) return;

            _spiteList = newClip.Sprites;
            _currentFrame = 0;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;
            if (_spiteList == null) return;

            if (_currentFrame >= _spiteList.Count)
            {
                if (_loop)
                {
                    _currentFrame = 0;
                }
                return;
            }

            _image.sprite = _spiteList[_currentFrame];

            _nextFrameTime += _secPerFrame;
            _currentFrame++;
        }
    }
}