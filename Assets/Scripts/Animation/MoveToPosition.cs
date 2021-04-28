using System.Collections;
using Coroutine;
using Interfaces;
using UnityEngine;

namespace Animation
{
    public sealed class MoveToPosition: GameHandler
    {
        private readonly GameObject _gameObject;
        private readonly Vector3 _positionToMove;
        private float _speed;
        private bool _moveToPosition;

        public MoveToPosition(GameObject gameObject, Vector3 vector3, float speed)
        {
            // сюда приходит объект и конечная точка
            _gameObject = gameObject;
            _positionToMove = vector3;
            _speed = speed;
        }
        
        private IEnumerator StartMoving()
        {
            while ((_gameObject.transform.position -_positionToMove).sqrMagnitude > 0.0f)
            {
                _gameObject.transform.position = Vector2.MoveTowards(
                    _gameObject.transform.position, _positionToMove, Time.deltaTime * _speed);
                yield return null;
            }
            base.Handle();
        }

        public override IGameHandler Handle()
        {
            StartMoving().StartCoroutine(out _,out _);
            return this;
        }
    }
}