using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DBManager;

public class DBVisualizer : MonoBehaviour
{

    public Text outputText;

    void Start()
    {

        DBManager.Init();

        DisplayParents();

    }


    public void DisplayParents()
    {

        List<Parent> parents = DBManager.GetAllParents();

        outputText.text = "Parents:\n";
        foreach (var parent in parents)
        {
            outputText.text += $"ID: {parent.Id}, Name: {parent.Name}, S/R Score : {parent.SustainabilityScore}/{parent.ReliabilityScore} \n";
        }
    }


    public void DisplayChildren(int parentId)
    {

        List<Child> children = DBManager.GetChildrenByParentId(parentId);

        outputText.text = $"Children for Parent ID {parentId}:\n";
        foreach (var child in children)
        {
            outputText.text += $"ID: {child.Id}, Name: {child.Name}, Age: {child.Age}, Size: {child.Size}\n";
        }
    }


    public void DisplayArticles(int childId)
    {

        List<Article> articles = DBManager.GetArticlesByChildId(childId);

        outputText.text = $"Articles for Child ID {childId}:\n";
        foreach (var article in articles)
        {
            outputText.text += $"ID: {article.Id}, Category: {article.Category}, Size: {article.SizeCategory}, Condition: {article.Condition}, Lifetime: {article.LifeTime}\n";
        }
    }


    public void AddParent(string name, int? sustainabilityScore, int? reliabilityScore)
    {
        DBManager.AddParent(name, sustainabilityScore, reliabilityScore);
        DisplayParents();
    }


    public void AddChild(string name, int parentId, int? age, string size)
    {
        DBManager.AddChild(name, parentId, age, size);
        DisplayChildren(parentId);
    }


    public void AddArticle(string name, string category, int childId, string sizeCategory, float condition, int? lifetime)
    {
        DBManager.AddArticle(name, childId, category, sizeCategory, condition, lifetime);
        DisplayArticles(childId);
    }



    public void DeleteParent(int parentId)
    {
        DBManager.DeleteParent(parentId);
        DisplayParents();
    }

    public void DeleteChild(int childId)
    {
        DBManager.DeleteChild(childId);
        DisplayChildren(childId);
    }


    public void DeleteArticle(int articleId)
    {
        DBManager.DeleteArticle(articleId);
        DisplayArticles(articleId);
    }

    void OnDestroy()
    {
        DBManager.Close();
    }


    #region TestMethods
    [Header("Testing")]
    public TMP_InputField nameInput;
    public TMP_InputField sustainabilityInput;
    public TMP_InputField reliabilityInput;

    public void AddTestParent()
    {
        DBManager.AddParent("Test Parent", 80, 90);
        DisplayParents();
    }

    public void AddParentFromUI()
    {
        string name = nameInput.text;
        int? sustainability = int.TryParse(sustainabilityInput.text, out var s) ? s : (int?)null;
        int? reliability = int.TryParse(reliabilityInput.text, out var r) ? r : (int?)null;

        DBManager.AddParent(name, sustainability, reliability);
        DisplayParents();
    }
    #endregion
}
