using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonoGame
{
    public class InputManager
    {
        private List<Action> _actions = new List<Action>();
        private static InputManager _instance = null;

        private InputManager() { }

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
            // add keys here
        } 

        public void Update()
        {

        }

    }
}
