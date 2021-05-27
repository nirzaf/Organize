using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Organize.Shared.Enitites;
using Organize.Shared.Enums;
using Organize.TestFake;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Organize.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly IList<BaseItem> Items;

        static ItemsController()
        {
            Items = TestData.TestUser.UserItems.ToList();
            var childItems = Items.OfType<ParentItem>().SelectMany(p => p.ChildItems).ToList();
            foreach (var childItem in childItems)
            {
                Items.Add(childItem);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery]int type)
        {
            var userId = int.Parse(Request.HttpContext.User.FindFirst("id").Value);
            var typeEnum = (ItemTypeEnum) type;
            if(typeEnum == ItemTypeEnum.Child)
            {
                var parentItemsIds = Items
                    .Where(i => i.ParentId == userId && i.ItemTypeEnum == ItemTypeEnum.Parent)
                    .Select(i => i.Id)
                    .ToList();
                return Ok(Items.Where(i => parentItemsIds.Contains(i.ParentId) && i.ItemTypeEnum == typeEnum));
            }

            return Ok(Items.Where(i => i.ParentId == userId && i.ItemTypeEnum == typeEnum));
        }

        [HttpPost]
        public IActionResult Post(JObject itemAsJson)
        {
            var newCreatedItem = CreateItem(itemAsJson);
            var newId = Items.Count == 0
                ? 1
                : Items.Max(i => i.Id) + 1;
            newCreatedItem.Id = newId;
            Items.Add(newCreatedItem);
            return Created(string.Empty,newCreatedItem.Id);
        }

        private BaseItem CreateItem(JObject itemAsJson)
        {
            var asBaseItem = itemAsJson.ToObject<BaseItem>();

            BaseItem newCreatedItem = new BaseItem();
            switch (asBaseItem.ItemTypeEnum)
            {
                case ItemTypeEnum.Text:
                    newCreatedItem = itemAsJson.ToObject<TextItem>();
                    break;
                case ItemTypeEnum.Url:
                    newCreatedItem = itemAsJson.ToObject<UrlItem>();
                    break;
                case ItemTypeEnum.Parent:
                    newCreatedItem = itemAsJson.ToObject<ParentItem>();
                    break;
                case ItemTypeEnum.Child:
                    newCreatedItem = itemAsJson.ToObject<ChildItem>();
                    break;
            }

            return newCreatedItem;
        }


        [HttpPut]
        public IActionResult Put([FromBody]JObject itemAsJson)
        {
            var updatedItem = CreateItem(itemAsJson);
            var itemInList = Items.FirstOrDefault(i => i.Id == updatedItem.Id);

            if(itemInList == null)
            {
                return BadRequest("Item not found");
            }

            var index = Items.IndexOf(itemInList);
            Items[index] = updatedItem;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if(item == null)
            {
                return BadRequest("No item with id found");
            }

            Items.Remove(item);
            return Ok();
        }
    }
}
