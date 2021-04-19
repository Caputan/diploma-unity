using System.Collections;
using Coroutine;
using Interfaces;
using UnityEngine;

namespace Animation
{
    public sealed class RotateAFewSeconds: GameHandler
    {
        private readonly GameObject _gameObject;
        private readonly float _rotationSpeed;
        private readonly float _rotationDuration;

        public RotateAFewSeconds(GameObject gameObject, float rotationSpeed, float rotationDuration)
        {
            _gameObject = gameObject;
            _rotationSpeed = rotationSpeed;
            _rotationDuration = rotationDuration;
        }
        
        private IEnumerator StartRotating()
        {
            var t = 0.0f;
            while ( t  < _rotationDuration )
            {
                t += Time.deltaTime;
                _gameObject.transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
                yield return null;
            }
            base.Handle();
        }

        public override IGameHandler Handle()
        {
            StartRotating().StartCoroutine(out _);
            return this;
        }

    }
}