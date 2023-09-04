using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Kultie.StateMachine.Behavior.EditorUtility
{
    public class StateMachineCreator : EditorWindow
    {
        string prefixClassName = "MyPrefix";
        string nameSpace = "MyNameSpace";
        string states = "Start,Process,End";
        string contextName = "Context";
        string targetFolder = "StateMachine";
        [MenuItem("Kultie/Create State Machine")]
        static void ShowWindow()
        {
            GetWindow<StateMachineCreator>(false, "State Machine Creator", true);

        }

        private void OnGUI()
        {
            nameSpace = EditorGUILayout.TextField("Namespace", nameSpace).Replace(@"\s", "");
            states = EditorGUILayout.TextField("States", states).Replace(@"\s", "");
            targetFolder = EditorGUILayout.TextField("Target Folder", targetFolder).Replace(@"\s", "");
            prefixClassName = EditorGUILayout.TextField("Prefix Class Name", prefixClassName).Replace(@"\s", "");
            if (GUILayout.Button("Create"))
            {
                CreateStateMachine();
                CreateContext();
                CreateStaticClass();
                CreateStates();

                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("Clear"))
            {
                prefixClassName = "";
                targetFolder = "";
                nameSpace = "";
                states = "";
                contextName = "";
            }
        }

        void CreateStateMachine()
        {
            string template = GetTemplate("StateMachine_Template");
            string content = template.Replace("[NameSpace]", nameSpace);
            content = content.Replace("[ContextName]", prefixClassName + contextName);
            content = content.Replace("[Prefix]", prefixClassName);
            CreateFile(prefixClassName + "StateMachine.cs", content);
        }

        void CreateContext()
        {
            string template = GetTemplate("StateContext_Template");
            string content = template.Replace("[NameSpace]", nameSpace);
            content = content.Replace("[ContextName]", prefixClassName + contextName);
            content = content.Replace("[Prefix]", prefixClassName);
            CreateFile(prefixClassName + contextName + ".cs", content);
        }

        void CreateStaticClass()
        {
            string template = GetTemplate("StaticClass_Template");
            string content = template.Replace("[NameSpace]", nameSpace);
            content = content.Replace("[Prefix]", prefixClassName);
            var names = GetStateNames();
            string namesContent = "";
            foreach (var n in names)
            {
                namesContent += string.Format("        public const string {0} = \"{1}\"; \n", n.ToUpper(), n);
            }
            content = content.Replace("[StateNames]", namesContent);
            CreateFile(prefixClassName + "StateMachineKeys.cs", content);

        }

        void CreateStates()
        {
            string template = GetTemplate("StateComponent_Template");
            string content = template.Replace("[NameSpace]", nameSpace);
            content = content.Replace("[ContextName]", prefixClassName + contextName);
            content = content.Replace("[Prefix]", prefixClassName);
            var names = GetStateNames();
            foreach (var n in names)
            {
                var stateName = n.Trim();
                string fileContent = content.Replace("[StateName]", prefixClassName + stateName);
                fileContent = fileContent.Replace("[StateNameKey]", stateName.ToUpper());
                CreateFile(string.Format("{0}State.cs", prefixClassName + stateName), fileContent);
            }
        }

        string[] GetStateNames()
        {
            string[] result = states.Split(',');
            return result;
        }

        void CreateFile(string fileName, string content)
        {
            string scriptDirectory = Path.Combine(Application.dataPath, "Scripts");
            if (!Directory.Exists(scriptDirectory))
            {
                Directory.CreateDirectory(scriptDirectory);
            }
            string folderDirectory = Path.Combine(scriptDirectory, targetFolder);
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            string _fileName = Path.Combine(folderDirectory, fileName);

            File.WriteAllText(_fileName, content);
        }

        string GetTemplate(string fileName)
        {
            string path = Path.Combine(Application.dataPath, "Plugins/Kultie", "StateMachine", "Components", "Editor", fileName + ".txt");
            return File.ReadAllText(path);
        }
    }
}