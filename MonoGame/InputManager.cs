using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Maze;

namespace MonoGame
{
    public class InputManager
    {
        private Dictionary<Key, Action> _handlerKeys = new Dictionary<Key, Action>();
        private static InputManager _instance = null;

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

        public void AddKeyHandler(Key key, Action handler)
        {
            _handlerKeys.Add(key, handler);
        } 

        public void Update()
        {
            foreach (var input in _handlerKeys)
            {
                if (Keyboard.IsKeyDown(input.Key))
                {
                    input.Value?.Invoke();
                }
            }

        }

    }
}
