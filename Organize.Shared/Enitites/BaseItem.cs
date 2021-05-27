using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Enitites
{
    public class BaseItem : BaseEntity
    {
        public int ParentId 
        { 
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }
        private int _parentId;

        public ItemTypeEnum ItemTypeEnum {
            get => _itemTypeEnum; 
            set => SetProperty(ref _itemTypeEnum,value); 
        }
        private ItemTypeEnum _itemTypeEnum;

        public int Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        private int _position;

        public bool IsDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }
        private bool _isDone = false;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;
    }
}
