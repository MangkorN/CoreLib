using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreLib.Utilities;

namespace CoreLib.Components
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnAnimationStart;
        public event Action OnAnimationEnd;

        public enum AnimatorType { Generic, UIElements }
        [EnumTooltip(
            "Generic Animator. (Default)",
            "Animator for UI elements, allows for swapping with other UI animators if needed."
            )]
        [SerializeField] private AnimatorType _animatorType;

        private Animator _animator;
        private bool _isPlaying;

        public AnimatorType Type => _animatorType;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// To be manually added to the Animation timeline at the start of the animation.
        /// </summary>
        private void AnimationStart()
        {
            _isPlaying = true;
            OnAnimationStart?.Invoke();
        }

        /// <summary>
        /// To be manually added to the Animation timeline at the end of the animation.
        /// </summary>
        private void AnimationEnd()
        {
            _isPlaying = false;
            OnAnimationEnd?.Invoke();
        }

        public void SetAnimatorTrigger(string name)
        {
            _animator.enabled = true;
            _animator.SetTrigger(name);
        }

        /// <summary>
        /// Disables the animator. Invokes OnAnimationEnd if Animator was playing.
        /// </summary>
        /// <returns>Animator was playing.</returns>
        public bool StopAnimator()
        {
            _animator.enabled = false;

            if (_isPlaying)
            {
                _isPlaying = false;
                OnAnimationEnd?.Invoke();
                return true;
            }
            return false;
        }

        public void ReplaceAnimator(RuntimeAnimatorController animController)
        {
            _animator.runtimeAnimatorController = animController;
        }
    }
}
