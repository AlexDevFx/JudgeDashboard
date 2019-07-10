using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Windows.Gaming.Input;

namespace JudgeScores
{
    public class ActionsProvider
    {
        private readonly Dictionary<GamepadButtons, MainActionTypes> _funcButtonsBinds = new Dictionary<GamepadButtons, MainActionTypes>();
        private readonly Dictionary<Keys, MainActionTypes> _funcKeysBinds = new Dictionary<Keys, MainActionTypes>();
        public MainActionTypes? GetAction(GamepadButtons button)
        {
            return GetActionInternal(_funcButtonsBinds, button);
        }
        
        public MainActionTypes? GetAction(Keys button)
        {
            return GetActionInternal(_funcKeysBinds, button);
        }
        
        public void AddAction(GamepadButtons button, MainActionTypes actionType)
        {
            AddActionInternal(_funcButtonsBinds, button, actionType);
        }
        
        public void AddAction(Keys button, MainActionTypes actionType)
        {
            AddActionInternal(_funcKeysBinds, button, actionType);
        }
        
        private MainActionTypes? GetActionInternal<T>(Dictionary<T, MainActionTypes> binds, T button)
        {
            MainActionTypes actionTypes;

            if( binds.TryGetValue(button, out actionTypes) )
                return actionTypes;

            return null;
        }
        
        public bool Contains(GamepadButtons button)
        {
            return _funcButtonsBinds.ContainsKey(button);
        }
        
        public bool Contains(Keys button)
        {
            return _funcKeysBinds.ContainsKey(button);
        }
        
        public bool Remove(GamepadButtons button)
        {
            return _funcButtonsBinds.Remove(button);
        }
        
        public bool Remove(Keys button)
        {
            return _funcKeysBinds.Remove(button);
        }
        
        private void AddActionInternal<T>(Dictionary<T, MainActionTypes> binds, T button, MainActionTypes actionType)
        {
            if( binds.ContainsKey(button) )
            {
                binds[button] = actionType;
            }
            else if (binds.ContainsValue(actionType))
            {
                var bind = binds.FirstOrDefault(e => e.Value == actionType);
                binds.Remove(bind.Key);
                binds.Add(button, actionType);
            }
            else
            {
                binds.Add(button, actionType);
            }
        }

        public IEnumerable<MainActionTypes> GetActions()
        {
            throw new System.NotImplementedException();
        }

        public ButtonConfig GetButton(MainActionTypes actionType)
        {
            ButtonConfig buttonConfig = new ButtonConfig();
            
            if(_funcButtonsBinds.Any(e => e.Value == actionType))
                buttonConfig.Button = _funcButtonsBinds.FirstOrDefault(e => e.Value == actionType).Key;
            if(_funcKeysBinds.Any(e => e.Value == actionType))
                buttonConfig.Key = _funcKeysBinds.FirstOrDefault(e => e.Value == actionType).Key;

            return buttonConfig;
        }
    }
}