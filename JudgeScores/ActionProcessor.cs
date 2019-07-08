using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Windows.Gaming.Input;

namespace JudgeScores
{
    public class ActionProcessor
    {
        private readonly ActionsProvider _actionsProvider = new ActionsProvider();
        private readonly ActionHandler _actionHandler = new ActionHandler();

        public void AddAction(MainActionTypes actionType, GamepadButtons button, Action action)
        {
            _actionsProvider.AddAction(button, actionType);
            _actionHandler.AddAction(actionType, action);
        }
        
        public void AddAction(MainActionTypes actionType, Keys button, Action action)
        {
            _actionsProvider.AddAction(button, actionType);
            _actionHandler.AddAction(actionType, action);
        }
        
        public void AddAction(MainActionTypes actionType, Action action)
        {
            _actionHandler.AddAction(actionType, action);
        }
        
        public void AddAction(Keys button, MainActionTypes actionType)
        {
            _actionsProvider.AddAction(button, actionType);
        }
        
        public void AddAction(GamepadButtons button, MainActionTypes actionType)
        {
            _actionsProvider.AddAction(button, actionType);
        }

        public void PerformAction(Keys button)
        {
            MainActionTypes? actionType = _actionsProvider.GetAction(button);
            
            if(actionType.HasValue)
                _actionHandler.PerformAction(actionType.Value);
        }
        
        public void PerformAction(GamepadButtons button)
        {
            MainActionTypes? actionType = _actionsProvider.GetAction(button);
            
            if(actionType.HasValue)
                _actionHandler.PerformAction(actionType.Value);
        }

        public bool Contains(GamepadButtons button)
        {
            return _actionsProvider.Contains(button);
        }
        
        public bool Contains(Keys button)
        {
            return _actionsProvider.Contains(button);
        }
        
        public void Remove(GamepadButtons button)
        {
            _actionsProvider.Remove(button);
        }
        
        public void Remove(Keys button)
        {
            _actionsProvider.Remove(button);
        }

        public Dictionary<MainActionTypes, ButtonConfig> GetActionsBinds()
        {
            Dictionary<MainActionTypes, ButtonConfig> binds = new Dictionary<MainActionTypes, ButtonConfig>();
            
            foreach (MainActionTypes actionType in _actionHandler.GetActions())
            { 
                binds.Add(actionType, _actionsProvider.GetButton(actionType));
            }

            return binds;
        }
    }
}