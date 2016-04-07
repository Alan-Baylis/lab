﻿using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Component to control ai behaviour.
    /// <para>AiController inherits from MonoBehaviour, so  this one should be attached to your game object. It is also responsible for binding tasks scripts with the logic (trees' tasks).
    /// Simply attach a AiBehaviour asset and tasks scripts. Use AiController to modificate AiBehaviour's parameters.</para>
    /// </summary>
    /// <example>
    /// This sample shows how to update parameters for current controller and run default behaviour tree.
    /// <code>
    ///using UnityEngine;
    ///using System.Collections;
    ///using lab;
    ///
    ///public class ExampleClass : MonoBehaviour {
    ///    public AiController controller;
    ///    void Start() {
    ///        controller.SetInt("money", 10);
    ///        if (!controller.Run()) {
    ///            Debug.Log("first tree failed.");
    ///        }
    ///    }
    ///}
    /// </code>
    /// </example>
    [DisallowMultipleComponent]
    public class AiController : MonoBehaviour {

        [SerializeField]
        private AiBehaviour _behaviour;
        [SerializeField]
        private List<ATaskScript> _tasks;

        private AiBlackboard _blackboard;
        private IList<AiTree> _trees;

        private AiBlackboard Blackboard {
            get {
                if (_behaviour == null) {
                    return null;
                }
                if (_blackboard == null) {
                    _blackboard = _behaviour.Blackboard.Clone();
                }
                return _blackboard;
            }
        }

        private IList<AiTree> Trees {
            get {
                if(_trees == null) {
                    if(_behaviour == null) {
                        return null;
                    }
                    _trees = _behaviour.Trees;
                }
                return _trees;
            }
        }

        /// <summary>
        /// Gets/Sets Behaviour for current AiController.
        /// </summary>
        public AiBehaviour Behaviour {
            get { return _behaviour; }
            set { _behaviour = value; _blackboard = null; }
        }

        /// <summary>
        /// Sets/Gets task list.
        /// </summary>
        public List<ATaskScript> Tasks {
            get { return _tasks; }
            set { _tasks = value; }
        }

        /// <summary>
        /// Sets int parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int parameters.</param>
        /// <param name="val">The new value for the int parameters.</param>
        public void SetInt(string key, int val) {
            Blackboard.IntParameters[key] = val;
        }

        /// <summary>
        /// Gets int parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int parameters.</param>
        /// <returns>The value for the int parameters.</returns>
        public int GetInt(string key) {
            return Blackboard.IntParameters[key];
        }

        /// <summary>
        /// Gets all int parameters keys.
        /// </summary>
        public IntParameter.KeyCollection IntKeys {
            get { return Blackboard.IntParameters.Keys; }
        }

        /// <summary>
        /// Sets float parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float parameters.</param>
        /// <param name="val">The new value for the float parameters.</param>
        public void SetFloat(string key, float val) {
            Blackboard.FloatParameters[key] = val;
        }

        /// <summary>
        /// Gets float parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float parameters.</param>
        /// <returns>The value for the float parameters.</returns>
        public float GetFloat(string key) {
            return Blackboard.FloatParameters[key];
        }

        /// <summary>
        /// Gets all float parameters keys.
        /// </summary>
        public FloatParameter.KeyCollection FloatKeys {
            get { return Blackboard.FloatParameters.Keys; }
        }

        /// <summary>
        /// Sets bool parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool parameters.</param>
        /// <param name="val">The new value for the bool parameters.</param>
        public void SetBool(string key, bool val) {
            Blackboard.BoolParameters[key] = val;
        }

        /// <summary>
        /// Gets bool parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool parameters.</param>
        /// <returns>The value for the bool parameters.</returns>
        public bool GetBool(string key) {
            return Blackboard.BoolParameters[key];
        }

        /// <summary>
        /// Gets all bool parameters keys.
        /// </summary>
        public BoolParameter.KeyCollection BoolKeys {
            get { return Blackboard.BoolParameters.Keys; }
        }

        /// <summary>
        /// Sets string parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string parameters.</param>
        /// <param name="val">The new value for the string parameters.</param>
        public void SetString(string key, string val) {
            Blackboard.StringParameters[key] = val;
        }

        /// <summary>
        /// Gets string parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string parameters.</param>
        /// <returns>The value for the string parameters.</returns>
        public string GetString(string key) {
            return Blackboard.StringParameters[key];
        }

        /// <summary>
        /// Gets all string parameters keys.
        /// </summary>
        public StringParameter.KeyCollection StringKyes {
            get { return Blackboard.StringParameters.Keys; }
        }

        /// <summary>
        /// Runs tree behaviour.
        /// </summary>
        /// <param name="i">Index of tree to run. Default value is 0.</param>
        /// <returns>True if tree succeed. Otherwise false.</returns>
        public bool Run(int i = 0) {
            return Trees[i].Run(Blackboard, Trees, Tasks);
        }
    }
}
