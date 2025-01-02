using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    private int m_ClickCount = 0; // 用于记录按钮点击次数
    private string m_ButtonPrefix = "Button"; // 按钮的前缀，用于从字符串中提取数字
    
    [MenuItem("Window/UI Toolkit/CustomEditor")]
    public static void ShowExample()
    {
        CustomEditor wnd = GetWindow<CustomEditor>();
        wnd.titleContent = new GUIContent("CustomEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
        Label title3 = new Label("These controls were created using C# code.");
        root.Add(title3);
        
        Button button = new Button();
        button.name = "Button3";
        button.text = "这是通过C#代码创建的按钮";
        root.Add(button);
        
        Toggle toggle = new Toggle();
        toggle.name = "Toggle3";
        toggle.label = "isShow3";
        root.Add(toggle);
        
        SetButtonHandler();
    }

    private void SetButtonHandler()
    {
        VisualElement root = rootVisualElement;
        var buttons = root.Query<Button>();
        buttons.ForEach((button) =>
        {
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
        });
    }
    private void OnButtonClicked(ClickEvent evt)
    {
        VisualElement root = rootVisualElement;
        m_ClickCount++;
        Button button = evt.currentTarget as Button;
        string buttonNumber = button.name.Substring(m_ButtonPrefix.Length);
        string toggleName = "Toggle" + buttonNumber;
        Toggle toggle = root.Q<Toggle>(toggleName);

        Debug.Log("按钮被点击了" + (toggle.value ? "次数: " + m_ClickCount : ""));
    }
}
