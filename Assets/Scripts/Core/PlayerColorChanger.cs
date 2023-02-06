using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class PlayerColorChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _playerMesh;
        [SerializeField] private List<PlayerStateColor> _colorsByState;
        
        
        public void ChangeColor(PlayerState playerState)
        {
            var newColor = _colorsByState.FirstOrDefault(x => x.PlayerState == playerState)?.Color;
            
            if (newColor.HasValue)
                _playerMesh.material.color = newColor.Value;
        }


        private void Start()
        {
            _playerMesh.material = Instantiate(_playerMesh.material); // make material unique
        }


        [Serializable]
        private class PlayerStateColor
        {
            [SerializeField] private PlayerState _playerState;
            [SerializeField] private Color _color;
            
            public PlayerState PlayerState => _playerState;
            public Color Color => _color;
        }
    }
}
