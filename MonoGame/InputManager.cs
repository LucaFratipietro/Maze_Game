using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Maze;
using SharpDX.MediaFoundation;

namespace MonoGame
{
    public class InputManager
    {
        private Dictionary<Keys, Action> _handlerKeys = new Dictionary<Keys, Action>();
        private static InputManager _instance = null;

        //keyboard state for only running on handler per click
        private KeyboardState _currentState = Keyboard.GetState();
        private KeyboardState _previousState = Keyboard.GetState();


        private InputManager()
        { 

        }

        public static InputManager Instance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }

        public void AddKeyHandler(Keys key, Action handler)
        {
            _handlerKeys.Add(key, handler);
        } 

        public void Update()
        {
            //fetches the current state of the keyboard
            GetCurrentState();

            foreach (var input in _handlerKeys)
            {
                if (_currentState.IsKeyDown(input.Key))
                {
                    if (_previousState.IsKeyUp(input.Key))
                    {
                        input.Value?.Invoke();
                    }
                }
            }
        }

        public void ClearKeys()
        {
            _handlerKeys = new Dictionary<Keys, Action>();
        }

        private void GetCurrentState()
        {

            //sets previous states as the currentstate, and updates currentState with GetState
            _previousState = _currentState;
            _currentState = Keyboard.GetState();

        }

    }
}
