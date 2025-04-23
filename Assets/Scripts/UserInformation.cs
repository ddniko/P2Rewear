using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UserInformation
{
    private static UserInformation instance;
    private UserInformation()
    {

    }
    public static UserInformation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserInformation();
            }
            return instance;
        }
    }
    public Image ProfilePic;
    public MParent User;
    public List<MChild> UserChildren;
}

