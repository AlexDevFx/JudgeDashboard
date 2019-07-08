using System;
using System.Collections.Generic;

namespace JudgeScores
{
    public class ActionHandler
    {
        private readonly Dictionary<MainActionTypes, Action> _actions = new Dictionary<MainActionTypes, Action>();
            
        public void AddAction(MainActionTypes actionType, Action action)
        {
            if (_actions.ContainsKey(actionType))
            {
                _actions[actionType] = action;
            }
            else
            {
                _actions.Add(actionType, action);
            }
        }

        public void PerformAction(MainActionTypes actionType)
        {
            if (_actions.TryGetValue(actionType, out Action action))
            {
                action.Invoke();
            }
        }

        public IEnumerable<MainActionTypes> GetActions()
        {
            return _actions.Keys;
        }
    }
}