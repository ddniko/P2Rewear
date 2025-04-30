using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DBManager;

public class DBVisualizer : MonoBehaviour
{

    public TextMeshProUGUI outputText;

    void Start()
    {

        

        DisplayParents();

    }


    public void DisplayParents()
    {

        List<MParent> parents = DBManager.GetAllParents();

        outputText.text = "Parents:\n";
        foreach (var parent in parents)
        {
            outputText.text += $"ID: {parent.Id}, Name: {parent.Name}, S/R Score : {parent.SustainabilityScore}/{parent.ReliabilityScore} \n";
        }
    }

    public void DisplayChildren(int parentId)
    {

        List<MChild> children = DBManager.GetChildrenByParentId(parentId);

        outputText.text = $"Children for MParent ID {parentId}:\n";
        foreach (var child in children)
        {
            outputText.text += $"ID: {child.Id}, Name: {child.Name}, Age: {child.Age}, Size: {child.Size}\n";
        }
    }


    public void DisplayArticles(int childId)
    {

        List<MArticle> articles = DBManager.GetArticlesByChildId(childId);

        outputText.text = $"Articles for MChild ID {childId}:\n";
        foreach (var article in articles)
        {
            outputText.text += $"ID: {article.Id}, Size: {article.Size}, Condition: {article.Condition}, Lifetime: {article.LifeTime}\n";
        }
    }


    public void AddParent(string name, int? sustainabilityScore, int? reliabilityScore, string password, string email, float distance)
    {
        DBManager.AddParent(name, sustainabilityScore, reliabilityScore, password, email, distance);
        DisplayParents();
    }


    public void AddChild(string name, int parentId, string age, string size)
    {
        DBManager.AddChild(name, parentId, age, size);
        DisplayChildren(parentId);
    }


    public void AddArticle(string name, int childId, string sizeCategory, string category, float condition, int? lifeTime, float prize, string description, byte[] imageData )
    {
        DBManager.AddArticle(name, childId, category, sizeCategory, condition, lifeTime, prize, description, imageData);
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

    #endregion
}
