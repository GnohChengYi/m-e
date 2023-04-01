using m_e.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m_e.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        readonly List<Item> sysNotifs;
        readonly List<Item> govNotifs;
        readonly Profile profile;

        public MockDataStore()
        {
            profile = new Profile { Name = "Merdeka bin Jumaat", IC = "010101-01-0001", LicenseValidity = "04/01/2022 - 03/01/2024" };
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Did you just log in?", Description="A new device/browser was used to log in to your account" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Update your contact details", Description="Make sure your contact information is up-to-date so you can stay in touch with us" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "System upgrade", Description="We’re making changes to improve our security. You need to change your password to finish the process. If you don’t, you’ll be asked to do so when you next log in or use the app. If you need help, get in touch with our support team at support@me.gov.my or call 603 8000 8000." },
            };
            sysNotifs = items;
            govNotifs = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Beware of evolving scam techniques", Description="Read more on https://www.malaymail.com/news/malaysia/2023/02/18/minister-govt-launches-national-anti-scam-campaign-2023-to-counter-evolving-scam-techniques/55604" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Don’t fan racial flames, says PM in stern warning", Description="Read more on https://www.freemalaysiatoday.com/category/nation/2023/03/17/dont-fan-racial-flames-says-pm-in-stern-warning/" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Don’t disrupt Johor’s unity, sultan warns", Description="Read more on https://www.freemalaysiatoday.com/category/nation/2023/03/22/dont-disrupt-johors-unity-sultan-warns/" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Flash flood warning issued for 6 states", Description="Read more on https://www.freemalaysiatoday.com/category/nation/2023/01/22/flash-flood-warning-issued-for-6-states/" },
            };
        }

        public async Task<Profile> GetProfileAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(profile);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Item> GetSysNotifsAsync(string id)
        {
            return await Task.FromResult(sysNotifs.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Item> GetGovNotifsAsync(string id)
        {
            return await Task.FromResult(govNotifs.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<Item>> GetSysNotifsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(sysNotifs);
        }

        public async Task<IEnumerable<Item>> GetGovNotifsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(govNotifs);
        }

    }
}
