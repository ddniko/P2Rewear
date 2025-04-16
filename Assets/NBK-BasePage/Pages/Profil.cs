using System;
using TMPro;
using UnityEngine;

public class Profil : BasePage
{
    public override Enum MyPage() => PAGENAMES.PROFIL;
    public TMP_Text TrustScore;
    public TMP_Text SustainabilityScore;
    public TMP_Text Name;
    private void Start()
    {
        TrustScore.text = LogIn.LoggedIn.ReliabilityScore.ToString();
        SustainabilityScore.text = LogIn.LoggedIn.SustainabilityScore.ToString();
        Name.text = LogIn.LoggedIn.Name.ToString();
    }
}
