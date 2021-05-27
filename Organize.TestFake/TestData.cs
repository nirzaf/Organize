using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Organize.TestFake
{
    public class TestData
    {
        public static User TestUser { get; private set; }

        public static void CreateTestUser(
            IUserItemManager userItemManager = null,
            IUserManager userManager = null)
        {
            var user = new User();
            user.Id = 123;
            user.UserName = "Ben";
            user.FirstName = "Benjamin";
            user.LastName = "Proft";
            user.Password = "test";
            user.GenderType = GenderTypeEnum.Male;
            user.UserItems = new ObservableCollection<BaseItem>();

            if(userManager != null)
            {
                userManager.InsertUserAsync(user);
            }

            TextItem textItem = null;
            if(userItemManager != null)
            {
                textItem = (TextItem)userItemManager
                    .CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Text).Result;
            } else
            {
                textItem = new TextItem();
                user.UserItems.Add(textItem);
            }

            
            textItem.ParentId = user.Id;    
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Detail = "From New Zealand preferred";
            textItem.ItemTypeEnum = ItemTypeEnum.Text;
            textItem.Position = 1;

            UrlItem urlItem;
            if (userItemManager != null)
            {
                urlItem = (UrlItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Url).Result;
            }
            else
            {
                urlItem = new UrlItem();
                user.UserItems.Add(urlItem);
            }

            urlItem.ParentId = user.Id;
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://drive.google.com/file/d/1NXiNFLEUGUiNtkyzdHDtf-HDocFh3OJ0/view?usp=sharing";
            urlItem.ItemTypeEnum = ItemTypeEnum.Url;
            urlItem.Position = 2;

            ParentItem parentItem;
            if (userItemManager != null)
            {
                parentItem = (ParentItem)userItemManager
                    .CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Parent)
                    .Result;
            }
            else
            {
                parentItem = new ParentItem();
                user.UserItems.Add(parentItem);
            }

            parentItem.ParentId = user.Id;
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemTypeEnum = ItemTypeEnum.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            ChildItem childItem;
            if (userItemManager != null)
            {
                childItem = (ChildItem)userItemManager
                    .CreateNewChildItemAndAddItToParentItemAsync(parentItem).Result;

                //Clear becuase entites are stored
                user.UserItems.Clear();
            }
            else
            {
                childItem = new ChildItem();
                parentItem.ChildItems.Add(childItem);
            }
            childItem.ParentId = parentItem.Id;
            childItem.Id = 4;
            childItem.ItemTypeEnum = ItemTypeEnum.Child;
            childItem.Position = 1;
            childItem.Title = "Cut";

            TestUser = user;
        }
    }
}
