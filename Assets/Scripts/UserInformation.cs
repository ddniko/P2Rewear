using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;


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
    public RawImage ProfilePic;
    public MParent User;
    public List<MChild> UserChildren;
}

