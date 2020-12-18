using Kaberdin_PostItNotes.Data;
using Kaberdin_PostItNotes.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Kaberdin_PostItNotes.Data.Models.StickerModel;

namespace Kaberdin_PostItNotes.Hubs
{
    public class IndexHub : Hub
    {
        DefaultDbContext dbContext;
        public IndexHub(DefaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override Task OnConnectedAsync()
        {
            var client = Clients.Client(Context.ConnectionId);
            var stickers = dbContext.Stickers.ToArray();
            foreach(StickerModel sticker in stickers)
            {
                client.SendAsync("addSticker", sticker.StickerId, sticker.PositionX, sticker.PositionY, sticker.Width, sticker.Height, sticker.Color, sticker.Content);
            }
            return base.OnConnectedAsync();
        }
        public async Task AddDefaultSticker()
        {
            StickerModel newSticker = new StickerModel()
            {
                Color = StickerColor.Yellow
            };
            dbContext.Stickers.Add(newSticker);
            await dbContext.SaveChangesAsync();
            _ = Clients.All.SendAsync("addSticker", newSticker.StickerId);
        }
        public async Task EditStickerColor(int stickerID,StickerColor color)
        {
            var sticker = dbContext.Stickers.Find(stickerID);
            if(sticker != null)
            {
                sticker.Color = color;
                dbContext.Update(sticker);
                await dbContext.SaveChangesAsync();
                _ = Clients.All.SendAsync("setStickerColor", sticker.StickerId,sticker.Color);
            }
        }
        public async Task RemoveSticker(int stickerID)
        {
            var sticker = dbContext.Stickers.Find(stickerID);
            if(sticker != null)
            {
                dbContext.Remove(sticker);
                await dbContext.SaveChangesAsync();
                _ = Clients.All.SendAsync("removeSticker", stickerID);
            }
        }
        public async Task MoveSticker(int stickerID, float x, float y)
        {
            _ = Clients.AllExcept(Context.ConnectionId).SendAsync("setStickerPosition", stickerID, x, y);
        }
        public async Task MoveStickerEnd(int stickerID,float x, float y)
        {
            var sticker = dbContext.Stickers.Find(stickerID);
            if (sticker != null)
            {
                sticker.PositionX = x;
                sticker.PositionY = y;
                dbContext.Update(sticker);
                await dbContext.SaveChangesAsync();
                _ = Clients.All.SendAsync("setStickerPosition", stickerID, x, y);
            }
        }
        public async Task ResizeSticker(int stickerID,int width,int height,float x, float y)
        {
            _ = Clients.AllExcept(Context.ConnectionId).SendAsync("setStickerSize", stickerID, width, height);
            _ = Clients.AllExcept(Context.ConnectionId).SendAsync("setStickerPosition", stickerID, x, y);
        }
        public async Task ResizeStickerEnd(int stickerID, int width,int height, float x, float y)
        {
            var sticker = dbContext.Stickers.Find(stickerID);
            if (sticker != null)
            {
                sticker.Width = width;
                sticker.Height = height;
                sticker.PositionX = x;
                sticker.PositionY = y;
                dbContext.Update(sticker);
                await dbContext.SaveChangesAsync();
                _ = Clients.All.SendAsync("setStickerSize", stickerID, width, height);
                _ = Clients.All.SendAsync("setStickerPosition", stickerID, x, y);
            }
        }
        public async Task EditContentSticker(int stickerID, String HTML)
        {
            var sticker = dbContext.Stickers.Find(stickerID);
            if (sticker != null)
            {
                sticker.Content = HTML;
                dbContext.Update(sticker);
                await dbContext.SaveChangesAsync();
                _ = Clients.All.SendAsync("setStickerContent", stickerID, HTML);
            }
        }

    }
}
